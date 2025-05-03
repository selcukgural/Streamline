namespace Streamline.Domain.Runtime;

/// <summary>
/// Represents a subscription to an event (e.g., message, signal, timer).
/// Typically created when an execution reaches a waiting state (e.g., intermediate catch event, event subprocess start).
/// </summary>
public sealed class EventSubscription(
    string eventType,
    string eventName,
    Guid executionId,
    Execution execution,
    Guid processInstanceId,
    string activityId,
    string? configuration = null,
    string? tenantId = null,
    string? attachedToActivityId = null)
    : EntityBase
{
    /// <summary>
    /// Type of the event (e.g., "message", "signal", "timer", "conditional", "compensation").
    /// </summary>
    public string EventType { get; init; } = eventType;

    /// <summary>
    /// Name of the event (e.g., message name, signal name).
    /// </summary>
    public string EventName { get; init; } = eventName; // Can be null for some types like timer

    /// <summary>
    /// ID of the Execution that is waiting for this event.
    /// </summary>
    public Guid ExecutionId { get; init; } = executionId;

    public Execution Execution { get; init; } = execution; // Assign required navigation property
    // Made init

    /// <summary>
    /// ID of the Process Instance this subscription belongs to.
    /// </summary>
    public Guid ProcessInstanceId { get; init; } = processInstanceId;
    // No direct navigation to ProcessInstance needed if accessible via Execution

    /// <summary>
    /// ID of the activity (catch event, boundary event, event subprocess) that created this subscription.
    /// </summary>
    public string ActivityId { get; init; } = activityId; // BPMN element ID

    /// <summary>
    /// Optional: ID of the Activity this event is attached to (for Boundary Events).
    /// </summary>
    public string? AttachedToActivityId { get; init; } = attachedToActivityId;

    /// <summary>
    /// Timestamp when the subscription was created.
    /// </summary>
    public DateTime CreatedTime { get; init; } = DateTime.UtcNow;

    /// <summary>
    /// Optional: Configuration for the subscription (e.g., timer definition, correlation keys for messages).
    /// Might store as JSON or separate fields.
    /// </summary>
    public string? Configuration { get; private set; } = configuration;

    /// <summary>
    /// Optional: Tenant ID if using multi-tenancy.
    /// </summary>
    public string? TenantId { get; init; } = tenantId; // Important for targeted event delivery

    /// <summary>
    /// Optional: Job ID for scheduler integration.
    /// </summary>
    public string? JobId { get; set; }

    // Private parameterless constructor MAY be needed for EF Core depending on configuration
    // If EF Core complains later, uncomment this.
    // private EventSubscription() { }

    // Public constructor taking all necessary fields
    // JobId is initially null

    public void SetConfiguration(string? configuration)
    {
        Configuration = configuration;
    }
}