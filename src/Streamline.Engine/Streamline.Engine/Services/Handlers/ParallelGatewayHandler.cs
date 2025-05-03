using Microsoft.Extensions.Logging;
using Streamline.Domain.Schema.Common;
using Streamline.Domain.Schema.Flow;
using Streamline.Domain.Schema.Gateways;
// For ParallelGateway
// For SequenceFlow, FlowNode
// Added for SequenceFlow
using Streamline.Engine.Abstractions;   // For IFlowNodeHandler, FlowNodeHandlerContext, IExecutionFlowManager
// Ensure Linq is imported

// Ensure Task is imported

namespace Streamline.Engine.Services.Handlers;

public class ParallelGatewayHandler(ILogger<ParallelGatewayHandler> logger) : IFlowNodeHandler<ParallelGateway>
{
    // IFlowNodeHandler arayüzünden gelen ExecuteAsync metodu
    public async Task ExecuteAsync(FlowNode node, FlowNodeHandlerContext context, CancellationToken cancellationToken)
    {
        // Gelen node'u doğru tipe cast et
        if (node is not ParallelGateway gateway)
        {
            logger.LogError("Node type mismatch. Expected ParallelGateway but got {NodeType} for Node {NodeId}", node.GetType().Name, node.Id);
            return;
        }

        // Get the full definition of the gateway to access incoming/outgoing flows
        // Incoming/Outgoing genellikle XML Qualified Name listesi içerir, bu isimlerle Definition nesnesinden akışları bulmalıyız.
        var incomingFlowRefs = gateway.Incoming; // XML Qualified Names
        var outgoingFlowRefs = gateway.Outgoing; // XML Qualified Names

        var isDiverging = incomingFlowRefs.Count <= 1 && outgoingFlowRefs.Count > 1;
        var isConverging = incomingFlowRefs.Count > 1 && outgoingFlowRefs.Count <= 1; // Allowing 0 or 1 outgoing for join

        logger.LogInformation("Executing Parallel Gateway {GatewayId} for Execution {ExecutionId}. IncomingRefs: {IncomingCount}, OutgoingRefs: {OutgoingCount}. Type: {GatewayType}",
            gateway.Id, context.Execution.Id, incomingFlowRefs.Count, outgoingFlowRefs.Count, isDiverging ? "Diverging (Fork)" : (isConverging ? "Converging (Join)" : "Unknown/Error"));

        // Find the corresponding ActivityInstance for this gateway entry (Gateways usually don't have instances, but checking)
        var activityInstance = context.Execution.ProcessInstance.ActivityInstances
            .FirstOrDefault(ai => ai.FlowNodeId == gateway.Id &&
                                  ai.EndTime == null);

        if (activityInstance != null)
        {
            activityInstance.Complete();
            logger.LogInformation("Completed Activity Instance {ActivityInstanceId} for Parallel Gateway {GatewayId}", activityInstance.Id, gateway.Id);
            await context.UnitOfWork.SaveChangesAsync(cancellationToken); // Save activity instance completion early
        }


        if (isDiverging)
        {
            await HandleDivergingGateway(gateway, context, cancellationToken);
        }
        else if (isConverging)
        {
            await HandleConvergingGateway(gateway, context, cancellationToken);
        }
        else
        {
            // This case indicates a modelling error (e.g., 1 incoming, 1 outgoing parallel gateway)
            logger.LogError("Parallel Gateway {GatewayId} has an invalid configuration. IncomingRefs: {IncomingCount}, OutgoingRefs: {OutgoingCount}. Execution {ExecutionId} will be terminated.",
                gateway.Id, incomingFlowRefs.Count, outgoingFlowRefs.Count, context.Execution.Id);
            context.Execution.Terminate();
            await context.UnitOfWork.SaveChangesAsync(cancellationToken); // Save termination
        }
    }

    private async Task HandleDivergingGateway(ParallelGateway gateway, FlowNodeHandlerContext context, CancellationToken cancellationToken)
    {
        logger.LogInformation("Parallel Gateway {GatewayId}: Forking execution {ExecutionId}.", gateway.Id, context.Execution.Id);

        // 1. Terminate the incoming execution
        context.Execution.Terminate();

        // Save termination of incoming execution and completion of ActivityInstance (if any)
        await context.UnitOfWork.SaveChangesAsync(cancellationToken);


        // 2. Find all outgoing sequence flows from the Definitions
        var outgoingFlowDefinitions = context.Definitions.FindSequenceFlows(gateway.Outgoing.Select(q => q.Name).ToList());

        if (outgoingFlowDefinitions == null || !outgoingFlowDefinitions.Any())
        {
            logger.LogError("Parallel Gateway {GatewayId}: Could not find any outgoing sequence flow definitions for fork. Terminating path.", gateway.Id);
            return; // Nothing more to do
        }

        logger.LogDebug("Parallel Gateway {GatewayId}: Found {Count} outgoing sequence flows. Creating new executions.", gateway.Id, outgoingFlowDefinitions.Count);

        // 3. For each outgoing flow, call the new ForkAndContinueExecutionAsync method
        var forkTasks = new List<Task>();
        foreach (var flow in outgoingFlowDefinitions)
        {
            logger.LogDebug("Parallel Gateway {GatewayId}: Calling ForkAndContinueExecutionAsync for outgoing flow {FlowId}", gateway.Id, flow.Id);
            // Pass the original execution context and the specific flow
            forkTasks.Add(context.ExecutionFlowManager.ForkAndContinueExecutionAsync(context.Execution, context.Definitions, flow, cancellationToken));
        }

        // Wait for all fork operations to complete (optional, depending on desired behavior)
        await Task.WhenAll(forkTasks);

        logger.LogInformation("Parallel Gateway {GatewayId}: Finished signaling continuation for {Count} outgoing flows.", gateway.Id, outgoingFlowDefinitions.Count);
    }

