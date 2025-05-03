using MediatR;
using Microsoft.Extensions.Logging;
using Streamline.Domain.Events; // For TimerFiredNotification
using Hangfire;

namespace Streamline.Application.Services; // Assuming Services namespace exists

/// <summary>
/// Service containing methods that can be invoked by the background job scheduler (e.g., Hangfire)
/// to trigger application events or notifications.
/// </summary>
public class TimerJobTriggerService(IMediator mediator, ILogger<TimerJobTriggerService> logger)
{
    /// <summary>
    /// Method to be called by the background scheduler when a timer is due.
    /// It publishes the TimerFiredNotification via MediatR.
    /// </summary>
    /// <param name="eventSubscriptionId">The ID of the event subscription that triggered this job.</param>
    [JobDisplayName("Trigger Timer Event: Sub ID {0}")] // Optional: Makes job name clearer in Hangfire dashboard
    public async Task TriggerTimerFiredNotificationAsync(Guid eventSubscriptionId)
    {
        try
        {
            logger.LogInformation("Timer job triggered for EventSubscription {EventSubscriptionId}. Publishing notification...", eventSubscriptionId);
            var notification = new TimerFiredNotification(eventSubscriptionId);
            await mediator.Publish(notification); // Use default CancellationToken or pass one if available
            logger.LogInformation("Successfully published TimerFiredNotification for EventSubscription {EventSubscriptionId}", eventSubscriptionId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error publishing TimerFiredNotification for EventSubscription {EventSubscriptionId} from background job.", eventSubscriptionId);
            // Re-throwing might cause Hangfire to retry the job depending on configuration.
            // Consider specific error handling or retry policies.
            throw; 
        }
    }
} 