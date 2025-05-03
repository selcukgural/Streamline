using Microsoft.Extensions.Logging;
using Streamline.Domain.Schema.Activities;
using Streamline.Domain.Schema.Common;
using Streamline.Domain.Schema.Events; // For StartEvent
using Streamline.Engine.Abstractions;
using Task = System.Threading.Tasks.Task;

namespace Streamline.Engine.Services.Handlers;

/// <summary>
/// Handles the execution logic for entering a SubProcess.
/// Creates a scope execution for the SubProcess and starts executions 
/// from the Start Events within the SubProcess.
/// </summary>
public class SubProcessHandler(ILogger<SubProcessHandler> logger) : IFlowNodeHandler<SubProcess>
{
    public async Task ExecuteAsync(FlowNode node, FlowNodeHandlerContext context, CancellationToken cancellationToken)
    {
        if (node is not SubProcess subProcess)
        {            
            logger.LogError("Node type mismatch. Expected SubProcess but got {NodeType} for Node {NodeId}", node.GetType().Name, node.Id);
            context.Execution.Fail("Incorrect node type passed to SubProcessHandler");
            await context.UnitOfWork.SaveChangesAsync(cancellationToken);
            return;
        }

        logger.LogInformation("Entering SubProcess {SubProcessId} ({SubProcessName}) for Execution {ExecutionId}", 
                            subProcess.Id, subProcess.Name ?? "Unnamed", context.Execution.Id);

        // 1. Create a new scope execution for the SubProcess
        // The scope execution represents the SubProcess itself and holds its local variables/state.
        // It is linked to the parent execution (context.Execution) and has IsScope=true.
        // We pass the SubProcess ID as the ScopeFlowNodeId.
        var scopeExecution = context.Execution.CreateChildExecution(isScope: true, scopeFlowNodeId: subProcess.Id);
        scopeExecution.CurrentFlowNodeId = subProcess.Id; // Scope execution initially points to the SubProcess node itself
        
        // Add the new scope execution to the process instance
        // Ensure ProcessInstance collection is managed correctly (either via navigation or explicit add)
        context.Execution.ProcessInstance.AddExecution(scopeExecution); 
        
        logger.LogDebug("Created Scope Execution {ScopeExecutionId} for SubProcess {SubProcessId}", 
                        scopeExecution.Id, subProcess.Id);

        // Save the new scope execution immediately?
        // Or wait until inner start events are processed?
        // Let's save now to ensure it exists before starting inner flows.
        await context.UnitOfWork.SaveChangesAsync(cancellationToken);

        // 2. Find Start Events within the SubProcess
        var startEvents = subProcess.FlowElement?.OfType<StartEvent>().ToList();

        if (startEvents == null || !startEvents.Any())
        {
            // This is usually a modeling error, a SubProcess should have at least one Start Event.
            logger.LogError("SubProcess {SubProcessId} does not contain any Start Events. Cannot start internal flow.", subProcess.Id);
            // Fail the scope execution? Or the parent?
            scopeExecution.Fail("SubProcess has no Start Event");
            // Parent execution (context.Execution) remains stuck at the SubProcess node.
            await context.UnitOfWork.SaveChangesAsync(cancellationToken);
            return;
        }

        logger.LogDebug("Found {Count} Start Event(s) in SubProcess {SubProcessId}. Starting internal flow(s).", 
                        startEvents.Count, subProcess.Id);

        // 3. Start a new execution path from each Start Event within the SubProcess scope.
        // These new executions are children of the scopeExecution.
        var startTasks = new List<Task>();
        foreach (var startEvent in startEvents)
        {
            logger.LogInformation("Starting execution within SubProcess {SubProcessId} from StartEvent {StartEventId}", 
                                subProcess.Id, startEvent.Id);
            // Use the StartExecutionAtNodeAsync method, providing the scopeExecution as initiator? 
            // Or create a child of scopeExecution first?
            // Let's refine: Create a child of the scope execution, position it, then continue.
            
            var innerExecution = scopeExecution.CreateChildExecution(isScope: false); // Inner flow is not a scope itself initially
            innerExecution.CurrentFlowNodeId = startEvent.Id; // Position at the start event
            context.Execution.ProcessInstance.AddExecution(innerExecution); 
            
            // Save the inner execution
            await context.UnitOfWork.SaveChangesAsync(cancellationToken);
            
            // Schedule continuation for the inner execution
            startTasks.Add(context.ExecutionFlowManager.ContinueExecutionAsync(innerExecution.Id, cancellationToken));
        }

        // Wait for all start event continuations to be scheduled/triggered
        // We don't wait for them to *finish* here.
        await Task.WhenAll(startTasks);

        // 4. The original execution (context.Execution) now waits implicitly.
        // It remains active but positioned at the SubProcess node.
        // Logic for when/how it continues after the SubProcess completes is handled elsewhere
        // (e.g., when all scope executions terminate or a specific End Event is reached).
        logger.LogInformation("Parent Execution {ExecutionId} is now waiting for SubProcess {SubProcessId} to complete.", 
                            context.Execution.Id, subProcess.Id);
        
        // DO NOT call ContinueExecutionAsync for context.Execution here.
    }
} 