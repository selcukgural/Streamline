using Microsoft.Extensions.Logging;
using Streamline.Domain.Runtime;
using Streamline.Domain.Schema.Common;
using BpmnTask = Streamline.Domain.Schema.Activities.Task;
// FlowNode için
using Streamline.Engine.Abstractions;    // For IFlowNodeHandler, FlowNodeHandlerContext

namespace Streamline.Engine.Services.Handlers;

/// <summary>
/// Base handler for BPMN Tasks. Creates an ActivityInstance and waits.
/// Specific task handlers (UserTask, ServiceTask) can inherit or be used instead.
/// </summary>
public class TaskHandler(ILogger<TaskHandler> logger) : IFlowNodeHandler<BpmnTask>
{
    // IFlowNodeHandler arayüzünden gelen ExecuteAsync metodu
    public virtual async Task ExecuteAsync(FlowNode node, FlowNodeHandlerContext context, CancellationToken cancellationToken)
    {
        // Gelen node'u doğru tipe cast et
        if (node is not BpmnTask task)
        {
            logger.LogError("Node type mismatch. Expected Task but got {NodeType} for Node {NodeId}", node.GetType().Name, node.Id);
            return;
        }

        logger.LogInformation("Executing Task {TaskId} ({TaskName}) for Execution {ExecutionId}", task.Id, task.Name, context.Execution.Id);

        // Create a new ActivityInstance using constructor and setting required navigation property
        var activityInstance = new ActivityInstance(
            processInstanceId: context.Execution.ProcessInstanceId,
            flowNodeId: task.Id,
            flowNodeName: task.Name
        )
        {
            // Explicitly set the required navigation property
            ProcessInstance = context.Execution.ProcessInstance
        };

        // Add the ActivityInstance to the ProcessInstance's collection
        context.Execution.ProcessInstance.AddActivityInstance(activityInstance);

        logger.LogInformation("Created Activity Instance {ActivityInstanceId} for Task {TaskId}", activityInstance.Id, task.Id);

        // Ensure the execution is marked as being at this task node
        // context.Execution.CurrentFlowNodeId is already set by ExecutionFlowManager before calling the handler.
        // context.Execution.CurrentFlowNodeId = task.Id;

        // Save the new ActivityInstance and the updated Execution state
        await context.UnitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Execution {ExecutionId} is now waiting at Task {TaskId}", context.Execution.Id, task.Id);

        // DO NOT call ContinueAsync here. The execution waits at the task
        // until an external trigger (e.g., task completion) signals it to continue.
    }
}