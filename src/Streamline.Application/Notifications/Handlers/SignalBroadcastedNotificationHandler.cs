using MediatR;
using Microsoft.Extensions.Logging;
using Streamline.Domain.Abstractions;
using Streamline.Domain.Runtime;
// For IExecutionFlowManager
using Streamline.Domain.Events; // Corrected namespace for Notifications

namespace Streamline.Application.Notifications.Handlers;
// Assuming Handlers sub-namespace

/// <summary>
/// Handles the SignalBroadcastedNotification to find and trigger waiting executions.
/// </summary>
public class SignalBroadcastedNotificationHandler(
    IRepository<EventSubscription> subscriptionRepository,
    IRepository<Execution> executionRepository,
    IExecutionFlowManager executionFlowManager,
    IUnitOfWork unitOfWork,
    ILogger<SignalBroadcastedNotificationHandler> logger)
    : INotificationHandler<SignalBroadcastedNotification>
{
    // Needed to load the execution
    // To save changes after handling

    public async Task Handle(SignalBroadcastedNotification notification, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Handling SignalBroadcastedNotification for Signal '{SignalName}' from ProcessInstance {SourceProcessInstanceId}",
            notification.SignalName, notification.SourceProcessInstanceId);

        // Find active event subscriptions for this signal name and type
        // TODO: Add TenantId filtering if applicable
        var matchingSubscriptions = await subscriptionRepository.ListAsync(sub =>
                sub.EventType == "Signal" &&
                sub.EventName == notification.SignalName &&
                sub.Execution != null && // Ensure Execution navigation property is loaded or query based on ExecutionId
                sub.Execution.IsActive == false && // Should be inactive because it's waiting
                sub.Execution.WaitingAtGatewayId != null, // It should be marked as waiting
            cancellationToken);

        if (!matchingSubscriptions.Any())
        {
            logger.LogInformation("No active subscriptions found for signal '{SignalName}'.", notification.SignalName);
            return;
        }

        logger.LogInformation("Found {Count} subscriptions for signal '{SignalName}'. Triggering executions...",
            matchingSubscriptions.Count, notification.SignalName);

        foreach (var subscription in matchingSubscriptions)
        {
            // Ensure execution is properly loaded (might be needed depending on repository implementation)
            var execution = subscription.Execution ??
                            await executionRepository.GetByIdAsync(subscription.ExecutionId, cancellationToken);

            if (execution == null || execution.IsActive)
            {
                logger.LogWarning(
                    "Subscription {SubscriptionId} points to a null or already active execution {ExecutionId}. Skipping.",
                    subscription.Id, subscription.ExecutionId);
                // Optionally, clean up this orphaned subscription?
                // _subscriptionRepository.Delete(subscription);
                continue;
            }

            logger.LogInformation(
                "Triggering Execution {ExecutionId} waiting at Node {WaitingNodeId} via Subscription {SubscriptionId}",
                execution.Id, execution.WaitingAtGatewayId, subscription.Id);

            // 1. Reactivate the execution
            execution.Reactivate();
            execution.LeaveConvergingGateway(); // Clear the waiting state marker

            // 2. Remove the subscription (it's consumed) - Use DeleteAsync
            await subscriptionRepository.DeleteAsync(subscription, cancellationToken);

            // 3. Trigger the flow manager to continue from the catch event node
            try
            {
                // Save changes (including deletion) before triggering continuation
                await unitOfWork.SaveChangesAsync(cancellationToken);
                await executionFlowManager.ContinueExecutionAsync(execution.Id, cancellationToken);
                logger.LogDebug("Continuation triggered for Execution {ExecutionId} after signal '{SignalName}'.",
                    execution.Id, notification.SignalName);
            }
            catch (Exception ex)
            {
                logger.LogError(ex,
                    "Error continuing Execution {ExecutionId} after signal '{SignalName}'. Execution might be stuck.",
                    execution.Id, notification.SignalName);
                // Should we attempt to fail the execution here? Or rely on subsequent checks?
                execution.Fail("Continuation failed after signal trigger", ex.Message);
                await unitOfWork.SaveChangesAsync(cancellationToken); // Attempt to save failure state
            }
        }
    }
}