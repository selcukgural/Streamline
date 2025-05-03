namespace Streamline.Domain.Events;

/// <summary>
/// Domain event raised when an escalation is triggered by an execution.
/// </summary>
/// <param name="ExecutionId">ID of the execution triggering the escalation.</param>
/// <param name="ProcessInstanceId">ID of the process instance.</param>
/// <param name="EscalationCode">The BPMN Escalation Code.</param>
/// <param name="EscalationName">Optional: Name of the escalation.</param>
/// <param name="ActivityId">Optional: ID of the activity where the escalation occurred.</param>
public record ExecutionEscalatedEvent(
    Guid ExecutionId,
    Guid ProcessInstanceId,
    string EscalationCode,
    string? EscalationName,
    string? ActivityId // Node where escalation was triggered
    ) : DomainEvent; 
    // Assuming DomainEvent is a base class or marker interface for domain events 