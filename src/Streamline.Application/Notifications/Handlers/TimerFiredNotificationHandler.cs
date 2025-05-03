using MediatR;
using Microsoft.Extensions.Logging;
using Streamline.Domain.Abstractions;
using Streamline.Domain.Runtime;
using Streamline.Domain.Events; // For TimerFiredNotification
// For parsing
using System.Text.RegularExpressions; // For basic cycle parsing
// Added for XmlConvert.ToTimeSpan
using NodaTime; // Added NodaTime
using NodaTime.Text; // Added for parsing
// Added for LINQ

// Added for ActivityInstance

namespace Streamline.Application.Notifications.Handlers;

/// <summary>
/// Handles the TimerFiredNotification to trigger the execution waiting on a timer event.
/// </summary>
public class TimerFiredNotificationHandler(
    IRepository<EventSubscription> subscriptionRepository,
    IRepository<Execution> executionRepository,
    IExecutionFlowManager executionFlowManager,
    IUnitOfWork unitOfWork,
    ILogger<TimerFiredNotificationHandler> logger,
    ITimerJobScheduler timerJobScheduler)
    : INotificationHandler<TimerFiredNotification>
{
    // Added scheduler

    // Injected scheduler

    public async Task Handle(TimerFiredNotification notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling TimerFiredNotification for Subscription {SubscriptionId}", notification.EventSubscriptionId);

        var subscriptionRepo = unitOfWork.GetRepository<EventSubscription>();
        var subscription = await subscriptionRepo.GetByIdAsync(notification.EventSubscriptionId, cancellationToken);

        if (subscription == null)
        {            
            logger.LogWarning("Subscription {SubscriptionId} not found for fired timer.", notification.EventSubscriptionId);
            return;
        }

        if (subscription.ExecutionId == null)
        {            
            logger.LogWarning("Subscription {SubscriptionId} has no execution ID.", notification.EventSubscriptionId);
            return;
        }

        // Prevent race conditions or duplicate processing?
        // Optional: Check subscription state or add locking if needed.

        // Extract timer definition type and value
        if (string.IsNullOrEmpty(subscription.Configuration) || !subscription.Configuration.Contains(':'))
        {            
            logger.LogWarning("Subscription {SubscriptionId} has invalid configuration format.", notification.EventSubscriptionId);
            return;
        }

        var configParts = subscription.Configuration.Split(';', 2); // Config ve Cancel=true/false ayır
        var timerParts = configParts[0].Split(':', 2);
        string timerType = timerParts[0];
        string timerValue = timerParts[1];
        bool cancelActivity = configParts.Length > 1 && configParts[1].Equals("Cancel=true", StringComparison.OrdinalIgnoreCase);

        logger.LogDebug("Timer details - Type: {TimerType}, Value: {TimerValue}, AttachedTo: {AttachedTo}, CancelActivity: {Cancel}", 
                        timerType, timerValue, subscription.AttachedToActivityId ?? "None", cancelActivity);

        bool reschedule = false;
        DateTime? nextDueTime = null;
        bool isFinalRepetition = false;
        int remainingRepetitions = -1; // -1 for infinite or not applicable

        if (timerType == "TimeCycle")
        {
            logger.LogDebug("Processing TimeCycle for Subscription {SubscriptionId}", subscription.Id);
            // Example: R5/PT1M or R/PT1M
            Match cycleMatch = Regex.Match(timerValue, @"^R(\d*)\/(.+)$");
            if (cycleMatch.Success)
            {               
                string repetitionsStr = cycleMatch.Groups[1].Value;
                string intervalStr = cycleMatch.Groups[2].Value;
                Duration interval;

                try
                {
                    interval = DurationPattern.Roundtrip.Parse(intervalStr).Value;
                    if (string.IsNullOrEmpty(repetitionsStr)) // Infinite repetitions (R/...) 
                    {                       
                        reschedule = true;
                        remainingRepetitions = -1; // Mark as infinite
                        logger.LogDebug("Infinite repetition cycle detected.");
                    }
                    else if (int.TryParse(repetitionsStr, out int repetitions) && repetitions > 0)
                    {
                        remainingRepetitions = repetitions;
                        if (repetitions > 1)
                        {
                            reschedule = true;
                            int nextRepetitionCount = repetitions - 1;
                            string nextConfigurationValue = $"R{nextRepetitionCount}/{intervalStr}";
                            // Update Configuration with decremented count
                            subscription.SetConfiguration($"{timerType}:{nextConfigurationValue}" + (configParts.Length > 1 ? $";{configParts[1]}" : ""));
                            logger.LogInformation("Decremented repetition count for Subscription {SubscriptionId}. New config value starts with: {NewValue}", 
                                                subscription.Id, nextConfigurationValue);
                            // Mark subscription for update
                            await subscriptionRepo.UpdateAsync(subscription, cancellationToken); 
                        }
                        else // Last repetition (repetitions == 1)
                        {
                            reschedule = false;
                            isFinalRepetition = true;
                            logger.LogInformation("Final repetition for Subscription {SubscriptionId}. Timer will not be rescheduled.", subscription.Id);
                            // Optionally delete the subscription after processing? Or let it stay as completed?
                        }
                    }
                    else { logger.LogError("Invalid repetition format in TimeCycle: {Repetitions}", repetitionsStr); }
                    
                    if (reschedule)
                    {
                        // Calculate next due time
                        Instant now = SystemClock.Instance.GetCurrentInstant();
                        nextDueTime = (now + interval).ToDateTimeUtc();
                        logger.LogDebug("Calculated next due time: {NextDueTimeUTC}", nextDueTime);
                    }
                }
                catch (Exception ex)
                { 
                    logger.LogError(ex, "Failed to parse TimeCycle interval '{Interval}' or handle repetitions for Subscription {SubscriptionId}", intervalStr, subscription.Id);
                    // Don't reschedule if parsing fails
                    reschedule = false; 
                }
            }
            else { logger.LogError("Invalid TimeCycle format: {TimerValue}", timerValue); }
        }
        else if (timerType == "TimeDuration" || timerType == "TimeDate")
        { 
            // One-time timers, do not reschedule
            reschedule = false;
             logger.LogDebug("One-time timer ({TimerType}) fired. No reschedule needed.", timerType);
        }
        else
        {            
            logger.LogWarning("Unhandled timer type '{TimerType}' in subscription {SubscriptionId}", timerType, subscription.Id);
        }

        // Trigger the execution flow manager
        // This should handle finding the correct execution and triggering the boundary event logic etc.
        logger.LogInformation("Triggering ExecutionFlowManager for timer event. Subscription: {SubscriptionId}, Execution: {ExecutionId}, Activity: {ActivityId}",
                            subscription.Id, subscription.ExecutionId, subscription.ActivityId);
                           
        // We need a way to tell the ExecutionFlowManager that a timer event occurred for a specific execution/activity.
        // Maybe a dedicated method or a specific signal type?
        // Option: Add method like `TriggerTimerEventAsync(Guid executionId, string boundaryEventActivityId)` to IExecutionFlowManager
        // await executionFlowManager.TriggerTimerEventAsync(subscription.ExecutionId.Value, subscription.ActivityId, cancelActivity, cancellationToken); 
        // For now, let's assume ContinueExecutionAsync might implicitly handle finding subscriptions? Less ideal.
        // Or raise a different MediatR notification that ExecutionFlowManager listens to?
        // Let's stick with the direct call for now, assuming the method exists or will be added.
        // *** This part requires IExecutionFlowManager to have a suitable method ***
        try 
        { 
            // TODO: Define and implement the actual method on IExecutionFlowManager 
            // Example placeholder call:
            // await executionFlowManager.TriggerBoundaryTimerEventAsync(subscription.ExecutionId.Value, subscription.ActivityId, cancelActivity, cancellationToken);
             logger.LogWarning("Actual triggering of execution flow for Timer Event is NOT IMPLEMENTED. Need method on IExecutionFlowManager.");
        }
        catch(Exception ex) 
        { 
            logger.LogError(ex, "Error triggering execution flow manager for timer subscription {SubscriptionId}", subscription.Id);
        }

        // Reschedule if needed AFTER processing the current event and saving state.
        if (reschedule && nextDueTime.HasValue)
        {
            try
            {
                 // Delete old job? Maybe Hangfire handles this if ID is stable? Or use RecurringJob?
                 // If JobId exists, try to delete it first.
                 if (!string.IsNullOrEmpty(subscription.JobId)) {
                      await timerJobScheduler.DeleteTimerJobAsync(subscription.JobId);
                      logger.LogDebug("Deleted previous timer job {JobId}", subscription.JobId);
                 }

                string newJobId = await timerJobScheduler.ScheduleTimerJobAsync(subscription.Id, nextDueTime.Value);
                subscription.JobId = newJobId; 
                // Update subscription again to store the new JobId
                await subscriptionRepo.UpdateAsync(subscription, cancellationToken);
                logger.LogInformation("Rescheduled timer job {NewJobId} for Subscription {SubscriptionId} at {NextDueTimeUTC}", 
                                    newJobId, subscription.Id, nextDueTime.Value);
            }
            catch (Exception ex)
            {               
                logger.LogError(ex, "Failed to reschedule timer job for Subscription {SubscriptionId}", subscription.Id);
                // What should happen if rescheduling fails? Incident?
            }
        }
        else if(isFinalRepetition)
        { 
            // Final repetition completed. Delete the Hangfire job if it exists.
            if (!string.IsNullOrEmpty(subscription.JobId)) {
                 try {
                      await timerJobScheduler.DeleteTimerJobAsync(subscription.JobId);
                      logger.LogInformation("Deleted final timer job {JobId} for completed cycle subscription {SubscriptionId}", subscription.JobId, subscription.Id);
                      subscription.JobId = null; // Clear JobId
                      await subscriptionRepo.UpdateAsync(subscription, cancellationToken);
                 }
                 catch (Exception ex) {
                      logger.LogError(ex, "Failed to delete final timer job {JobId} for subscription {SubscriptionId}", subscription.JobId, subscription.Id);
                 }
            }
        }

        // Save changes from subscription update (new config, new JobId, or cleared JobId)
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogDebug("Finished handling TimerFiredNotification for Subscription {SubscriptionId}. SaveChanges called.", notification.EventSubscriptionId);
    }
} 