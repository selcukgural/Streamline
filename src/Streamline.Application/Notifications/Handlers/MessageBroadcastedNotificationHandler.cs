using MediatR;
using Microsoft.Extensions.Logging;
using Streamline.Domain.Abstractions;
using Streamline.Domain.Runtime;
using Streamline.Domain.Events;

namespace Streamline.Application.Notifications.Handlers;

/// <summary>
/// Handles the MessageBroadcastedNotification to find and trigger waiting executions.
/// NOTE: Initial version matches only on message name. Correlation logic needs to be added.
/// </summary>
public class MessageBroadcastedNotificationHandler(
    IRepository<EventSubscription> subscriptionRepository,
    IRepository<Execution> executionRepository,
    IExecutionFlowManager executionFlowManager,
    IUnitOfWork unitOfWork,
    ILogger<MessageBroadcastedNotificationHandler> logger)
    : INotificationHandler<MessageBroadcastedNotification>
{
    public async Task Handle(MessageBroadcastedNotification notification, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Handling MessageBroadcastedNotification for Message '{MessageName}' from ProcessInstance {SourceProcessInstanceId}",
            notification.MessageName, notification.SourceProcessInstanceId);

        // Find active event subscriptions for this message name and type
        // TODO: Implement Correlation Logic! 
        // This needs to compare correlation keys from the message payload (notification) 
        // with correlation keys stored in the EventSubscription (potentially in the Configuration field).
        // The current query ONLY matches on message name.
        // TODO: Add TenantId filtering if applicable
        var matchingSubscriptions = await subscriptionRepository.ListAsync(sub =>
                sub.EventType == "Message" &&
                sub.EventName == notification.MessageName &&
                sub.Execution != null &&
                sub.Execution.IsActive == false &&
                sub.Execution.WaitingAtGatewayId != null,
            cancellationToken);

        if (!matchingSubscriptions.Any())
        {
            logger.LogInformation("No active subscriptions found for message '{MessageName}' (basic name match).",
                notification.MessageName);
            return;
        }

        logger.LogInformation(
            "Found {Count} potential subscriptions for message '{MessageName}' based on name. Triggering matching executions (correlation TBD)...",
            matchingSubscriptions.Count, notification.MessageName);

        // TODO: Once correlation is implemented, this loop might only process ONE correlated subscription.
        foreach (var subscription in matchingSubscriptions)
        {
            var execution = subscription.Execution ??
                            await executionRepository.GetByIdAsync(subscription.ExecutionId, cancellationToken);

            if (execution == null || execution.IsActive)
            {
                logger.LogWarning(
                    "Subscription {SubscriptionId} points to a null or already active execution {ExecutionId}. Skipping.",
                    subscription.Id, subscription.ExecutionId);
                continue;
            }

            logger.LogInformation(
                "Triggering Execution {ExecutionId} waiting at Node {WaitingNodeId} via Subscription {SubscriptionId} (Correlation check pending).",
                execution.Id, execution.WaitingAtGatewayId, subscription.Id);

            // 1. Reactivate the execution
            execution.Reactivate();
            execution.LeaveConvergingGateway();

            // 2. Remove the subscription
            await subscriptionRepository.DeleteAsync(subscription, cancellationToken);

            // 3. Trigger the flow manager
            try
            {
                await unitOfWork.SaveChangesAsync(cancellationToken);
                await executionFlowManager.ContinueExecutionAsync(execution.Id, cancellationToken);
                logger.LogDebug("Continuation triggered for Execution {ExecutionId} after message '{MessageName}'.",
                    execution.Id, notification.MessageName);
            }
            catch (Exception ex)
            {
                logger.LogError(ex,
                    "Error continuing Execution {ExecutionId} after message '{MessageName}'. Execution might be stuck.",
                    execution.Id, notification.MessageName);
                execution.Fail("Continuation failed after message trigger", ex.Message);
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }
    }
}