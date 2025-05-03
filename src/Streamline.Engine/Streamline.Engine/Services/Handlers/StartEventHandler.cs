using Microsoft.Extensions.Logging;
using Streamline.Domain.Schema.Common;
using Streamline.Domain.Schema.Events;
using Streamline.Engine.Abstractions;

namespace Streamline.Engine.Services.Handlers;

public class StartEventHandler(ILogger<StartEventHandler> logger) : IFlowNodeHandler<StartEvent>
{
    public async Task ExecuteAsync(FlowNode node, FlowNodeHandlerContext context, CancellationToken cancellationToken)
    {
        if (node is not StartEvent startEvent)
        {
            logger.LogError("Node type mismatch. Expected StartEvent but got {NodeType} for Node {NodeId}", node.GetType().Name, node.Id);
            return;
        }

        logger.LogInformation("Executing Start Event {StartEventId} for Execution {ExecutionId}", startEvent.Id, context.Execution.Id);

        var activityInstance = context.Execution.ProcessInstance.ActivityInstances
                                .FirstOrDefault(ai => ai.FlowNodeId == startEvent.Id && ai.EndTime == null);

        if (activityInstance == null)
        {
            logger.LogDebug("No existing ActivityInstance found for Start Event {StartEventId} (expected for newly created execution). Proceeding.", startEvent.Id);
        }
        else
        {
            activityInstance.Complete();
            logger.LogInformation("Completed existing Activity Instance {ActivityInstanceId} for Start Event {StartEventId}", activityInstance.Id, startEvent.Id);
        }

        await context.ExecutionFlowManager.ContinueExecutionAsync(context.Execution.Id, cancellationToken);

        logger.LogInformation("Signaled continuation via ExecutionFlowManager for Execution {ExecutionId} from Start Event {StartEventId}", context.Execution.Id, startEvent.Id);

        await context.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
} 