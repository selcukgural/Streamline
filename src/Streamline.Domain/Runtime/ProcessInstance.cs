namespace Streamline.Domain.Runtime;

/// <summary>
/// Represents a single execution of a process definition.
/// </summary>
public sealed class ProcessInstance : EntityBase
{
    /// <summary>
    /// Identifier of the BPMN process definition this instance belongs to.
    /// </summary>
    public required string ProcessDefinitionId { get; init; } // Set via constructor or method

    /// <summary>
    /// Optional business key associated with this process instance.
    /// </summary>
    public string? BusinessKey { get; init; }

    /// <summary>
    /// Current state of the process instance.
    /// </summary>
    public ProcessInstanceState State { get; set; } // State can change

    /// <summary>
    /// Timestamp when the process instance was started.
    /// </summary>
    public DateTime StartTime { get; init; }

    /// <summary>
    /// Timestamp when the process instance ended (completed, aborted, terminated).
    /// Null if the instance is still running or suspended.
    /// </summary>
    public DateTime? EndTime { get; set; }

    // Navigation Properties

    private readonly List<Variable> _variables = [];

    /// <summary>
    /// Variables associated with this process instance.
    /// </summary>
    public IReadOnlyCollection<Variable> Variables => _variables.AsReadOnly();

    private readonly List<ActivityInstance> _activityInstances = [];

    /// <summary>
    /// Activity instances belonging to this process instance.
    /// </summary>
    public IReadOnlyCollection<ActivityInstance> ActivityInstances => _activityInstances.AsReadOnly();

    private readonly List<Execution> _executions = [];

    /// <summary>
    /// Executions (tokens) currently active within this process instance.
    /// </summary>
    public IReadOnlyCollection<Execution> Executions => _executions.AsReadOnly();

    private readonly List<EventSubscription> _eventSubscriptions = [];

    /// <summary>
    /// Event subscriptions associated with this process instance.
    /// </summary>
    public IReadOnlyCollection<EventSubscription> EventSubscriptions => _eventSubscriptions.AsReadOnly();

    private readonly List<Job> _jobs = [];

    /// <summary>
    /// Jobs associated with this process instance (e.g., timers, async tasks).
    /// </summary>
    public IReadOnlyCollection<Job> Jobs => _jobs.AsReadOnly();

    private readonly List<Incident> _incidents = [];

    /// <summary>
    /// Incidents that occurred within this process instance.
    /// </summary>
    public IReadOnlyCollection<Incident> Incidents => _incidents.AsReadOnly();

    // Private constructor for EF Core
    private ProcessInstance()
    {
        // ProcessDefinitionId = null!; // Initialization no longer needed with init
        // Collections initialized here for EF Core hydration
        _variables = [];
        _activityInstances = [];
        _executions = [];
        _eventSubscriptions = [];
        _jobs = [];
        _incidents = [];
    }

    // Public constructor
    public ProcessInstance(string processDefinitionId, string? businessKey = null)
    {
        ProcessDefinitionId = processDefinitionId;
        BusinessKey = businessKey;
        State = ProcessInstanceState.Running; // Initial state
        StartTime = DateTime.UtcNow;

        // Initialize collections
        _variables = [];
        _activityInstances = [];
        _executions = [];
        _eventSubscriptions = [];
        _jobs = [];
        _incidents = [];

        // Add initial execution token
        // Need to satisfy required ProcessInstance for Execution
        // _executions.Add(new Execution { ProcessInstanceId = this.Id, IsActive = true, IsScope = true, ProcessInstance = this });
        _executions.Add(new Execution(this)); // Use the updated constructor
    }

    // Methods to manage collections (example)
    public void AddVariable(Variable variable)
    {
        // Add logic to handle existing variable names etc.
        _variables.Add(variable); // Simplified for now
    }

    public void AddActivityInstance(ActivityInstance activityInstance)
    {
        _activityInstances.Add(activityInstance);
    }

    // Method to add a new execution (called by ExecutionFlowManager)
    public void AddExecution(Execution execution)
    {
        if (execution.ProcessInstanceId != Id)
        {
            throw new ArgumentException("Execution belongs to a different process instance.", nameof(execution));
        }

        if (!_executions.Any(e => e.Id == execution.Id))
        {
            _executions.Add(execution);
        }
    }

    // Method to add a new incident
    public void AddIncident(Incident incident)
    {
        if (incident.ProcessInstanceId != Id)
        {
            throw new ArgumentException("Incident belongs to a different process instance.", nameof(incident));
        }

        if (!_incidents.Any(i => i.Id == incident.Id))
        {
            _incidents.Add(incident);
        }
    }

    // Add methods for state transitions, adding/removing executions etc.

    public void Complete()
    {
        if (State == ProcessInstanceState.Completed || State == ProcessInstanceState.Aborted ||
            State == ProcessInstanceState.Terminated)
        {
            // Already ended, maybe log a warning or ignore?
            return;
        }

        State = ProcessInstanceState.Completed;
        EndTime = DateTime.UtcNow;
        // Potentially clean up active executions?
    }

    public void Suspend()
    {
        if (State != ProcessInstanceState.Running)
        {
            // Can only suspend a running instance
            throw new InvalidOperationException(
                $"Cannot suspend process instance {Id} because it is not in Running state (Current: {State})");
        }

        State = ProcessInstanceState.Suspended;
        // Suspend related jobs?
    }

    public void Activate()
    {
        if (State != ProcessInstanceState.Suspended)
        {
            // Can only activate a suspended instance
            throw new InvalidOperationException(
                $"Cannot activate process instance {Id} because it is not in Suspended state (Current: {State})");
        }

        State = ProcessInstanceState.Running;
        // Reactivate related jobs?
    }

    public void Terminate(bool force = false) // Optional parameter to bypass checks
    {
        if (State == ProcessInstanceState.Completed || State == ProcessInstanceState.Aborted ||
            State == ProcessInstanceState.Terminated)
        {
            return; // Already ended
        }

        State = ProcessInstanceState.Terminated;
        EndTime = DateTime.UtcNow;
        // Terminate active executions and associated jobs/subscriptions
        // This logic might be complex and handled by the engine calling this method.
    }

    public void Abort(string? reason = null)
    {
        if (State == ProcessInstanceState.Completed || State == ProcessInstanceState.Aborted ||
            State == ProcessInstanceState.Terminated)
        {
            return; // Already ended
        }

        State = ProcessInstanceState.Aborted;
        EndTime = DateTime.UtcNow;
        // Log the reason? Create an incident?
        // Terminate active executions etc.
    }
}