    private async Task HandleConvergingGateway(ParallelGateway gateway, FlowNodeHandlerContext context, CancellationToken cancellationToken)
    {
        logger.LogInformation("Parallel Gateway {GatewayId}: Execution {ExecutionId} arrived at converging gateway.", gateway.Id, context.Execution.Id);

        // 1. Mark the incoming execution as arrived and waiting at this gateway
        context.Execution.ArriveAtConvergingGateway(gateway.Id);
        // Save the updated execution state (IsActive=false, WaitingAtGatewayId set)
        await context.UnitOfWork.SaveChangesAsync(cancellationToken);

        // 2. Check if all required executions have arrived
        // Incoming count should be determined from the *definition*, not just the instance's properties if they aren't fully resolved.
        // We need the actual SequenceFlow objects coming into this gateway from the definition.
        var incomingFlowDefinitions = context.Definitions.FindIncomingSequenceFlows(gateway.Id); // Assuming this method exists
        var requiredExecutions = incomingFlowDefinitions?.Count ?? gateway.Incoming.Count; // Fallback to XML count if definition query fails
        if (requiredExecutions <= 1) {
            logger.LogWarning("Parallel Gateway {GatewayId}: Converging gateway reached but has {RequiredCount} or fewer incoming flows defined. Proceeding as if join is met.", gateway.Id, requiredExecutions);
            // If only 1 incoming, behave like a normal sequence flow join (no waiting needed)
        }

        // Query all ACTIVE executions that might be heading towards this gateway
        // AND all INACTIVE executions already waiting at this gateway.
        var relevantExecutions = context.Execution.ProcessInstance.Executions
            .Where(ex => ex.IsActive || ex.WaitingAtGatewayId == gateway.Id)
            .ToList(); // Materialize for counting

        // Count how many distinct incoming flows have an inactive execution waiting at this gateway.
        // This requires knowing which SequenceFlow led each execution to wait here.
        // This information isn't directly on the Execution entity. This logic needs refinement.
        // --- Simplified Check --- :
        // Count how many executions are now inactive and waiting at this specific gateway
        var arrivedCount = relevantExecutions.Count(ex => !ex.IsActive && ex.WaitingAtGatewayId == gateway.Id);
        // ------------------------

        logger.LogDebug("Parallel Gateway {GatewayId}: Check join condition. Required: {RequiredCount}, Arrived (Waiting): {ArrivedCount}",
            gateway.Id, requiredExecutions, arrivedCount);

        if (arrivedCount >= requiredExecutions)
        {
            logger.LogInformation("Parallel Gateway {GatewayId}: All {RequiredCount} incoming executions have arrived. Signaling continuation.", gateway.Id, requiredExecutions);

            // Terminate all waiting executions except the one that triggered the join (optional, based on audit needs)
            var waitingExecutions = relevantExecutions.Where(ex => ex.WaitingAtGatewayId == gateway.Id).ToList();
            foreach(var exec in waitingExecutions)
            {
                if(exec.Id != context.Execution.Id) // Don't terminate the current trigger execution again
                {
                    exec.Terminate(); // Mark others as terminated after join
                }
            }
            await context.UnitOfWork.SaveChangesAsync(cancellationToken); // Save terminations

            // Find the single outgoing sequence flow reference
            var outgoingFlowRef = gateway.Outgoing.FirstOrDefault();
            if (outgoingFlowRef == null)
            {
                logger.LogWarning("Parallel Gateway {GatewayId}: Converging gateway has no outgoing sequence flow defined. Path ends here.", gateway.Id);
                return;
            }

            // Find the flow definition
            var outgoingFlow = context.Definitions.FindFlowElementById<SequenceFlow>(outgoingFlowRef.Name);
            if (outgoingFlow == null)
            {
                logger.LogError("Parallel Gateway {GatewayId}: Could not find outgoing sequence flow definition for {FlowName}. Process cannot continue.", gateway.Id, outgoingFlowRef.Name);
                return;
            }

            // Reactivate the current execution and signal continuation along the single outgoing flow.
            // The ExecutionFlowManager should ideally create a *new* execution for the outgoing path,
            // using the context of the joined executions.
            context.Execution.Reactivate(); // Reactivate the one that completed the join to proceed
            context.Execution.LeaveConvergingGateway();
            await context.ExecutionFlowManager.ContinueExecutionAsync(context.Execution.Id, cancellationToken);

            logger.LogInformation("Parallel Gateway {GatewayId}: Signaled continuation for joined execution {ExecutionId} along flow {FlowId}", gateway.Id, context.Execution.Id, outgoingFlow.Id);
        }
        else
        {
            logger.LogInformation("Parallel Gateway {GatewayId}: Execution {ExecutionId} is now waiting for {RemainingCount} more parallel paths to arrive.",
                gateway.Id, context.Execution.Id, requiredExecutions - arrivedCount);
            // Do nothing further. The execution is inactive and waiting.
        }
    }
}