using MediatR;

namespace Streamline.Domain.Events;

/// <summary>
/// Notification published when a signal event (intermediate catch, boundary, or throw followed by catch) 
/// is broadcast and matched to an existing subscription.
/// </summary>
/// <param name="EventSubscriptionId">The ID of the EventSubscription that matched the signal.</param>
/// <param name="Payload">Optional: Data broadcast with the signal.</param>
public record SignalReceivedNotification(
    Guid EventSubscriptionId,
    object? Payload = null
    ) : INotification; 