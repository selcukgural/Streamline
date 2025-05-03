using MediatR;

namespace Streamline.Domain.Events;

/// <summary>
/// Notification published when a message is broadcasted by a throw event.
/// Represents a significant domain event related to message propagation.
/// </summary>
/// <param name="MessageName">The name of the message that was broadcasted.</param>
/// <param name="SourceProcessInstanceId">The ID of the process instance that broadcasted the message.</param>
/// <param name="Payload">Optional payload/variables carried by the message.</param> 
public record MessageBroadcastedNotification(
    string MessageName,
    Guid SourceProcessInstanceId
    // Dictionary<string, object>? Payload = null 
    // string? TenantId = null
    ) : INotification; 