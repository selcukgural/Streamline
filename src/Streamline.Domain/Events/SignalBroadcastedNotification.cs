using MediatR;

namespace Streamline.Domain.Events;

/// <summary>
/// Notification published when a signal is broadcasted by a throw event.
/// Represents a significant domain event related to signal propagation.
/// </summary>
/// <param name="SignalName">The name of the signal that was broadcasted.</param>
/// <param name="SourceProcessInstanceId">The ID of the process instance that broadcasted the signal (optional, for context).</param>
public record SignalBroadcastedNotification(
    string SignalName, 
    Guid SourceProcessInstanceId
    // string? TenantId = null 
    ) : INotification; 