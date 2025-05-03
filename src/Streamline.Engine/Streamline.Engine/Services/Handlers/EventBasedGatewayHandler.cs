using Microsoft.Extensions.Logging;
using Streamline.Domain.Schema.Gateways; // For EventBasedGateway
using Streamline.Domain.Schema.Common;
using Streamline.Engine.Abstractions;

namespace Streamline.Engine.Services.Handlers;

/// <summary>
/// Placeholder handler for Event-Based Gateways.
/// </summary>
public class EventBasedGatewayHandler(ILogger<EventBasedGatewayHandler> logger) : IFlowNodeHandler<EventBasedGateway>
{
    public async Task ExecuteAsync(FlowNode node, FlowNodeHandlerContext context, CancellationToken cancellationToken)
    {
        if (node is not EventBasedGateway gateway)
        {
            logger.LogError("Node type mismatch. Expected EventBasedGateway but got {NodeType} for Node {NodeId}", node.GetType().Name, node.Id);
            context.Execution.Fail("Incorrect node type passed to EventBasedGatewayHandler");
            await context.UnitOfWork.SaveChangesAsync(cancellationToken);
            return;
        }

        logger.LogInformation("Executing EventBasedGateway handler for Node {NodeId} ({NodeName}) - Execution {ExecutionId}", 
            gateway.Id, gateway.Name ?? "Unnamed EventBasedGateway", context.Execution.Id);
        
        // TODO: Implement Event-Based Gateway logic (Create event subscriptions for outgoing paths, wait for first event)
        logger.LogWarning("EventBasedGateway {NodeId} handler is not fully implemented. Process will currently wait here indefinitely.", gateway.Id);

        // DO NOT call ContinueExecutionAsync here. Event-Based Gateways wait for an event trigger.
        context.Execution.ArriveAtConvergingGateway(gateway.Id); // Use Arrive to signify waiting state
        await context.UnitOfWork.SaveChangesAsync(cancellationToken);

        // Placeholder: Log that we should be creating subscriptions
        logger.LogDebug("Placeholder EventBasedGateway {NodeId}: Should create event subscriptions for outgoing paths.", gateway.Id);
    }
} 