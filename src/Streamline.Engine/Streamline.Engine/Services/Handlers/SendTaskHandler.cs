using Microsoft.Extensions.Logging;
using Streamline.Domain.Schema.Activities; // For SendTask
using Streamline.Domain.Schema.Common;
using Streamline.Engine.Abstractions;
using Task = System.Threading.Tasks.Task;

namespace Streamline.Engine.Services.Handlers;

/// <summary>
/// Placeholder handler for Send Tasks.
/// </summary>
public class SendTaskHandler(ILogger<SendTaskHandler> logger) : IFlowNodeHandler<SendTask>
{
    public async Task ExecuteAsync(FlowNode node, FlowNodeHandlerContext context, CancellationToken cancellationToken)
    {
        if (node is not SendTask sendTask)
        {
            logger.LogError("Node type mismatch. Expected SendTask but got {NodeType} for Node {NodeId}", node.GetType().Name, node.Id);
            context.Execution.Fail("Incorrect node type passed to SendTaskHandler");
            await context.UnitOfWork.SaveChangesAsync(cancellationToken);
            return;
        }

        logger.LogInformation("Executing SendTask handler for Node {NodeId} ({NodeName}) - Execution {ExecutionId}", 
            sendTask.Id, sendTask.Name ?? "Unnamed SendTask", context.Execution.Id);
        
        // TODO: Implement Send Task logic (e.g., sending a message)
        logger.LogWarning("SendTask {NodeId} handler is not fully implemented.", sendTask.Id);

        // For placeholder, assume immediate completion and continue
        logger.LogDebug("Placeholder SendTask {NodeId} completed, continuing execution {ExecutionId}.", sendTask.Id, context.Execution.Id);
        await context.ExecutionFlowManager.ContinueExecutionAsync(context.Execution.Id, cancellationToken);
    }
} 