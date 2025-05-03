using Microsoft.Extensions.Logging;
using Streamline.Domain.Runtime;
using Streamline.Domain.Schema.Common;
using Streamline.Domain.Schema.Flow;
using Streamline.Domain.Schema.Gateways;
// For ExclusiveGateway
// For SequenceFlow, Expression
// For SequenceFlow namespace
using Streamline.Engine.Abstractions; // For IFlowNodeHandler, FlowNodeHandlerContext, IExecutionFlowManager

namespace Streamline.Engine.Services.Handlers;

public class ExclusiveGatewayHandler(ILogger<ExclusiveGatewayHandler> logger) : IFlowNodeHandler<ExclusiveGateway>
{
    public async Task ExecuteAsync(FlowNode node, FlowNodeHandlerContext context,
        CancellationToken cancellationToken)
    {
        if (node is not ExclusiveGateway gateway)
        {
            logger.LogError("Node type mismatch. Expected ExclusiveGateway but got {NodeType} for Node {NodeId}", node.GetType().Name, node.Id);
            return;
        }

        logger.LogInformation("Executing Exclusive Gateway {GatewayId} for Execution {ExecutionId}", gateway.Id,
            context.Execution.Id);

        var activityInstance = context.Execution.ProcessInstance.ActivityInstances
            .FirstOrDefault(ai => ai.FlowNodeId == gateway.Id && ai.EndTime == null);
        if (activityInstance != null)
        {
            activityInstance.Complete();
            logger.LogInformation("Completed Activity Instance {ActivityInstanceId} for Exclusive Gateway {GatewayId}",
                activityInstance.Id, gateway.Id);
        }

        context.Execution.Terminate();
        await context.UnitOfWork.SaveChangesAsync(cancellationToken);


        SequenceFlow? chosenFlow = null;
        var outgoingFlows =
            context.Definitions.FindSequenceFlows(gateway.Outgoing.Select(q => q.Name).ToList());

        SequenceFlow? defaultFlow = null;
        if (!string.IsNullOrEmpty(gateway.Default))
        {
            defaultFlow = outgoingFlows.FirstOrDefault(f => f.Id == gateway.Default);
        }

        foreach (var flow in
                 outgoingFlows.Where(f =>
                     f.Id != gateway.Default))
        {
            if (flow.ConditionExpression != null && !string.IsNullOrEmpty(flow.ConditionExpression.Id) && flow.ConditionExpression.Text != null && flow.ConditionExpression.Text.Length > 0)
            {
                var conditionMet = EvaluateCondition(flow.ConditionExpression, context.Execution.ProcessInstance);
                var conditionTextForLog = flow.ConditionExpression.Text != null ? string.Join(",", flow.ConditionExpression.Text) : "<null or empty>";
                logger.LogDebug(
                    "Exclusive Gateway {GatewayId}: Evaluating flow {FlowId}. Condition: '{Condition}'. Met: {Result}",
                    gateway.Id, flow.Id, conditionTextForLog,
                    conditionMet);

                if (conditionMet)
                {
                    chosenFlow = flow;
                    break;
                }
            }
        }

        if (chosenFlow == null)
        {
            if (defaultFlow != null)
            {
                logger.LogInformation("Exclusive Gateway {GatewayId}: No condition met, taking default flow {FlowId}",
                    gateway.Id, defaultFlow.Id);
                chosenFlow = defaultFlow;
            }
            else
            {
                logger.LogError(
                    "Exclusive Gateway {GatewayId}: No condition met and no default flow specified. Process cannot continue for terminated execution {ExecutionId}.",
                    gateway.Id, context.Execution.Id);
                return;
            }
        }

        logger.LogInformation("Exclusive Gateway {GatewayId}: Taking outgoing flow {FlowId}", gateway.Id,
            chosenFlow.Id);
        await context.ExecutionFlowManager.ContinueExecutionAsync(context.Execution.Id, cancellationToken);
    }

    private static bool EvaluateCondition(Expression? conditionExpression, ProcessInstance processInstance)
    {
        var expressionText = conditionExpression?.Text.FirstOrDefault();
        var variableName = expressionText?.Trim() ?? string.Empty;
        if (string.IsNullOrEmpty(variableName)) return false;

        var variable = processInstance.Variables.FirstOrDefault(v => v.Name == variableName);
        if (variable?.Value == null) return false;

        return bool.TryParse(variable.Value, out var boolValue) 
            ? boolValue 
            : string.Equals(variable.Value, "true", StringComparison.OrdinalIgnoreCase);
    }
}