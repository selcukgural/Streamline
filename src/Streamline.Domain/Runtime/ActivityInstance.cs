using Streamline.Domain.Events; // DomainEvent için

namespace Streamline.Domain.Runtime;

/// <summary>
/// Represents the state of a specific Flow Node (e.g., Task, Gateway, Event)
/// within a running Process Instance.
/// </summary>
public sealed class ActivityInstance : EntityBase
{
    /// <summary>
    /// ID of the Process Instance this activity belongs to.
    /// </summary>
    public Guid ProcessInstanceId { get; init; }

    public ProcessInstance ProcessInstance { get; set; }

    /// <summary>
    /// ID of the Execution this activity instance is associated with.
    /// An activity might be directly under the process instance (null ExecutionId) or under a specific execution scope.
    /// </summary>
    public Guid? ExecutionId { get; init; }
    public Execution? Execution { get; init; }

    /// <summary>
    /// ID of the BPMN Flow Node element (e.g., Task ID, Gateway ID) in the definition.
    /// </summary>
    public string FlowNodeId { get; init; }

    /// <summary>
    /// Optional: Name of the Flow Node element from the definition (for easier debugging/logging).
    /// </summary>
    public string? FlowNodeName { get; init; }

    /// <summary>
    /// Current state of this activity instance.
    /// </summary>
    public ActivityInstanceState State { get; set; }

    /// <summary>
    /// Timestamp when this activity instance became active.
    /// </summary>
    public DateTime StartTime { get; init; }

    /// <summary>
    /// Timestamp when this activity instance ended (Completed, Faulted, Terminated, Cancelled).
    /// Null if still active.
    /// </summary>
    public DateTime? EndTime { get; set; }

    // --- Task Specific --- 
    /// <summary>
    /// ID of the user this task is assigned to (for User Tasks).
    /// </summary>
    public string? AssigneeId { get; set; }

    /// <summary>
    /// Comma-separated list of candidate user IDs (for User Tasks).
    /// </summary>
    public string? CandidateUserIds { get; set; }

    /// <summary>
    /// Comma-separated list of candidate group IDs (for User Tasks).
    /// </summary>
    public string? CandidateGroupIds { get; set; }

    /// <summary>
    /// Due date for the task (optional).
    /// </summary>
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// Follow-up date for the task (optional).
    /// </summary>
    public DateTime? FollowUpDate { get; set; }

    /// <summary>
    /// Priority of the task (optional).
    /// </summary>
    public int? Priority { get; set; }
    // ---------------------

    // --- Call Activity Specific ---
    /// <summary>
    /// ID of the process instance called by this Call Activity instance.
    /// </summary>
    public Guid? CalledProcessInstanceId { get; set; }
    // ---------------------------

    // --- Error/Fault Specific ---
    /// <summary>
    /// Error message if the state is Faulted.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Stack trace or error details if the state is Faulted.
    /// </summary>
    public string? ErrorDetails { get; set; } // Consider TEXT type in DB
    // ---------------------------

    // Private constructor for EF Core
    private ActivityInstance()
    {
        // No explicit initialization needed with init settersß
        // ProcessInstance = null!;
        // FlowNodeId = null!;
    }

    // Public constructor
    public ActivityInstance(Guid processInstanceId, string flowNodeId, Guid? executionId = null, string? flowNodeName = null)
    {
        ProcessInstanceId = processInstanceId;
        FlowNodeId = flowNodeId;
        ExecutionId = executionId;
        FlowNodeName = flowNodeName;
        State = ActivityInstanceState.Active; // Initial state
        StartTime = DateTime.UtcNow;
        // ProcessInstance/Execution property will be set by EF Core or explicitly if needed.
    }

    // --- Behavior Methods ---

    public void Complete()
    {
        if (State != ActivityInstanceState.Active)
        {
            // Can only complete an active instance (or maybe Compensating? depends on logic)
            throw new InvalidOperationException(
                $"Cannot complete activity instance {Id} because it is not in Active state (Current: {State})");
        }

        State = ActivityInstanceState.Completed;
        EndTime = DateTime.UtcNow;
    }

    public void Fail(string errorMessage, string? errorDetails = null)
    {
        if (State != ActivityInstanceState.Active)
        {
            throw new InvalidOperationException(
                $"Cannot mark activity instance {Id} as failed because it is not in Active state (Current: {State})");
        }

        State = ActivityInstanceState.Faulted;
        EndTime = DateTime.UtcNow;
        ErrorMessage = errorMessage;
        ErrorDetails = errorDetails;

        // Raise domain event for failure
        AddDomainEvent(new ActivityFailedEvent(this));
    }

    public void Cancel()
    {
        // Allow cancellation from Active or potentially other states depending on engine rules
        if (State == ActivityInstanceState.Completed || State == ActivityInstanceState.Faulted ||
            State == ActivityInstanceState.Cancelled)
        {
            return; // Already ended
        }

        State = ActivityInstanceState.Cancelled;
        EndTime = DateTime.UtcNow;
        // Trigger compensation? Clean up related data?
    }

    public void Assign(string assigneeId)
    {
        // Typically only applicable to User Tasks and maybe others
        if (State != ActivityInstanceState.Active)
        {
            throw new InvalidOperationException(
                $"Cannot assign activity instance {Id} because it is not in Active state (Current: {State})");
        }

        AssigneeId = assigneeId;
        // Maybe clear candidate users/groups if applicable
    }

    // Add methods for setting candidate users/groups, due dates etc. if needed

    // -----------------------
}