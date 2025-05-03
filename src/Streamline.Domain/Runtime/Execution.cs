using Streamline.Domain.Events; // ExecutionSignaledEvent için (Namespace düzeltildi)

namespace Streamline.Domain.Runtime;

/// <summary>
/// Represents a path of execution (token) within a process instance.
/// A process instance can have multiple concurrent executions (e.g., after a parallel gateway).
/// </summary>
public sealed class Execution : EntityBase
{
    /// <summary>
    /// ID of the Process Instance this execution belongs to.
    /// </summary>
    public Guid ProcessInstanceId { get; init; }
    public ProcessInstance ProcessInstance { get; set; }

    /// <summary>
    /// ID of the parent execution, if this is a nested execution (e.g., within a subprocess).
    /// Null for the main execution path of the process instance.
    /// </summary>
    public Guid? ParentExecutionId { get; init; }
    public Execution? ParentExecution { get; init; } // Navigation property

    /// <summary>
    /// ID of the Flow Node where this execution is currently located (waiting or active).
    /// Can be null initially or temporarily.
    /// </summary>
    public string? CurrentFlowNodeId { get; set; }

    /// <summary>
    /// If the execution is inactive because it's waiting at a converging gateway, 
    /// this holds the ID of that gateway. Otherwise null.
    /// </summary>
    public string? WaitingAtGatewayId { get; set; }

    /// <summary>
    /// Indicates if this execution path is currently active.
    /// Becomes false when the path terminates or merges.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Indicates if this execution is related to a scope (e.g., subprocess).
    /// </summary>
    public bool IsScope { get; init; } // Can be determined when created

    /// <summary>
    /// If IsScope is true and this represents a SubProcess scope,
    /// this holds the ID of the SubProcess node in the parent scope's definition.
    /// Null for the main process scope.
    /// </summary>
    public string? ScopeFlowNodeId { get; init; } // Added ScopeFlowNodeId

    /// <summary>
    /// If the execution failed, this contains the BPMN Error Code.
    /// </summary>
    public string? ErrorCode { get; private set; } // Added ErrorCode
    
    /// <summary>
    /// If the execution failed, this contains the error message.
    /// </summary>
    public string? ErrorMessage { get; private set; } // Added ErrorMessage

    /// <summary>
    /// If the execution failed, this contains the ID of the FlowNode where the failure occurred.
    /// </summary>
    public string? FailedAtNodeId { get; private set; } // Added FailedAtNodeId

    /// <summary>
    /// If an escalation occurred, this contains the BPMN Escalation Code.
    /// </summary>
    public string? LastEscalationCode { get; private set; } // Added EscalationCode

    // Navigation Properties (One Execution can have multiple associated items)

    private readonly List<Variable> _variables = [];
    /// <summary>
    /// Variables local to this execution scope.
    /// </summary>
    public IReadOnlyCollection<Variable> Variables => _variables.AsReadOnly();

    private readonly List<EventSubscription> _eventSubscriptions = [];
    /// <summary>
    /// Event subscriptions currently waiting on this execution path.
    /// </summary>
    public IReadOnlyCollection<EventSubscription> EventSubscriptions => _eventSubscriptions.AsReadOnly();
        
    private readonly List<Job> _jobs;
    /// <summary>
    /// Jobs associated with this execution (e.g., async continuation job for this path).
    /// </summary>
    public IReadOnlyCollection<Job> Jobs => _jobs.AsReadOnly();

    private readonly List<Incident> _incidents;
    /// <summary>
    /// Incidents that occurred within this execution path.
    /// </summary>
    public IReadOnlyCollection<Incident> Incidents => _incidents.AsReadOnly();

    // Private constructor for EF Core
    private Execution() 
    {
        ProcessInstance = null!; // Still need null! for private constructor warning if non-nullable
        _variables = [];
        _eventSubscriptions = [];
        _jobs = [];
        _incidents = [];
    }

    // Public constructor for root execution
    public Execution(ProcessInstance processInstance)
    {
        ArgumentNullException.ThrowIfNull(processInstance);
        ProcessInstanceId = processInstance.Id;
        ProcessInstance = processInstance; // Set navigation property
        ParentExecutionId = null; 
        CurrentFlowNodeId = null; // Typically set when entering the first node
        IsActive = true;
        IsScope = true; // Root execution is a scope
        ScopeFlowNodeId = null; // Root process scope has no node ID
        // Initialize collections
        _variables = [];
        _eventSubscriptions = [];
        _jobs = [];
        _incidents = [];
    }

