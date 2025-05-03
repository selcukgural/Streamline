using Streamline.Domain.Runtime;

namespace Streamline.Domain.Events;

/// <summary>
/// Raised when an activity instance enters the Faulted state.
/// </summary>
public sealed record ActivityFailedEvent : DomainEvent
{
    public Guid ActivityInstanceId { get; }
    public Guid ProcessInstanceId { get; }
    public Guid? ExecutionId { get; }
    public string FlowNodeId { get; }
    public string? ErrorMessage { get; }
    public string? ErrorDetails { get; }

    public ActivityFailedEvent(ActivityInstance activityInstance)
    {
        ActivityInstanceId = activityInstance.Id;
        ProcessInstanceId = activityInstance.ProcessInstanceId;
        ExecutionId = activityInstance.ExecutionId;
        FlowNodeId = activityInstance.FlowNodeId;
        ErrorMessage = activityInstance.ErrorMessage;
        ErrorDetails = activityInstance.ErrorDetails;
    }
} 