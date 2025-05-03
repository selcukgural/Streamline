using MediatR;

namespace Streamline.Domain.Events;

/// <summary>
/// Notification published when a scheduled timer event is fired.
/// </summary>
/// <param name="EventSubscriptionId">The ID of the EventSubscription corresponding to the fired timer.</param>
public record TimerFiredNotification(
    Guid EventSubscriptionId
    ) : INotification; 