    // Public constructor for child execution
    public Execution(Execution parentExecution, string? currentFlowNodeId = null, bool isScope = false, string? scopeFlowNodeId = null)
    {
        ArgumentNullException.ThrowIfNull(parentExecution);
        ProcessInstanceId = parentExecution.ProcessInstanceId;
        ProcessInstance = parentExecution.ProcessInstance; // Set navigation property from parent
        ParentExecutionId = parentExecution.Id;
        ParentExecution = parentExecution; // Set navigation property
        CurrentFlowNodeId = currentFlowNodeId;
        IsActive = true;
        IsScope = isScope;
        ScopeFlowNodeId = isScope ? scopeFlowNodeId : null; // Set only if it's a scope execution
        // Validate that scopeFlowNodeId is provided if isScope is true?
        if (isScope && string.IsNullOrEmpty(scopeFlowNodeId)){
             // Maybe log a warning or throw? Depends on strictness needed.
             // For now, allow null but it might cause issues later if used.
             // throw new ArgumentException("scopeFlowNodeId must be provided when isScope is true", nameof(scopeFlowNodeId));
        }
        // Initialize collections
        _variables = [];
        _eventSubscriptions = [];
        _jobs = [];
        _incidents = [];
    }

    // Methods to manage collections (example)
    public void AddVariableLocal(Variable variable)
    {
        // Add logic for local scope variables
        _variables.Add(variable);
    }
    public void AddEventSubscription(EventSubscription subscription)
    {
        _eventSubscriptions.Add(subscription);
    }
    public void AddJob(Job job)
    {
        _jobs.Add(job);
    }
    public void AddIncident(Incident incident)
    {
        _incidents.Add(incident);
    }

    // --- Behavior Methods ---

    /// <summary>
    /// Moves the execution into the specified flow node.
    /// Typically called by the engine when a sequence flow is taken.
    /// </summary>
    /// <param name="flowNodeId">The ID of the target flow node.</param>
    public void EnterFlowNode(string flowNodeId)
    {
        if (!IsActive)
        {
            throw new InvalidOperationException($"Cannot enter flow node {flowNodeId} because execution {Id} is not active.");
        }
        CurrentFlowNodeId = flowNodeId;
    }

    /// <summary>
    /// Signals that the execution is leaving the current flow node.
    /// This often triggers the engine to evaluate outgoing sequence flows.
    /// </summary>
    public void LeaveFlowNode()
    {
        if (!IsActive || CurrentFlowNodeId == null)
        {
            throw new InvalidOperationException($"Cannot leave flow node because execution {Id} is not active or not currently at a node.");
        }
        // CurrentFlowNodeId = null; // Or keep it for history? Depends on design.
        // The engine will typically take over here to find the next node.
    }

    /// <summary>
    /// Terminates this execution path.
    /// If this is the last active execution, it might lead to process instance termination.
    /// </summary>
    /// <param name="reason">Optional reason for termination.</param>
    public void Terminate(string? reason = null)
    {
        if (!IsActive)
        {
            return; // Already inactive
        }
        IsActive = false;
        // Log the reason if provided? 
        // Or store it on the Execution? Need a new field: TerminationReason
        // For now, the reason isn't stored on the entity.
        if (WaitingAtGatewayId == null) 
        {
            CurrentFlowNodeId = null; 
        }
    }

    /// <summary>
    /// Creates a new child execution of the current one.
    /// Used for parallel flows or entering scopes (like sub-processes).
    /// </summary>
    /// <param name="isScope">Indicates if the new execution represents a new variable scope.</param>
    /// <param name="scopeFlowNodeId">If IsScope is true and this represents a SubProcess scope,
    /// this holds the ID of the SubProcess node in the parent scope's definition.
    /// Null for the main process scope.</param>
    /// <returns>The newly created child execution.</returns>
    public Execution CreateChildExecution(bool isScope = false, string? scopeFlowNodeId = null)
    {
        if (!IsActive)
        {
            throw new InvalidOperationException($"Cannot create child execution from inactive execution {Id}.");
        }
            
        // Create the child, linking it to this execution and the same process instance
        var childExecution = new Execution(this, CurrentFlowNodeId, isScope, scopeFlowNodeId);
            
        // The engine or calling logic is responsible for adding this child 
        // to the ProcessInstance's _executions collection if direct tracking is needed,
        // or managing it via the parent relationship.
        return childExecution;
    }

