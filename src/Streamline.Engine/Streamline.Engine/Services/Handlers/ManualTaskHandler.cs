using Microsoft.Extensions.Logging;
using Streamline.Domain.Schema.Activities; // For ManualTask
using Streamline.Domain.Schema.Common;
using Streamline.Engine.Abstractions;
using Task = System.Threading.Tasks.Task;

namespace Streamline.Engine.Services.Handlers;

/// <summary>
/// Placeholder handler for Manual Tasks.
/// </summary>
public class ManualTaskHandler(ILogger<ManualTaskHandler> logger) : IFlowNodeHandler<ManualTask>
{
    public async Task ExecuteAsync(FlowNode node, FlowNodeHandlerContext context, CancellationToken cancellationToken)
    {
        if (node is not ManualTask manualTask)
        {
            logger.LogError("Node type mismatch. Expected ManualTask but got {NodeType} for Node {NodeId}",
                node.GetType().Name, node.Id);
            context.Execution.Fail("Incorrect node type passed to ManualTaskHandler");
            await context.UnitOfWork.SaveChangesAsync(cancellationToken);
            return;
        }

        logger.LogInformation("Executing ManualTask handler for Node {NodeId} ({NodeName}) - Execution {ExecutionId}",
            manualTask.Id, manualTask.Name ?? "Unnamed ManualTask", context.Execution.Id);

        // TODO: Implement Manual Task logic (Waiting for external completion signal)
        logger.LogWarning(
            "ManualTask {NodeId} handler is not fully implemented. Process will currently wait here indefinitely.",
            manualTask.Id);

        // DO NOT call ContinueExecutionAsync here. Manual Tasks wait for external completion.
        // Create ActivityInstance to signify waiting state
        var activityInstance = context.Execution.ProcessInstance.ActivityInstances
            .FirstOrDefault(ai =>
                ai.ExecutionId == context.Execution.Id && ai.FlowNodeId == manualTask.Id && ai.EndTime == null);

        if (activityInstance == null)
        {
            var taskActivity = new Domain.Runtime.ActivityInstance(
                context.Execution.ProcessInstanceId,
                manualTask.Id,
                context.Execution.Id,
                manualTask.Name
            );
            taskActivity.ProcessInstance = context.Execution.ProcessInstance;
            context.Execution.ProcessInstance.AddActivityInstance(taskActivity);
            logger.LogInformation("Created placeholder ActivityInstance {ActivityInstanceId} for ManualTask {NodeId}",
                taskActivity.Id, manualTask.Id);
            await context.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
        else
        {
            logger.LogDebug("Found existing active ActivityInstance {ActivityInstanceId} for ManualTask {NodeId}",
                activityInstance.Id, manualTask.Id);
        }
    }
}