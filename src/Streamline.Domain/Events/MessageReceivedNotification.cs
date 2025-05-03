using MediatR;

namespace Streamline.Domain.Events;

/// <summary>
/// Notification published when a message event (intermediate catch, boundary) 
/// is received and correlated to an existing subscription.
/// </summary>
/// <param name="EventSubscriptionId">The ID of the EventSubscription that matched the message.</param>
/// <param name="Payload">Optional: Data received with the message.</param>
/// <param name="CorrelationKey">Optional: Correlation key used for matching.</param>
public record MessageReceivedNotification(
    Guid EventSubscriptionId, 
    object? Payload = null,
    string? CorrelationKey = null
    ) : INotification; 