    /// <summary>
    /// Signals that the execution has completed its work at the current node 
    /// and is ready to proceed. This triggers the engine to evaluate
    /// outgoing sequence flows based on the signal by raising a domain event.
    /// </summary>
    /// <param name="signalName">Optional signal identifier (e.g., outgoing sequence flow ID).</param>
    /// <param name="data">Optional data associated with the signal.</param>
    public void Signal(string? signalName = null, object? data = null)
    {
        if (!IsActive)
        {
            throw new InvalidOperationException($"Cannot signal from inactive execution {Id}.");
        }
        if (CurrentFlowNodeId == null)
        {
            throw new InvalidOperationException($"Cannot signal from execution {Id} as it is not currently positioned at a flow node.");
        }

        // Remove direct call to engine logic
        // LeaveFlowNode(); 
            
        // Raise domain event instead
        var signaledEvent = new ExecutionSignaledEvent(
            ExecutionId: Id,
            ProcessInstanceId: ProcessInstanceId,
            CurrentFlowNodeId: CurrentFlowNodeId, // Pass current node ID
            SignalName: signalName, 
            Data: data
        );
        AddDomainEvent(signaledEvent); // Use the method from EntityBase

        // TODO: Consider removing the original TODO comment below if this fulfills the requirement
        // TODO: Consider raising a domain event here instead of directly calling LeaveFlowNode
        // AddDomainEvent(new ExecutionSignaledEvent(this.Id, signalName, data));
    }

    // Add a specific method for arriving at a converging gateway
    public void ArriveAtConvergingGateway(string gatewayId)
    {
        if (!IsActive)
        {
            throw new InvalidOperationException($"Inactive execution {Id} cannot arrive at gateway {gatewayId}.");
        }
        IsActive = false; // Execution becomes inactive, waiting for others
        CurrentFlowNodeId = gatewayId; // Mark location
        WaitingAtGatewayId = gatewayId; // Mark waiting state
        // Don't clear CurrentFlowNodeId here
    }

    /// <summary>
    /// Reactivates an execution that was waiting at a converging gateway.
    /// </summary>
    public void Reactivate()
    {
        if (IsActive)
        {
            // Already active, do nothing or log a warning
            return; 
        }
        if (WaitingAtGatewayId == null)
        {
            throw new InvalidOperationException($"Cannot reactivate execution {Id} as it was not waiting at a converging gateway.");
        }
        IsActive = true;
        // WaitingAtGatewayId should be cleared by LeaveConvergingGateway
    }

    /// <summary>
    /// Clears the waiting state after an execution successfully leaves a converging gateway.
    /// </summary>
    public void LeaveConvergingGateway()
    {
        // No need to check IsActive here, can be called after Reactivate
        WaitingAtGatewayId = null;
        // CurrentFlowNodeId is typically updated by EnterFlowNode of the next node
    }

    /// <summary>
    /// Marks the execution as failed.
    /// </summary>
    /// <param name="errorMessage">Reason for failure.</param>
    /// <param name="errorDetails">Optional details.</param>
    public void Fail(string errorMessage, string? errorDetails = null)
    {
        FailInternal(null, errorMessage, errorDetails);
    }
    
    /// <summary>
    /// Marks the execution as failed with a specific BPMN Error Code.
    /// </summary>
    /// <param name="errorCode">BPMN Error Code.</param>
    /// <param name="errorMessage">Reason for failure.</param>
    /// <param name="errorDetails">Optional details.</param>
    public void Fail(string errorCode, string errorMessage, string? errorDetails = null) // Added overload
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(errorCode, nameof(errorCode));
        FailInternal(errorCode, errorMessage, errorDetails);
    }

    private void FailInternal(string? errorCode, string errorMessage, string? errorDetails)
    {
        if (!IsActive)
        {
            // Log or decide if throwing is appropriate when trying to fail an inactive execution
            // For now, just return to avoid issues if called multiple times.
            return; 
            // throw new InvalidOperationException($"Cannot fail inactive execution {Id}.");
        }
        IsActive = false;
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
        FailedAtNodeId = CurrentFlowNodeId; // Store the node where failure happened
        CurrentFlowNodeId = null; // Now clear current node
        WaitingAtGatewayId = null;
        
        // TODO: Raise domain event
    }

    /// <summary>
    /// Triggers an escalation event from this execution.
    /// This typically does not change the execution state but signals that an escalation should be handled.
    /// </summary>
    /// <param name="escalationCode">The BPMN Escalation Code.</param>
    /// <param name="escalationName">Optional: Name associated with the escalation.</param>
    public void Escalate(string escalationCode, string? escalationName = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(escalationCode, nameof(escalationCode));

        if (!IsActive)
        {
            // Log warning? Escalation from inactive execution might be invalid.
            // throw new InvalidOperationException($"Cannot escalate from inactive execution {Id}.");
            return; // Or allow it?
        }

        LastEscalationCode = escalationCode;
        // CurrentFlowNodeId might be relevant here too, or we might need EscalatedAtNodeId?
        // For now, just store the code.

        // Raise domain event for Escalation
        AddDomainEvent(new ExecutionEscalatedEvent(Id, ProcessInstanceId, escalationCode, escalationName, CurrentFlowNodeId));
        // Note: ExecutionEscalatedEvent needs to be created in Streamline.Domain.Events
    }

    // TODO: Add methods for creating child executions (Forking for parallel gateways)
    // TODO: Add methods for signaling/triggering (used by events, tasks)

    // -----------------------
}