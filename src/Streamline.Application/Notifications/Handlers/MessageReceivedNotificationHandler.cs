using MediatR;
using Microsoft.Extensions.Logging;
using Streamline.Domain.Abstractions;
using Streamline.Domain.Events;
using Streamline.Domain.Runtime;

namespace Streamline.Application.Notifications.Handlers;

/// <summary>
/// Handles the MessageReceivedNotification to trigger the execution waiting on a message event.
/// </summary>
public class MessageReceivedNotificationHandler(
    IRepository<EventSubscription> subscriptionRepository,
    IRepository<Execution> executionRepository,
    IRepository<ActivityInstance> activityInstanceRepository, // Added ActivityInstance repo
    IExecutionFlowManager executionFlowManager,
    IUnitOfWork unitOfWork,
    ILogger<MessageReceivedNotificationHandler> logger
    // No TimerJobScheduler needed for Message
    )
    : INotificationHandler<MessageReceivedNotification>
{
    public async Task Handle(MessageReceivedNotification notification, CancellationToken cancellationToken)
    {        
        logger.LogInformation("Handling MessageReceivedNotification for EventSubscription {EventSubscriptionId}. CorrelationKey: {CorrelationKey}", 
                             notification.EventSubscriptionId, notification.CorrelationKey ?? "N/A");

        var subscription = await subscriptionRepository.GetByIdAsync(notification.EventSubscriptionId, cancellationToken);

        if (subscription == null) { 
            logger.LogWarning("EventSubscription {EventSubscriptionId} not found for received message.", notification.EventSubscriptionId);
            return; 
        }
        
        // Verify it's a message subscription 
        if (subscription.EventType != "Message")
        {
             logger.LogWarning("Subscription {SubscriptionId} is not a Message subscription. Type: {EventType}", subscription.Id, subscription.EventType);
             return;
        }
        
        logger.LogDebug("Processing Message Subscription {SubscriptionId} for Event Name '{EventName}'", 
                        subscription.Id, subscription.EventName);

        // --- Check if it's a Boundary Event Subscription --- 
        if (!string.IsNullOrEmpty(subscription.AttachedToActivityId))
        {
            logger.LogInformation("Message received for Boundary Event Subscription {SubscriptionId} attached to Activity {AttachedActivityId}",
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
                 // No JobId for message events typically
                 return; 
            }
            
            var targetExecution = activeExecutionsOnActivity.First(); 
            logger.LogDebug("Found {Count} active execution(s) on attached Activity {ActivityId}. Target Execution ID: {TargetExecutionId}",
                            activeExecutionsOnActivity.Count, subscription.AttachedToActivityId, targetExecution.Id);

            if (cancelActivity) // --- Interrupting Logic --- 
            {
                logger.LogInformation("Interrupting Execution {TargetExecutionId} for Activity {ActivityId} due to Message Boundary Event {BoundaryEventId}", 
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
                    logger.LogDebug("Starting new execution from Interrupting Message Boundary Event {BoundaryEventId}", subscription.ActivityId);
                    await executionFlowManager.StartExecutionAtNodeAsync(subscription.ProcessInstanceId, subscription.ActivityId, targetExecution.Id, cancellationToken);
                }
                catch (Exception startEx) { 
                     logger.LogError(startEx, "Failed to start new execution from Interrupting Message Boundary Event {BoundaryEventId}", subscription.ActivityId);
                }
            }
            else // --- Non-interrupting Logic --- 
            {
                 logger.LogInformation("Non-interrupting Message Boundary Event {BoundaryEventId} triggered for Activity {ActivityId}. Target Execution {TargetExecutionId} continues.",
                                     subscription.ActivityId, subscription.AttachedToActivityId, targetExecution.Id);
                 try
                 {
                    logger.LogDebug("Starting new execution from Non-Interrupting Message Boundary Event {BoundaryEventId}", subscription.ActivityId);
                    await executionFlowManager.StartExecutionAtNodeAsync(subscription.ProcessInstanceId, subscription.ActivityId, targetExecution.Id, cancellationToken); 
                 }
                 catch (Exception startEx) { 
                     logger.LogError(startEx, "Failed to start new execution from Non-interrupting Message Boundary Event {BoundaryEventId}", subscription.ActivityId);
                 }
            }

            // Delete the consumed message subscription
            logger.LogDebug("Deleting Message Boundary Event Subscription {SubscriptionId}", subscription.Id);
            await subscriptionRepository.DeleteAsync(subscription, cancellationToken);
        }
        else // --- Handle Regular Intermediate Catch Event Message --- 
        {            
            logger.LogDebug("Message received for Intermediate Catch Event Subscription {SubscriptionId}", subscription.Id);
            var execution = subscription.Execution ?? await executionRepository.GetByIdAsync(subscription.ExecutionId, cancellationToken);

            if (execution == null || execution.IsActive) { 
                 logger.LogWarning("Subscription {SubscriptionId} points to a null or already active execution {ExecutionId}. Cannot trigger message continuation.", 
                                 subscription.Id, subscription.ExecutionId);
                 await subscriptionRepository.DeleteAsync(subscription, cancellationToken);
                 return; 
            }

            logger.LogInformation("Triggering Execution {ExecutionId} waiting at Node {WaitingNodeId} via Message Subscription {SubscriptionId}",
                             execution.Id, execution.WaitingAtGatewayId ?? subscription.ActivityId, subscription.Id);

            // 1. Reactivate the waiting execution
            execution.Reactivate();
            execution.LeaveConvergingGateway();

            // 2. Delete the consumed message subscription
            logger.LogDebug("Deleting Intermediate Message Event Subscription {SubscriptionId}", subscription.Id);
            await subscriptionRepository.DeleteAsync(subscription, cancellationToken);

            // 3. Trigger the flow manager for the reactivated execution
            try
            {
                await unitOfWork.SaveChangesAsync(cancellationToken); // Save execution reactivation first
                await executionFlowManager.ContinueExecutionAsync(execution.Id, cancellationToken);
                logger.LogDebug("Continuation triggered for Execution {ExecutionId} after message for Subscription {SubscriptionId}.", 
                                execution.Id, subscription.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error continuing Execution {ExecutionId} after message trigger for Subscription {SubscriptionId}. Execution might be stuck.", 
                              execution.Id, subscription.Id);
                execution.Fail("Continuation failed after message trigger", ex.Message);
                // SaveChangesAsync will be called at the end
            }
        }
        
        // Final SaveChanges for subscription deletion and potential execution/activity updates
        await unitOfWork.SaveChangesAsync(cancellationToken); 
    }
} 