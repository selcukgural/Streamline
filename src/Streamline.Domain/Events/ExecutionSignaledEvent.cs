namespace Streamline.Domain.Events; 
/// <summary>
/// Represents an event raised when an execution signals it's ready to continue.
/// </summary>
/// <param name="ExecutionId">The ID of the execution that was signaled.</param>
/// <param name="ProcessInstanceId">The ID of the process instance.</param>
/// <param name="CurrentFlowNodeId">The ID of the flow node where the signal occurred.</param>
/// <param name="SignalName">Optional signal identifier.</param>
/// <param name="Data">Optional data associated with the signal.</param>
public record ExecutionSignaledEvent(
    Guid ExecutionId,
    Guid ProcessInstanceId,
    string CurrentFlowNodeId, // Include node ID for context
    string? SignalName,
    object? Data
) : DomainEvent; // Inherit from DomainEvent 