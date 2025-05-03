using Microsoft.Extensions.Logging;
using Streamline.Domain.Schema.Gateways; // For InclusiveGateway
using Streamline.Domain.Schema.Common;
using Streamline.Engine.Abstractions;

namespace Streamline.Engine.Services.Handlers;

/// <summary>
/// Placeholder handler for Inclusive Gateways.
/// </summary>
public class InclusiveGatewayHandler(ILogger<InclusiveGatewayHandler> logger) : IFlowNodeHandler<InclusiveGateway>
{
    public async Task ExecuteAsync(FlowNode node, FlowNodeHandlerContext context, CancellationToken cancellationToken)
    {
        if (node is not InclusiveGateway gateway)
        {
            logger.LogError("Node type mismatch. Expected InclusiveGateway but got {NodeType} for Node {NodeId}", node.GetType().Name, node.Id);
            context.Execution.Fail("Incorrect node type passed to InclusiveGatewayHandler");
            await context.UnitOfWork.SaveChangesAsync(cancellationToken);
            return;
        }

        logger.LogInformation("Executing InclusiveGateway handler for Node {NodeId} ({NodeName}) - Execution {ExecutionId}", 
            gateway.Id, gateway.Name ?? "Unnamed InclusiveGateway", context.Execution.Id);
        
        // TODO: Implement Inclusive Gateway logic (Diverging: Evaluate conditions, fork. Converging: Wait for all active incoming flows)
        logger.LogWarning("InclusiveGateway {NodeId} handler is not fully implemented.", gateway.Id);

        // Placeholder: Assume diverging for now and take ALL outgoing flows (like parallel)
        // This is INCORRECT for inclusive converging, and needs proper implementation.
        var isLikelyDiverging = (gateway.Incoming?.Count ?? 0) <= 1 && (gateway.Outgoing?.Count ?? 0) > 1;
        if(isLikelyDiverging)
        {
            context.Execution.Terminate(); // Terminate incoming
            await context.UnitOfWork.SaveChangesAsync(cancellationToken);
            var outgoingFlowDefinitions = context.Definitions.FindSequenceFlows(gateway.Outgoing.Select(q => q.Name).ToList());
            if(outgoingFlowDefinitions != null)
            {
                 var forkTasks = new List<Task>();
                foreach (var flow in outgoingFlowDefinitions)
                {
                     logger.LogDebug("Placeholder InclusiveGateway {GatewayId}: Forking for flow {FlowId}", gateway.Id, flow.Id);
                     // Incorrect logic for inclusive, just mimicking parallel for placeholder
                     forkTasks.Add(context.ExecutionFlowManager.ForkAndContinueExecutionAsync(context.Execution, context.Definitions, flow, cancellationToken));
                }
                 await Task.WhenAll(forkTasks);
            }
        }
        else // Assume converging or error - Placeholder just stops
        {
             logger.LogWarning("Placeholder InclusiveGateway {NodeId}: Converging logic not implemented. Execution {ExecutionId} stopped.", gateway.Id, context.Execution.Id);
             context.Execution.ArriveAtConvergingGateway(gateway.Id); // Mark as waiting
             await context.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        // logger.LogDebug("Placeholder InclusiveGateway {NodeId} completed, continuing execution {ExecutionId}.", gateway.Id, context.Execution.Id);
        // await context.ExecutionFlowManager.ContinueExecutionAsync(context.Execution.Id, cancellationToken); // Incorrect for gateways
    }
} 