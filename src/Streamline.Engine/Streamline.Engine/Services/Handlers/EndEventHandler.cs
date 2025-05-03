using Microsoft.Extensions.Logging;
using Streamline.Domain.Schema.Common;
using Streamline.Domain.Schema.Events;
// For EndEvent
using Streamline.Engine.Abstractions; // For IFlowNodeHandler, FlowNodeHandlerContext
using Streamline.Domain.Runtime; // Execution için eklendi
using Streamline.Domain.Abstractions; // Added for IExecutionFlowManager & IUnitOfWork
using Streamline.Domain.Schema.Activities; // Added for Transaction
using Task = System.Threading.Tasks.Task; // Keep alias

// Include ve ToListAsync için eklendi (gerekli olabilir)

// FlowNode için

namespace Streamline.Engine.Services.Handlers;

public class EndEventHandler(
    ILogger<EndEventHandler> logger,
    IUnitOfWork unitOfWork, // Already had UnitOfWork
    IExecutionFlowManager executionFlowManager // Added IExecutionFlowManager
    ) : IFlowNodeHandler<EndEvent>
{
    public async Task ExecuteAsync(FlowNode node, FlowNodeHandlerContext context, CancellationToken cancellationToken)
    {
        if (node is not EndEvent endEvent)
        {
            logger.LogError("Node type mismatch. Expected EndEvent but got {NodeType} for Node {NodeId}", node.GetType().Name, node.Id);
            return;
        }

        var execution = context.Execution;
        var definitions = context.Definitions;
        var eventDefinition = endEvent.EventDefinition.FirstOrDefault(); // Get the specific definition if any

        logger.LogInformation("Executing End Event {EndEventId} (Definition Type: {DefinitionType}) for Execution {ExecutionId}", 
                         endEvent.Id, eventDefinition?.GetType().Name ?? "None", execution.Id);

        // --- Handle Specific End Event Types --- 

        if (eventDefinition is TerminateEventDefinition)
        {
            await HandleTerminateEndEventAsync(endEvent, context, cancellationToken);
        }
        else if (eventDefinition is ErrorEventDefinition errorDef)
        {
            await HandleErrorEndEventAsync(endEvent, errorDef, context, cancellationToken);
        }
        else if (eventDefinition is EscalationEventDefinition escalationDef)
        {
            await HandleEscalationEndEventAsync(endEvent, escalationDef, context, cancellationToken);
        }
        else if (eventDefinition is CancelEventDefinition)
        {
            await HandleCancelEndEventAsync(endEvent, context, cancellationToken);
        }
        else // Includes None End Event (eventDefinition == null or other unsupported type)
        {   
             if(eventDefinition != null) { 
                 logger.LogWarning("Unhandled End Event Definition type: {DefinitionType} for EndEvent {EndEventId}. Treating as None End Event.", 
                                 eventDefinition.GetType().Name, endEvent.Id);
             }
            await HandleNoneEndEventAsync(endEvent, context, cancellationToken);
        }
    }

    // --- Helper Methods for Each End Event Type --- 

    private async Task HandleTerminateEndEventAsync(EndEvent terminateEvent, FlowNodeHandlerContext context, CancellationToken cancellationToken)
    {
        var processInstanceId = context.Execution.ProcessInstanceId;
        logger.LogWarning("Handling Terminate End Event {TerminateEventId} for Process Instance {ProcessInstanceId}. All active executions will be terminated.",
                         terminateEvent.Id, processInstanceId);

        var executionRepo = unitOfWork.GetRepository<Execution>();
        var activityInstanceRepo = unitOfWork.GetRepository<ActivityInstance>();
        var processInstanceRepo = unitOfWork.GetRepository<ProcessInstance>();

        var activeExecutions = await executionRepo.ListAsync(e => e.ProcessInstanceId == processInstanceId && e.IsActive, cancellationToken);
        var terminationReason = $"Process instance terminated by TerminateEndEvent {terminateEvent.Id}";

        if (activeExecutions.Any())
        {
            logger.LogInformation("Terminating {Count} active executions.", activeExecutions.Count);
            foreach (var exec in activeExecutions)
            {
                if (exec.IsActive) exec.Terminate(terminationReason);
            }
        }
        else { logger.LogInformation("No other active executions found to terminate."); }
        
        if (context.Execution.IsActive) // Terminate the triggering execution if somehow still active
        {
            context.Execution.Terminate(terminationReason);
        }

        var activeActivityInstances = await activityInstanceRepo.ListAsync(ai => ai.ProcessInstanceId == processInstanceId && ai.EndTime == null, cancellationToken);
        if (activeActivityInstances.Any())
        {
             logger.LogInformation("Canceling {Count} active activity instances.", activeActivityInstances.Count);
             foreach (var ai in activeActivityInstances)
             {
                 ai.Cancel();
             }
        }

        var processInstance = await processInstanceRepo.GetByIdAsync(processInstanceId, cancellationToken);
        if (processInstance != null && processInstance.State != ProcessInstanceState.Completed)
        {
            logger.LogWarning("Marking Process Instance {ProcessInstanceId} as Completed.", processInstanceId);
            processInstance.Complete(); 
        }
        else if (processInstance == null) { logger.LogError("Could not find Process Instance {ProcessInstanceId} to complete.", processInstanceId); }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Terminate End Event {TerminateEventId} processing completed.", terminateEvent.Id);
    }

    private async Task HandleErrorEndEventAsync(EndEvent errorEndEvent, ErrorEventDefinition errorDef, FlowNodeHandlerContext context, CancellationToken cancellationToken)
    {
        var execution = context.Execution;
        var definitions = context.Definitions;
        var errorCode = errorDef.ErrorRef?.Name; // Get code from linked Error element
        var errorMessage = $"Error signaled by ErrorEndEvent {errorEndEvent.Id}";

        if (!string.IsNullOrEmpty(errorCode))
        {
            errorMessage = $"Error '{errorCode}' signaled by ErrorEndEvent {errorEndEvent.Id}";
            logger.LogWarning("Handling Error End Event {ErrorEventId} with ErrorCode: {ErrorCode}", errorEndEvent.Id, errorCode);
        }
        else
        {            
            logger.LogWarning("Handling Error End Event {ErrorEventId}, but no ErrorCode defined.", errorEndEvent.Id);
            errorCode = $"Error_{errorEndEvent.Id}"; // Generate default if needed
        }

        execution.Fail(errorCode, errorMessage);
        logger.LogDebug("Execution {ExecutionId} marked as failed. Triggering error propagation.", execution.Id);

        var handled = await executionFlowManager.FindAndHandleErrorAsync(execution, definitions, errorCode, cancellationToken);

        if (!handled)
        { logger.LogWarning("Error propagation for ErrorCode '{ErrorCode}' was NOT handled.", errorCode); }
        else { logger.LogInformation("Error propagation for ErrorCode '{ErrorCode}' was handled.", errorCode); }
        
        // FindAndHandleErrorAsync should save changes.
    }

    private async Task HandleEscalationEndEventAsync(EndEvent escalationEndEvent, EscalationEventDefinition escalationDef, FlowNodeHandlerContext context, CancellationToken cancellationToken)
    {
        var execution = context.Execution;
        var definitions = context.Definitions;
        var escalationCode = escalationDef.EscalationRef?.Name; // Get code from linked Escalation element

        if (string.IsNullOrEmpty(escalationCode))
        {             
             logger.LogWarning("Handling Escalation End Event {EscalationEventId}, but no EscalationCode defined. Skipping propagation.", escalationEndEvent.Id);
             await HandleNoneEndEventAsync(escalationEndEvent, context, cancellationToken); // Treat as None if no code
             return;
        }
        
        logger.LogInformation("Handling Escalation End Event {EscalationEventId} with EscalationCode: {EscalationCode}", escalationEndEvent.Id, escalationCode);
        
        execution.Escalate(escalationCode);
        logger.LogDebug("Execution {ExecutionId} marked with LastEscalationCode. Triggering escalation propagation.", execution.Id);
        
        var handled = await executionFlowManager.FindAndHandleEscalationAsync(execution, definitions, escalationCode, execution.CurrentFlowNodeId, cancellationToken);

        if (!handled) { logger.LogInformation("Escalation propagation for EscalationCode '{EscalationCode}' was NOT handled.", escalationCode); } 
        else { logger.LogInformation("Escalation propagation for EscalationCode '{EscalationCode}' was handled.", escalationCode); }

        // IMPORTANT: Do NOT terminate the current execution here. Flow continues unless interrupted.
        await unitOfWork.SaveChangesAsync(cancellationToken); // Save the EscalationCode state
        logger.LogDebug("Finished processing EscalationEndEvent {EscalationEventId}. Execution continues.", escalationEndEvent.Id);
    }

    private async Task HandleCancelEndEventAsync(EndEvent cancelEndEvent, FlowNodeHandlerContext context, CancellationToken cancellationToken)
    {
        var execution = context.Execution;
        var definitions = context.Definitions;
        logger.LogInformation("Handling Cancel End Event {CancelEventId}. Attempting transaction cancellation.", cancelEndEvent.Id);

        // Find Transaction Scope
        var scopeExecution = execution;
        while (scopeExecution != null && !scopeExecution.IsScope)
        {
             if (scopeExecution.ParentExecution == null && scopeExecution.ParentExecutionId.HasValue) 
             { scopeExecution = await unitOfWork.GetRepository<Execution>().GetByIdAsync(scopeExecution.ParentExecutionId.Value, cancellationToken); }
             else { scopeExecution = scopeExecution.ParentExecution; }
        }

        if (scopeExecution == null || !scopeExecution.IsScope || string.IsNullOrEmpty(scopeExecution.ScopeFlowNodeId))
        { /* Log error, Fail execution */ 
            logger.LogError("CancelEndEvent {CancelEventId} not within a valid scope.", cancelEndEvent.Id);
            execution.Fail("CancelEndEventMisplaced", $"CancelEndEvent {cancelEndEvent.Id} not within a valid Transaction scope.");
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return; 
        }

        var transactionNode = definitions.FindFlowElementById<Transaction>(scopeExecution.ScopeFlowNodeId);
        if (transactionNode == null) 
        { /* Log error, Fail execution */
            logger.LogError("CancelEndEvent {CancelEventId} scope {ScopeFlowNodeId} is not a Transaction.", cancelEndEvent.Id, scopeExecution.ScopeFlowNodeId);
            execution.Fail("CancelEndEventNotInTransaction", $"CancelEndEvent {cancelEndEvent.Id} scope {scopeExecution.ScopeFlowNodeId} is not a Transaction.");
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return; 
        }
        
        // Find attached Cancel Boundary Event
        var cancelBoundaryEvent = definitions.RootElement
                                                  .OfType<Process>() // Filter for Process elements
                                                  .SelectMany(p => p.FlowElement?.OfType<BoundaryEvent>() ?? Enumerable.Empty<BoundaryEvent>()) // Select BoundaryEvents from each Process
                                                  .FirstOrDefault(be => 
                                                        be.AttachedToRef?.Name == transactionNode.Id && 
                                                        be.EventDefinition.OfType<CancelEventDefinition>().Any());

        if (cancelBoundaryEvent == null) 
        { /* Log error, Fail scope */ 
            logger.LogError("Missing Cancel Boundary Event for Transaction {TransactionId}.", transactionNode.Id);
            scopeExecution.Fail("MissingCancelBoundaryEvent", $"No Cancel Boundary Event for Transaction {transactionNode.Id}. Required by {cancelEndEvent.Id}.");
            if(execution.IsActive) execution.Terminate("Missing Cancel Boundary Event");
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return; 
        }

        logger.LogInformation("Found Cancel Boundary Event {BoundaryEventId}. Triggering cancellation flow.", cancelBoundaryEvent.Id);
        
        if(execution.IsActive) { execution.Terminate($"Reached CancelEndEvent {cancelEndEvent.Id}"); }

        try
        {
            await executionFlowManager.TerminateScopeContentsAsync(scopeExecution, true, $"Transaction cancelled by {cancelEndEvent.Id} via {cancelBoundaryEvent.Id}", cancellationToken);
            await executionFlowManager.StartExecutionAtNodeAsync(execution.ProcessInstanceId, cancelBoundaryEvent.Id, scopeExecution.Id, cancellationToken);
            logger.LogInformation("Cancellation flow started from {BoundaryEventId}.", cancelBoundaryEvent.Id);
        }
        catch(Exception ex)
        { /* Log error, Fail scope if active */
            logger.LogError(ex, "Failed during cancellation process for {BoundaryEventId}.", cancelBoundaryEvent.Id);
            if(scopeExecution.IsActive) 
            { 
               scopeExecution.Fail("CancelFlowFailed", $"Failed during cancel flow: {ex.Message}");
               await unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }
        // Changes saved within TerminateScopeContentsAsync, StartExecutionAtNodeAsync or catch block.
    }

    private async Task HandleNoneEndEventAsync(EndEvent endEvent, FlowNodeHandlerContext context, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling None End Event {EndEventId}.", endEvent.Id);
        var currentExecution = context.Execution;
        var parentExecutionId = currentExecution.ParentExecutionId;
        var processInstanceId = currentExecution.ProcessInstanceId;

        // Complete associated ActivityInstance if one exists (optional for EndEvent, but good practice)
        var activityInstance = context.Execution.ProcessInstance?.ActivityInstances
            .FirstOrDefault(ai => ai.FlowNodeId == endEvent.Id && ai.EndTime == null);
        if (activityInstance != null) { activityInstance.Complete(); }
        else { logger.LogDebug("No active ActivityInstance found for End Event {EndEventId}.", endEvent.Id); }

        currentExecution.Terminate();
        logger.LogInformation("Terminated Execution {ExecutionId}.", currentExecution.Id);

        var continueParent = false;

        // Check if SubProcess completed
        if (parentExecutionId.HasValue)
        {
            var executionRepo = unitOfWork.GetRepository<Execution>();
            var scopeExecution = await executionRepo.GetByIdAsync(parentExecutionId.Value, cancellationToken);
            if (scopeExecution != null && scopeExecution.IsScope)
            {
                var otherActive = await executionRepo.AnyAsync(
                    e => e.ParentExecutionId == parentExecutionId.Value && e.IsActive && e.Id != currentExecution.Id,
                    cancellationToken);
                
                if (!otherActive)
                {
                    logger.LogInformation("SubProcess scope {ScopeExecutionId} completed. Attempting to continue parent.", scopeExecution.Id);
                    if (scopeExecution.ParentExecutionId.HasValue)
                    {
                        // Find the execution waiting at the SubProcess node
                        var parentWaitingExecution = await executionRepo.FirstOrDefaultAsync(e => 
                            e.Id == scopeExecution.ParentExecutionId.Value && 
                            e.CurrentFlowNodeId == scopeExecution.ScopeFlowNodeId && 
                            e.IsActive, cancellationToken);
                            
                        if (parentWaitingExecution != null)
                        {
                            logger.LogInformation("Found waiting parent execution {ParentExecutionId}. Triggering continuation.", parentWaitingExecution.Id);
                            await executionFlowManager.ContinueExecutionAsync(parentWaitingExecution.Id, cancellationToken);
                            continueParent = true; // Signal that parent continued, no need for PI check here
                        }
                        else { logger.LogWarning("Could not find suitable waiting parent execution for completed scope {ScopeExecutionId}.", scopeExecution.Id); }
                    }
                    else { logger.LogWarning("Completed scope {ScopeExecutionId} has no parent.", scopeExecution.Id); }
                }
                 else { logger.LogDebug("SubProcess scope {ScopeExecutionId} not complete yet.", scopeExecution.Id); }
            }
        }

        // Check if Process Instance completed (only if not continuing a parent from SubProcess)
        if (!continueParent)
        {
            var executionRepo = unitOfWork.GetRepository<Execution>();
            var anyOtherActive = await executionRepo.AnyAsync(
                e => e.ProcessInstanceId == processInstanceId && e.IsActive && e.Id != currentExecution.Id,
                cancellationToken);

            if (!anyOtherActive)
            {
                logger.LogInformation("Process Instance {ProcessInstanceId} completed.", processInstanceId);
                var processInstanceRepo = unitOfWork.GetRepository<ProcessInstance>();
                var processInstance = await processInstanceRepo.GetByIdAsync(processInstanceId, cancellationToken);
                if (processInstance != null) { processInstance.Complete(); }
                else { logger.LogError("Could not find Process Instance {ProcessInstanceId} to complete.", processInstanceId); }
                
                // Save changes for PI completion if not continuing parent
                await unitOfWork.SaveChangesAsync(cancellationToken);
                logger.LogDebug("Saved final PI state for {ProcessInstanceId}.", processInstanceId);
            }
            else {
                logger.LogDebug("Other active executions exist for PI {ProcessInstanceId}. Saving current execution termination.", processInstanceId);
                // Save changes for current execution termination if PI not complete
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }
        // If continueParent is true, ContinueExecutionAsync would have saved necessary changes.
        logger.LogDebug("Finished processing None End Event {EndEventId}.", endEvent.Id);
    }
}