using MediatR;
using Microsoft.Extensions.Logging;
using Streamline.Domain.Abstractions;
using Streamline.Domain.Events;
using Streamline.Domain.Runtime;

namespace Streamline.Application.Notifications.Handlers;

/// <summary>
/// Handles the SignalReceivedNotification to trigger the execution waiting on a signal event.
/// </summary>
public class SignalReceivedNotificationHandler(
    IRepository<EventSubscription> subscriptionRepository,
    IRepository<Execution> executionRepository,
    IRepository<ActivityInstance> activityInstanceRepository, // Added ActivityInstance repo
    IExecutionFlowManager executionFlowManager,
    IUnitOfWork unitOfWork,
    ILogger<SignalReceivedNotificationHandler> logger
    // No TimerJobScheduler needed for Signal
    )
    : INotificationHandler<SignalReceivedNotification>
{
    public async Task Handle(SignalReceivedNotification notification, CancellationToken cancellationToken)
    {        
        logger.LogInformation("Handling SignalReceivedNotification for EventSubscription {EventSubscriptionId}", 
                             notification.EventSubscriptionId);

        var subscription = await subscriptionRepository.GetByIdAsync(notification.EventSubscriptionId, cancellationToken);

        if (subscription == null) { 
            logger.LogWarning("EventSubscription {EventSubscriptionId} not found for received signal.", notification.EventSubscriptionId);
            return; 
        }
        
        // Verify it's a signal subscription 
        if (subscription.EventType != "Signal")
        {
             logger.LogWarning("Subscription {SubscriptionId} is not a Signal subscription. Type: {EventType}", subscription.Id, subscription.EventType);
             return;
        }
        
        logger.LogDebug("Processing Signal Subscription {SubscriptionId} for Event Name '{EventName}'", 
                        subscription.Id, subscription.EventName);

        // --- Check if it's a Boundary Event Subscription --- 
        if (!string.IsNullOrEmpty(subscription.AttachedToActivityId))
        {
            logger.LogInformation("Signal received for Boundary Event Subscription {SubscriptionId} attached to Activity {AttachedActivityId}",
                                subscription.Id, subscription.AttachedToActivityId);

            // Parse CancelActivity flag
            bool cancelActivity = false;
            if (subscription.Configuration != null)
            {
                var configParts = subscription.Configuration.Split(';');
                var cancelPart = configParts.FirstOrDefault(p => p.StartsWith("Cancel="));
                if (cancelPart != null && bool.TryParse(cancelPart.Substring("Cancel=".Length), out bool parsedCancel))
                {
                    cancelActivity = parsedCancel;
                }
                else { 
                    logger.LogWarning("Could not parse CancelActivity flag from configuration '{Config}' for Boundary Subscription {SubscriptionId}. Assuming non-interrupting (false).", 
                                    subscription.Configuration, subscription.Id);
                }
            }
            else { 
                 logger.LogWarning("Configuration is null for Boundary Subscription {SubscriptionId}. Cannot determine CancelActivity. Assuming non-interrupting (false).", subscription.Id);
            }
            
            logger.LogInformation("Boundary Event {BoundaryEventId} is {InterruptingStatus}", 
                                subscription.ActivityId, cancelActivity ? "Interrupting" : "Non-interrupting");

            // Find the execution(s)
            var activeExecutionsOnActivity = await executionRepository.ListAsync(
                e => e.ProcessInstanceId == subscription.ProcessInstanceId &&
                     e.CurrentFlowNodeId == subscription.AttachedToActivityId && 
                     e.IsActive, 
                cancellationToken);

            if (!activeExecutionsOnActivity.Any()) { 
                 logger.LogWarning("Boundary Event {BoundaryEventId} triggered, but no active execution found on attached Activity {ActivityId}. Subscription {SubscriptionId} might be obsolete.",
                                   subscription.ActivityId, subscription.AttachedToActivityId, subscription.Id);
                 await subscriptionRepository.DeleteAsync(subscription, cancellationToken);
                 return; 
            }
            
            var targetExecution = activeExecutionsOnActivity.First(); 
            logger.LogDebug("Found {Count} active execution(s) on attached Activity {ActivityId}. Target Execution ID: {TargetExecutionId}",
                            activeExecutionsOnActivity.Count, subscription.AttachedToActivityId, targetExecution.Id);

            if (cancelActivity) // --- Interrupting Logic --- 
            {
                logger.LogInformation("Interrupting Execution {TargetExecutionId} for Activity {ActivityId} due to Signal Boundary Event {BoundaryEventId}", 
                                    targetExecution.Id, subscription.AttachedToActivityId, subscription.ActivityId);
                targetExecution.Terminate("Interrupted by Boundary Event " + subscription.ActivityId);
                
                var activityInstance = await activityInstanceRepository.FirstOrDefaultAsync(
                     ai => ai.ExecutionId == targetExecution.Id && ai.FlowNodeId == subscription.AttachedToActivityId && ai.EndTime == null, 
                     cancellationToken);
                if (activityInstance != null) { 
                    activityInstance.Cancel(); 
                    await activityInstanceRepository.UpdateAsync(activityInstance, cancellationToken);
                    logger.LogDebug("Cancelled ActivityInstance {ActivityInstanceId} for interrupted execution.", activityInstance.Id); 
                } else { 
                     logger.LogWarning("Could not find active ActivityInstance for Execution {TargetExecutionId} on Activity {ActivityId} to cancel.",
                                      targetExecution.Id, subscription.AttachedToActivityId);
                }

                try
                {
                    logger.LogDebug("Starting new execution from Interrupting Signal Boundary Event {BoundaryEventId}", subscription.ActivityId);
                    await executionFlowManager.StartExecutionAtNodeAsync(subscription.ProcessInstanceId, subscription.ActivityId, targetExecution.Id, cancellationToken);
                }
                catch (Exception startEx) { 
                     logger.LogError(startEx, "Failed to start new execution from Interrupting Signal Boundary Event {BoundaryEventId}", subscription.ActivityId);
                }
            }
            else // --- Non-interrupting Logic --- 
            {
                 logger.LogInformation("Non-interrupting Signal Boundary Event {BoundaryEventId} triggered for Activity {ActivityId}. Target Execution {TargetExecutionId} continues.",
                                     subscription.ActivityId, subscription.AttachedToActivityId, targetExecution.Id);
                 try
                 {
                    logger.LogDebug("Starting new execution from Non-Interrupting Signal Boundary Event {BoundaryEventId}", subscription.ActivityId);
                    await executionFlowManager.StartExecutionAtNodeAsync(subscription.ProcessInstanceId, subscription.ActivityId, targetExecution.Id, cancellationToken); 
                 }
                 catch (Exception startEx) { 
                     logger.LogError(startEx, "Failed to start new execution from Non-interrupting Signal Boundary Event {BoundaryEventId}", subscription.ActivityId);
                 }
            }

            // Delete the consumed signal subscription
            logger.LogDebug("Deleting Signal Boundary Event Subscription {SubscriptionId}", subscription.Id);
            await subscriptionRepository.DeleteAsync(subscription, cancellationToken);
        }
        else // --- Handle Regular Intermediate Catch Event Signal --- 
        {            
            logger.LogDebug("Signal received for Intermediate Catch Event Subscription {SubscriptionId}", subscription.Id);
            var execution = subscription.Execution ?? await executionRepository.GetByIdAsync(subscription.ExecutionId, cancellationToken);

            if (execution == null || execution.IsActive) { 
                 logger.LogWarning("Subscription {SubscriptionId} points to a null or already active execution {ExecutionId}. Cannot trigger signal continuation.", 
                                 subscription.Id, subscription.ExecutionId);
                 await subscriptionRepository.DeleteAsync(subscription, cancellationToken);
                 return; 
            }

            logger.LogInformation("Triggering Execution {ExecutionId} waiting at Node {WaitingNodeId} via Signal Subscription {SubscriptionId}",
                             execution.Id, execution.WaitingAtGatewayId ?? subscription.ActivityId, subscription.Id);

            // 1. Reactivate the waiting execution
            execution.Reactivate();
            execution.LeaveConvergingGateway();

            // 2. Delete the consumed signal subscription
            logger.LogDebug("Deleting Intermediate Signal Event Subscription {SubscriptionId}", subscription.Id);
            await subscriptionRepository.DeleteAsync(subscription, cancellationToken);

            // 3. Trigger the flow manager for the reactivated execution
            try
            {
                await unitOfWork.SaveChangesAsync(cancellationToken); // Save execution reactivation first
                await executionFlowManager.ContinueExecutionAsync(execution.Id, cancellationToken);
                logger.LogDebug("Continuation triggered for Execution {ExecutionId} after signal for Subscription {SubscriptionId}.", 
                                execution.Id, subscription.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error continuing Execution {ExecutionId} after signal trigger for Subscription {SubscriptionId}. Execution might be stuck.", 
                              execution.Id, subscription.Id);
                execution.Fail("Continuation failed after signal trigger", ex.Message);
                // SaveChangesAsync will be called at the end
            }
        }
        
        // Final SaveChanges 
        await unitOfWork.SaveChangesAsync(cancellationToken); 
    }
} 