using MediatR;
using Microsoft.Extensions.Logging;
using Streamline.Domain.Schema.Events;
using Streamline.Domain.Schema.Common;
using Streamline.Engine.Abstractions;
// For Process type
using Streamline.Domain.Events; // Assuming notifications will be here

namespace Streamline.Engine.Services.Handlers;

/// <summary>
/// Handles Intermediate Throw Events based on their event definition.
/// Publishes notifications for events like Signal.
/// </summary>
public class IntermediateThrowEventHandler(
    ILogger<IntermediateThrowEventHandler> logger,
    IMediator mediator // Injected IMediator
    ) : IFlowNodeHandler<IntermediateThrowEvent>
{
    public async Task ExecuteAsync(FlowNode node, FlowNodeHandlerContext context, CancellationToken cancellationToken)
    {
        if (node is not IntermediateThrowEvent throwEvent)
        {
            logger.LogError("Node type mismatch. Expected IntermediateThrowEvent but got {NodeType} for Node {NodeId}", node.GetType().Name, node.Id);
            context.Execution.Fail("Incorrect node type passed to IntermediateThrowEventHandler");
            await context.UnitOfWork.SaveChangesAsync(cancellationToken);
            return;
        }

        logger.LogInformation("Executing IntermediateThrowEvent handler for Node {NodeId} ({NodeName}) - Execution {ExecutionId}", 
            throwEvent.Id, throwEvent.Name ?? "Unnamed IntermediateThrowEvent", context.Execution.Id);

        // Determine the type of throw event based on the first event definition found
        var eventDefinition = throwEvent.EventDefinition.FirstOrDefault();

        var continueExecution = true; // Assume normal continuation unless Link event changes it

        switch (eventDefinition)
        {
            case LinkEventDefinition linkDef:
                logger.LogInformation("IntermediateThrowEvent {NodeId} is a Link Event targeting '{LinkName}'. Attempting to find matching catch event.", throwEvent.Id, linkDef.Name);
                continueExecution = await HandleLinkEventAsync(throwEvent, linkDef, context, cancellationToken);
                break;

            case SignalEventDefinition signalDef:
                var signalName = signalDef.SignalRef?.Name;
                if (string.IsNullOrWhiteSpace(signalName))
                {
                    logger.LogError("SignalEventDefinition for Throw Event {NodeId} is missing required Signal name reference.", throwEvent.Id);
                    context.Execution.Fail("Signal Throw Event is missing Signal name.");
                    await context.UnitOfWork.SaveChangesAsync(cancellationToken);
                    continueExecution = false; // Failed, do not continue
                    break;
                }
                
                logger.LogInformation("IntermediateThrowEvent {NodeId} is broadcasting Signal '{SignalName}'.", throwEvent.Id, signalName);
                
                // Publish notification for the signal broadcast
                var notification = new SignalBroadcastedNotification(signalName, context.Execution.ProcessInstanceId);
                await mediator.Publish(notification, cancellationToken);
                logger.LogDebug("Published SignalBroadcastedNotification for Signal '{SignalName}'.", signalName);
                
                // After broadcasting, normal execution continues for the throw event itself
                break;

            case MessageEventDefinition messageDef:
                var messageName = messageDef.MessageRef?.Name;
                if (string.IsNullOrWhiteSpace(messageName))
                {
                    logger.LogError("MessageEventDefinition for Throw Event {NodeId} is missing required Message name reference.", throwEvent.Id);
                    context.Execution.Fail("Message Throw Event is missing Message name.");
                    await context.UnitOfWork.SaveChangesAsync(cancellationToken);
                    continueExecution = false; // Failed, do not continue
                    break;
                }
                
                logger.LogInformation("IntermediateThrowEvent {NodeId} is broadcasting Message '{MessageName}'.", throwEvent.Id, messageName);
                
                // TODO: Gather variables/payload to include in the message/notification?
                // var messagePayload = ...;
                
                // Publish notification for the message broadcast
                var notificationMessage =
                    new MessageBroadcastedNotification(messageName, context.Execution.ProcessInstanceId);
                await mediator.Publish(notificationMessage, cancellationToken);
                logger.LogDebug("Published MessageBroadcastedNotification for Message '{MessageName}'.", messageName);
                
                // After broadcasting, normal execution continues for the throw event itself
                break;

            case EscalationEventDefinition escalationDef:
                 var escalationCode = escalationDef.EscalationRef?.Name ?? "(Unnamed Escalation)";
                logger.LogInformation("IntermediateThrowEvent {NodeId} is an Escalation Event with code '{EscalationCode}'.", throwEvent.Id, escalationCode);
                // TODO: Implement escalation propagation logic
                logger.LogWarning("Escalation handling for Escalation Event {NodeId} is not yet implemented.", throwEvent.Id);
                break;

             case CompensateEventDefinition compensateDef:
                logger.LogInformation("IntermediateThrowEvent {NodeId} is a Compensation Event.", throwEvent.Id);
                // TODO: Implement compensation triggering logic
                logger.LogWarning("Compensation handling for Compensation Event {NodeId} is not yet implemented.", throwEvent.Id);
                break;
                
            case null: // Equivalent to None Intermediate Throw Event
                logger.LogInformation("IntermediateThrowEvent {NodeId} is a None Event. Continuing execution.", throwEvent.Id);
                // No specific action, just continue
                break;

            default:
                logger.LogWarning("IntermediateThrowEvent {NodeId} has an unhandled event definition type: {DefinitionType}. Continuing execution as if it were None.", 
                                throwEvent.Id, eventDefinition.GetType().Name);
                // Treat unknown types like None for now
                break;
        }

        // Continue the execution flow unless handled differently (e.g., by Link event)
        if (continueExecution)
        {
            logger.LogDebug("IntermediateThrowEvent {NodeId} handler finished processing, continuing execution {ExecutionId}.", throwEvent.Id, context.Execution.Id);
            await context.ExecutionFlowManager.ContinueExecutionAsync(context.Execution.Id, cancellationToken);
        }
    }

    private async Task<bool> HandleLinkEventAsync(IntermediateThrowEvent throwLinkEvent, LinkEventDefinition linkDef, FlowNodeHandlerContext context, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(linkDef.Name))
        {
            logger.LogError("LinkEventDefinition for Throw Event {NodeId} is missing the required 'name' attribute.", throwLinkEvent.Id);
            context.Execution.Fail("Link Throw Event is missing target link name.");
            await context.UnitOfWork.SaveChangesAsync(cancellationToken);
            return false; // Do not continue
        }

        // Find the corresponding Catch Link Event in the *same* process definition
        var process = context.Definitions?.RootElement.OfType<Process>().FirstOrDefault(); 
        if (process == null) {
            logger.LogError("Could not find Process element in definitions for Link Event {NodeId}.", throwLinkEvent.Id);
            context.Execution.Fail("Process definition not found for Link Event.");
            await context.UnitOfWork.SaveChangesAsync(cancellationToken);
            return false;
        }

        var catchLinkEvent = process.FlowElement
                                   .OfType<IntermediateCatchEvent>()
                                   .FirstOrDefault(ice => ice.EventDefinition.OfType<LinkEventDefinition>().Any(ld => ld.Name == linkDef.Name));

        if (catchLinkEvent == null)
        {            
            logger.LogError("Could not find matching Intermediate Catch Link Event with name '{LinkName}' for Throw Event {NodeId} in process {ProcessId}.", 
                           linkDef.Name, throwLinkEvent.Id, process.Id);
            context.Execution.Fail($"Matching Catch Link Event '{linkDef.Name}' not found.");
            await context.UnitOfWork.SaveChangesAsync(cancellationToken);
            return false; // Do not continue
        }

        logger.LogInformation("Found matching Catch Link Event {CatchEventId} for Throw Link Event {ThrowEventId}. Jumping execution {ExecutionId}.", 
                           catchLinkEvent.Id, throwLinkEvent.Id, context.Execution.Id);

        // Update the execution to point to the catch event
        context.Execution.EnterFlowNode(catchLinkEvent.Id);
        await context.UnitOfWork.SaveChangesAsync(cancellationToken); // Save the new position

        // Trigger continuation from the new node (the catch event)
        // The catch event's handler will then execute its logic (which for link is typically just continuing)
        await context.ExecutionFlowManager.ContinueExecutionAsync(context.Execution.Id, cancellationToken);

        return false; // We handled continuation explicitly, so the main loop shouldn't continue from the throw event
    }
} 