using Microsoft.Extensions.Logging;
using Streamline.Domain.Schema.Activities; // For ReceiveTask
using Streamline.Domain.Schema.Common;
using Streamline.Engine.Abstractions;
using Task = System.Threading.Tasks.Task;

namespace Streamline.Engine.Services.Handlers;

/// <summary>
/// Placeholder handler for Receive Tasks.
/// </summary>
public class ReceiveTaskHandler(ILogger<ReceiveTaskHandler> logger) : IFlowNodeHandler<ReceiveTask>
{
    public async Task ExecuteAsync(FlowNode node, FlowNodeHandlerContext context, CancellationToken cancellationToken)
    {
        if (node is not ReceiveTask receiveTask)
        {
            logger.LogError("Node type mismatch. Expected ReceiveTask but got {NodeType} for Node {NodeId}",
                node.GetType().Name, node.Id);
            context.Execution.Fail("Incorrect node type passed to ReceiveTaskHandler");
            await context.UnitOfWork.SaveChangesAsync(cancellationToken);
            return;
        }

        logger.LogInformation("Executing ReceiveTask handler for Node {NodeId} ({NodeName}) - Execution {ExecutionId}",
            receiveTask.Id, receiveTask.Name ?? "Unnamed ReceiveTask", context.Execution.Id);

        // TODO: Implement Receive Task logic (Waiting for a message correlation)
        logger.LogWarning(
            "ReceiveTask {NodeId} handler is not fully implemented. Process will currently wait here indefinitely.",
            receiveTask.Id);

        // DO NOT call ContinueExecutionAsync here. Receive Tasks wait for message correlation.
        // Create ActivityInstance to signify waiting state
        var activityInstance = context.Execution.ProcessInstance.ActivityInstances
            .FirstOrDefault(ai =>
                ai.ExecutionId == context.Execution.Id && ai.FlowNodeId == receiveTask.Id && ai.EndTime == null);

        if (activityInstance == null)
        {
            var taskActivity = new Domain.Runtime.ActivityInstance(
                context.Execution.ProcessInstanceId,
                receiveTask.Id,
                context.Execution.Id,
                receiveTask.Name
            );
            taskActivity.ProcessInstance = context.Execution.ProcessInstance;
            context.Execution.ProcessInstance.AddActivityInstance(taskActivity);
            logger.LogInformation("Created placeholder ActivityInstance {ActivityInstanceId} for ReceiveTask {NodeId}",
                taskActivity.Id, receiveTask.Id);
            await context.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
        else
        {
            logger.LogDebug("Found existing active ActivityInstance {ActivityInstanceId} for ReceiveTask {NodeId}",
                activityInstance.Id, receiveTask.Id);
        }
    }
}