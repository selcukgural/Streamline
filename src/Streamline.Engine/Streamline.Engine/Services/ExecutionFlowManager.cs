using Microsoft.Extensions.Logging;
using Streamline.Domain.Abstractions;
using Streamline.Domain.Runtime;
using Streamline.Domain.Schema.Common;
using Streamline.Domain.Schema.Events;
using Streamline.Domain.Schema.Flow;
using Streamline.Engine.Abstractions;
using Streamline.Domain.Schema.Activities;
using BpmnTask = Streamline.Domain.Schema.Activities.Task;
using NodaTime;
using NodaTime.Text;
using Task = System.Threading.Tasks.Task;

// For EventSubscription

namespace Streamline.Engine.Services;

/// <summary>
/// Implements the logic for advancing executions through the process definition.
/// </summary>
public class ExecutionFlowManager(
    IUnitOfWork unitOfWork,
    IBpmnXmlService bpmnXmlService,
    ILogger<ExecutionFlowManager> logger,
    IFlowNodeHandlerFactory flowNodeHandlerFactory,
    ITimerJobScheduler timerJobScheduler)
    : IExecutionFlowManager
{
    public async Task ContinueExecutionAsync(Guid executionId, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("ExecutionFlowManager starting continuation for Execution {ExecutionId}", executionId);

        var executionRepo = unitOfWork.GetRepository<Execution>();
        var execution = await executionRepo.GetByIdAsync(executionId, cancellationToken);

        if (execution == null || !execution.IsActive || string.IsNullOrEmpty(execution.CurrentFlowNodeId))
        {
            logger.LogWarning("Execution {ExecutionId} not found, inactive, or not positioned at a node. Cannot continue.", executionId);
            return;
        }

        var processInstance = await unitOfWork.GetRepository<ProcessInstance>().GetByIdAsync(execution.ProcessInstanceId, cancellationToken);
        if (processInstance == null)
        {
            logger.LogError("ProcessInstance {ProcessInstanceId} not found for Execution {ExecutionId}. Terminating execution.", execution.ProcessInstanceId, execution.Id);
            execution.Terminate();
            return;
        }
        execution.ProcessInstance = processInstance;

        Definitions definitions;
        try
        {
            var xmlContent = await GetProcessDefinitionXmlAsync(processInstance.ProcessDefinitionId, cancellationToken);
            definitions = bpmnXmlService.Import(xmlContent);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to load or parse process definition {ProcessDefinitionId} for Execution {ExecutionId}. Failing execution.", processInstance.ProcessDefinitionId, execution.Id);
            execution.Fail("Failed to load process definition", ex.Message);
            return;
        }

        var process = definitions.RootElement.OfType<Process>().FirstOrDefault();
        if (process == null)
        {
            logger.LogError("No process element found in definition {ProcessDefinitionId}. Failing execution.", processInstance.ProcessDefinitionId);
            execution.Fail("Process element not found in definition");
            return;
        }

        var currentFlowNode = process.FlowElement?.OfType<FlowNode>().FirstOrDefault(fn => fn.Id == execution.CurrentFlowNodeId);
        if (currentFlowNode == null)
        {
            logger.LogWarning("Cannot continue execution {ExecutionId}: Current node {FlowNodeId} not found in definition. Failing execution.", execution.Id, execution.CurrentFlowNodeId);
            execution.Fail("Current flow node not found in definition");
            return;
        }

        logger.LogDebug("Continuing execution {ExecutionId} from node {NodeId} ({NodeType})", execution.Id, currentFlowNode.Id, currentFlowNode.GetType().Name);

        if (currentFlowNode is Activity activityNode)
        {
            await RegisterBoundaryEventSubscriptionsAsync(execution, definitions, activityNode, cancellationToken);
        }

        try
        {
            var handler = flowNodeHandlerFactory.GetHandler(currentFlowNode);
            var context = new FlowNodeHandlerContext(execution, definitions, unitOfWork, this);
            await handler.ExecuteAsync(currentFlowNode, context, cancellationToken);

            // Check for FAILURE after handler execution
            if (!context.Execution.IsActive && !string.IsNullOrEmpty(context.Execution.ErrorCode))
            {
                 logger.LogWarning("Execution {ExecutionId} failed at node {NodeId} with ErrorCode '{ErrorCode}'. Searching for error handler.", 
                                 context.Execution.Id, currentFlowNode.Id, context.Execution.ErrorCode);
                 await FindAndHandleErrorAsync(context.Execution, definitions, context.Execution.ErrorCode, cancellationToken);
                 return; // Stop normal processing after error handling
            }
            
            // Check for ESCALATION after handler execution
            if (context.Execution.IsActive && !string.IsNullOrEmpty(context.Execution.LastEscalationCode)) // Check IsActive for escalation
            {
                 var escalationCode = context.Execution.LastEscalationCode;
                 var escalatedAtNodeId = context.Execution.CurrentFlowNodeId; // Node where Escalate() was called
                 
                 // Clear the code on the execution now that we are handling it
                 // context.Execution.ClearLastEscalation(); // Needs method on Execution
                 
                 logger.LogInformation("Execution {ExecutionId} triggered EscalationCode '{EscalationCode}' at node {NodeId}. Searching for handler.", 
                                     context.Execution.Id, escalationCode, escalatedAtNodeId ?? "(unknown)");
                 var handled = await FindAndHandleEscalationAsync(context.Execution, definitions, escalationCode, escalatedAtNodeId, cancellationToken);
                 
                 // If escalation was handled (e.g., new path started), the current execution 
                 // usually continues normally unless the handler logic dictates otherwise.
                 // We don't return here by default, allowing the original flow to proceed 
                 // after the escalation handler (if any) has been triggered.
                 logger.LogDebug("Escalation handled status: {Handled}", handled);
            }
        }
        catch (Exception ex) // Catch exceptions from handler or context creation
        {            
            logger.LogError(ex, "Error executing handler for node {NodeId} ({NodeType}) in execution {ExecutionId}. Failing execution.", 
                           currentFlowNode?.Id ?? "Unknown", currentFlowNode?.GetType().Name ?? "Unknown", executionId);
            // Ensure execution is marked as failed if not already
            if (execution.IsActive) 
            { 
                // Fail without a specific BPMN error code if the exception wasn't caught and translated by the handler
                execution.Fail("Handler execution failed", ex.ToString());
            }
            // Save changes even if context is null or execution failed
            await unitOfWork.SaveChangesAsync(cancellationToken); 
            return; // Exit after catching handler exception
        }

        // Only log finished if no error occurred or if error handling completed without exiting
        logger.LogDebug("ExecutionFlowManager finished processing continuation trigger for Execution {ExecutionId}", executionId);
        // Do we need a final SaveChanges here? Depends if state could change after handler without error.
        // await unitOfWork.SaveChangesAsync(cancellationToken); 
    }

    public async Task ForkAndContinueExecutionAsync(Execution originalExecution, Definitions definitions, SequenceFlow flowToFollow, CancellationToken cancellationToken)
    {
        logger.LogDebug("Forking execution for Flow {FlowId} from original Execution {OriginalExecutionId}", flowToFollow.Id, originalExecution.Id);

        ArgumentNullException.ThrowIfNull(originalExecution.ProcessInstance, nameof(originalExecution.ProcessInstance));

        var targetNode = definitions.FindFlowElementById<FlowNode>(flowToFollow.TargetRef);
        if (targetNode == null)
        {
            logger.LogError("Could not find target node {TargetRef} for sequence flow {FlowId}. Cannot fork execution.", flowToFollow.TargetRef, flowToFollow.Id);
            return;
        }

        var newExecution = new Execution(originalExecution.ProcessInstance)
        {
            CurrentFlowNodeId = targetNode.Id
        };
        originalExecution.ProcessInstance.AddExecution(newExecution);

        logger.LogInformation("Created new Execution {NewExecutionId} for forked path starting at Node {TargetNodeId} ({TargetNodeType})",
            newExecution.Id, targetNode.Id, targetNode.GetType().Name);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await ContinueExecutionAsync(newExecution.Id, cancellationToken);

        logger.LogDebug("Triggered continuation for new forked Execution {NewExecutionId}", newExecution.Id);
    }

    private async Task HandleNodeEntryAsync(Execution execution, FlowNode targetNode, CancellationToken cancellationToken)
    {
        logger.LogDebug("[HandleNodeEntryAsync] Handling entry into node {NodeId} ({NodeType}) for execution {ExecutionId}", targetNode.Id, targetNode.GetType().Name, execution.Id);

        if (targetNode is BpmnTask)
        {
            var activityInstanceRepo = unitOfWork.GetRepository<ActivityInstance>();
            var existingInstance = execution.ProcessInstance?.ActivityInstances.FirstOrDefault(ai =>
                ai.ExecutionId == execution.Id && ai.FlowNodeId == targetNode.Id && ai.EndTime == null);

            if (existingInstance == null)
            {
                var taskActivity = new ActivityInstance(
                    execution.ProcessInstanceId,
                    targetNode.Id,
                    execution.Id,
                    targetNode.Name
                );
                taskActivity.ProcessInstance = execution.ProcessInstance ?? throw new InvalidOperationException("ProcessInstance not loaded on Execution.");

                await activityInstanceRepo.AddAsync(taskActivity, cancellationToken);
                logger.LogInformation("[HandleNodeEntryAsync] Created ActivityInstance {ActivityInstanceId} for Task {NodeId}", taskActivity.Id, targetNode.Id);
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }
            else
            {
                logger.LogInformation("[HandleNodeEntryAsync] Found existing active ActivityInstance {ActivityInstanceId} for Task {NodeId}. No action taken.", existingInstance.Id, targetNode.Id);
            }
        }
        else if (targetNode is EndEvent)
        {
            // End event logic (termination) is likely handled by the EndEventHandler.
            // No specific action needed here in the generic entry handler.
        }
        else
        {
            logger.LogDebug("[HandleNodeEntryAsync] No specific entry action defined for {NodeType}", targetNode.GetType().Name);
        }
    }

    private async Task RegisterBoundaryEventSubscriptionsAsync(Execution execution, Definitions definitions, Activity activityNode, CancellationToken cancellationToken)
    {
        logger.LogDebug("Checking for Boundary Events attached to Activity {ActivityId} ({ActivityName}) for Execution {ExecutionId}", 
                        activityNode.Id, activityNode.Name ?? "Unnamed", execution.Id);
                        
        var process = definitions.RootElement.OfType<Process>().FirstOrDefault();
        if (process == null) 
        {
            logger.LogWarning("Process not found in definitions while checking for boundary events.");
            return;
        }

        var attachedBoundaryEvents = process.FlowElement
                                          .OfType<BoundaryEvent>()
                                          .Where(be => be.AttachedToRef?.Name == activityNode.Id) 
                                          .ToList();

        if (!attachedBoundaryEvents.Any())
        {
            logger.LogDebug("No Boundary Events found attached to Activity {ActivityId}", activityNode.Id);
            return;
        }

        var subscriptionRepo = unitOfWork.GetRepository<EventSubscription>();

        foreach (var boundaryEvent in attachedBoundaryEvents)
        {
            logger.LogInformation("Found Boundary Event {BoundaryEventId} ({BoundaryEventName}) attached to {ActivityId}. Type: {EventType}, Interrupting: {IsInterrupting}",
                                boundaryEvent.Id, boundaryEvent.Name ?? "Unnamed", activityNode.Id, boundaryEvent.EventDefinition.FirstOrDefault()?.GetType().Name ?? "Unknown", !boundaryEvent.CancelActivity);

            var eventDefinition = boundaryEvent.EventDefinition.FirstOrDefault();
            string? eventType = null;
            string? eventName = null;
            string? configurationValue = null;
            var requiresSubscription = true;
            var timerDefinitionType = "";
            var timerDefinitionValue = "";

            try
            {
                switch (eventDefinition)
                {
                    case TimerEventDefinition timerDef:
                        eventType = "Timer";
                        eventName = boundaryEvent.Id;
                        try
                        {
                            if (timerDef.TimeDate?.Text?.FirstOrDefault() != null)
                            {
                                timerDefinitionValue = timerDef.TimeDate.Text.First();
                                timerDefinitionType = "TimeDate";
                                InstantPattern.ExtendedIso.Parse(timerDefinitionValue);
                            }
                            else if (timerDef.TimeDuration?.Text?.FirstOrDefault() != null)
                            {
                                timerDefinitionValue = timerDef.TimeDuration.Text.First();
                                timerDefinitionType = "TimeDuration";
                                DurationPattern.Roundtrip.Parse(timerDefinitionValue);
                            }
                            else if (timerDef.TimeCycle?.Text?.FirstOrDefault() != null)
                            {
                                timerDefinitionValue = timerDef.TimeCycle.Text.First();
                                timerDefinitionType = "TimeCycle";
                                if (!timerDefinitionValue.Contains('/') || !timerDefinitionValue.StartsWith("R"))
                                    throw new FormatException("Invalid ISO 8601 repeating interval format.");
                            }
                            else
                            {
                                throw new InvalidOperationException("Timer definition (TimeDate/TimeDuration/TimeCycle) is missing.");
                            }
                            configurationValue = $"{timerDefinitionType}:{timerDefinitionValue}";
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, "Invalid timer definition format for Boundary Event {BoundaryEventId}. Value: '{TimerValue}'", boundaryEvent.Id, timerDefinitionValue ?? "(null)");
                            requiresSubscription = false;
                        }
                        break;

                    case MessageEventDefinition msgDef:
                        eventType = "Message";
                        eventName = msgDef.MessageRef?.Name;
                        if (string.IsNullOrWhiteSpace(eventName))
                        {
                            logger.LogError("MessageEventDefinition for Boundary Event {BoundaryEventId} is missing required Message name reference.", boundaryEvent.Id);
                            requiresSubscription = false;
                        }
                        else
                        {
                            configurationValue = "Message";
                        }
                        break;

                    case SignalEventDefinition signalDef:
                        eventType = "Signal";
                        eventName = signalDef.SignalRef?.Name;
                        if (string.IsNullOrWhiteSpace(eventName))
                        {
                            logger.LogError("SignalEventDefinition for Boundary Event {BoundaryEventId} is missing required Signal name reference.", boundaryEvent.Id);
                            requiresSubscription = false;
                        }
                        else
                        {
                            configurationValue = "Signal"; 
                            logger.LogWarning("Boundary Event Subscription logic for {EventType} is implemented (basic subscription created), but requires triggering mechanism.", eventType);
                        }
                        break;
                        
                    default:
                        requiresSubscription = false;
                        logger.LogWarning("Unhandled or unsupported Boundary Event Definition type: {DefinitionType} for Boundary Event {BoundaryEventId}",
                                        eventDefinition?.GetType().Name ?? "null", boundaryEvent.Id);
                        break;
                }

                if (requiresSubscription && eventType != null && eventName != null)
                {
                    var finalConfiguration = $"{configurationValue};Cancel={boundaryEvent.CancelActivity}";

                    var subscription = new EventSubscription(
                        eventType: eventType,
                        eventName: eventName,
                        executionId: execution.Id,
                        execution: execution,
                        processInstanceId: execution.ProcessInstanceId,
                        activityId: boundaryEvent.Id,
                        configuration: finalConfiguration,
                        attachedToActivityId: activityNode.Id
                    );

                    await subscriptionRepo.AddAsync(subscription, cancellationToken);
                    await unitOfWork.SaveChangesAsync(cancellationToken);

                    logger.LogInformation("Created EventSubscription {SubscriptionId} for Boundary Event {BoundaryEventId} ({EventType}) attached to {ActivityId}. Config: {Config}",
                                        subscription.Id, boundaryEvent.Id, eventType, activityNode.Id, finalConfiguration);

                    if (eventType == "Timer")
                    {
                        DateTime? initialDueTime = null;
                        try
                        {
                             if (timerDefinitionType == "TimeDate") 
                             {
                                 var dueInstant = InstantPattern.ExtendedIso.Parse(timerDefinitionValue).Value;
                                 initialDueTime = dueInstant.ToDateTimeUtc();
                             }
                             else if (timerDefinitionType == "TimeDuration")
                             {
                                 var duration = DurationPattern.Roundtrip.Parse(timerDefinitionValue).Value;
                                 var now = SystemClock.Instance.GetCurrentInstant();
                                 initialDueTime = (now + duration).ToDateTimeUtc();
                             }
                             else if (timerDefinitionType == "TimeCycle")
                             {
                                 var parts = timerDefinitionValue.Split('/');
                                 var interval = DurationPattern.Roundtrip.Parse(parts[1]).Value; 
                                 var now = SystemClock.Instance.GetCurrentInstant();
                                 initialDueTime = (now + interval).ToDateTimeUtc();
                             }
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, "Failed to calculate initial due time for Boundary Timer {BoundaryEventId}. Config: '{Config}'", boundaryEvent.Id, finalConfiguration);
                            await subscriptionRepo.DeleteAsync(subscription, cancellationToken);
                            await unitOfWork.SaveChangesAsync(cancellationToken);
                            continue;
                        }

                        if (initialDueTime.HasValue)
                        {
                            try
                            {
                                var jobId = await timerJobScheduler.ScheduleTimerJobAsync(subscription.Id, initialDueTime.Value);
                                subscription.JobId = jobId;
                                await subscriptionRepo.UpdateAsync(subscription, cancellationToken);
                                await unitOfWork.SaveChangesAsync(cancellationToken);
                                logger.LogInformation("Scheduled timer job {JobId} for Boundary Event Subscription {SubscriptionId} to fire at {DueTimeUTC}", jobId, subscription.Id, initialDueTime.Value);
                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, "Failed to schedule timer job or store JobId for Boundary Subscription {SubscriptionId}", subscription.Id);
                            }
                        }
                        else { 
                             logger.LogError("Could not calculate initial due time for Boundary Timer {BoundaryEventId}. Subscription created but job not scheduled.", boundaryEvent.Id);
                        }
                    }
                }
            }
            catch (Exception outerEx)
            {
                logger.LogError(outerEx, "Error processing Boundary Event {BoundaryEventId} attached to Activity {ActivityId}", boundaryEvent.Id, activityNode.Id);
            }
        }

        await Task.CompletedTask;
    }

    private async Task<string> GetProcessDefinitionXmlAsync(string processDefinitionIdOrPath, CancellationToken cancellationToken)
    {
        if (File.Exists(processDefinitionIdOrPath))
        {
            return await File.ReadAllTextAsync(processDefinitionIdOrPath, cancellationToken);
        }
        logger.LogError("Process definition XML not found at path: {ProcessDefinitionPath}", processDefinitionIdOrPath);
        throw new FileNotFoundException("Process definition XML not found.", processDefinitionIdOrPath);
    }

    public async Task<Execution> StartExecutionAtNodeAsync(Guid processInstanceId, string targetFlowNodeId, Guid? initiatingExecutionId = null, CancellationToken cancellationToken = default)
    {
        logger.LogDebug("Starting new execution directly at node {TargetFlowNodeId} in ProcessInstance {ProcessInstanceId}. Initiated by Execution: {InitiatingExecutionId}",
                       targetFlowNodeId, processInstanceId, initiatingExecutionId?.ToString() ?? "None");

        var piRepo = unitOfWork.GetRepository<ProcessInstance>();
        var processInstance = await piRepo.GetByIdAsync(processInstanceId, cancellationToken);

        if (processInstance == null)
        {
            logger.LogError("ProcessInstance {ProcessInstanceId} not found. Cannot start execution at node {TargetFlowNodeId}.", processInstanceId, targetFlowNodeId);
            // Consider throwing a specific exception
            throw new InvalidOperationException($"ProcessInstance {processInstanceId} not found.");
        }

        // Optional: Validate targetFlowNodeId exists in the definition
        // Definitions definitions = await LoadDefinitionsForInstance(processInstance, cancellationToken); // Helper needed
        // var targetNode = definitions.FindFlowElementById<FlowNode>(targetFlowNodeId);
        // if (targetNode == null) { ... log error and throw ... }

        var newExecution = new Execution(processInstance) // Pass ProcessInstance to constructor
        {
            CurrentFlowNodeId = targetFlowNodeId
            // We might want to link initiatingExecutionId if needed for tracking/correlation
        };
        processInstance.AddExecution(newExecution); // Ensure this method exists and works correctly

        logger.LogInformation("Created new Execution {NewExecutionId} starting at Node {TargetNodeId}",
                            newExecution.Id, targetFlowNodeId);

        await unitOfWork.SaveChangesAsync(cancellationToken); // Save the new execution

        // Trigger continuation for the newly created execution
        await ContinueExecutionAsync(newExecution.Id, cancellationToken);

        logger.LogDebug("Triggered continuation for new Execution {NewExecutionId} started at node {TargetNodeId}", newExecution.Id, targetFlowNodeId);

        return newExecution; // Return the created execution
    }

    /// <summary>
    /// Searches for and executes an appropriate Error Boundary Event or Error Start Event 
    /// within the current scope or parent scopes.
    /// </summary>
    public async Task<bool> FindAndHandleErrorAsync(Execution failedExecution, Definitions definitions, string errorCode, CancellationToken cancellationToken)
    {
        logger.LogDebug("Searching for error handler for ErrorCode '{ErrorCode}' starting propagation from Execution {ExecutionId}", errorCode, failedExecution.Id);

        var currentSearchExecution = failedExecution;
        var handled = false;
        var executionRepo = unitOfWork.GetRepository<Execution>(); // Get repo instance outside loop

        while (currentSearchExecution != null && !handled)
        {
            var scopeExecution = currentSearchExecution;
            object? scopeElement = null; // Use object for Process or SubProcess
            
            // Find the nearest parent (or self) that IsScope
            while (scopeExecution != null && !scopeExecution.IsScope)
            {
                // TODO: Ensure ParentExecution is loaded efficiently if needed.
                // If ParentExecution is null but ParentExecutionId has value, we need to load it.
                // Example (needs optimization/correct repository method):
                // if (scopeExecution.ParentExecution == null && scopeExecution.ParentExecutionId.HasValue) {
                //     scopeExecution = await unitOfWork.GetRepository<Execution>().GetByIdAsync(scopeExecution.ParentExecutionId.Value);
                // }
                // else {
                      scopeExecution = scopeExecution.ParentExecution; 
                // }
                if (scopeExecution == null && currentSearchExecution.ParentExecutionId.HasValue) {
                     logger.LogWarning("Parent execution object was not loaded during error propagation search for Execution {ExecId}. Stopping search at this level.", currentSearchExecution.Id);
                     // Break or handle based on loading strategy
                     break;
                }
            }

            if (scopeExecution == null) // Reached the top without finding a scope execution (shouldn't happen if root IsScope)
            {
                // Assume we are in the main process context
                scopeElement = definitions.RootElement.OfType<Process>().FirstOrDefault();
                logger.LogDebug("Searching for error handler within main process scope.");
            }
            else
            {
                // Use ScopeFlowNodeId to find the SubProcess element
                var scopeActivityId = scopeExecution.ScopeFlowNodeId; 
                 if (!string.IsNullOrEmpty(scopeActivityId))
                 {
                    scopeElement = definitions.FindFlowElementById<SubProcess>(scopeActivityId); 
                    logger.LogDebug("Searching for error handler within scope of SubProcess {ScopeActivityId}", scopeActivityId);
                 }
                 else
                 {
                     logger.LogWarning("Scope Execution {ScopeExecutionId} is missing ScopeFlowNodeId. Falling back to main process search.", scopeExecution.Id);
                     scopeElement = definitions.RootElement.OfType<Process>().FirstOrDefault();
                 }
            }

            if (scopeElement == null)
            {
                logger.LogError("Could not determine the BPMN scope element (Process/SubProcess) for error handling search. Stopping propagation.");
                break; // Cannot continue search without a scope element
            }
            
            // Define the collection of elements within the current scope to search
            var elementsInScope = Enumerable.Empty<FlowElement>();
            if (scopeElement is Process proc) { elementsInScope = proc.FlowElement ?? elementsInScope; }
            else if (scopeElement is SubProcess sub) { elementsInScope = sub.FlowElement ?? elementsInScope; }


            // 2. Search for Error Boundary Events attached to the scope activity (only if scope is SubProcess)
            if (scopeElement is SubProcess scopeSubProcess)
            {
                var errorBoundaryEvent = definitions.RootElement.OfType<Process>().SelectMany(p => p.FlowElement ?? Enumerable.Empty<FlowElement>()) // Search globally for boundary events
                    .OfType<BoundaryEvent>()
                    .FirstOrDefault(be => be.AttachedToRef?.Name == scopeSubProcess.Id &&
                                           be.EventDefinition.OfType<ErrorEventDefinition>().Any(eed => eed.ErrorRef?.Name == errorCode));
                
                if (errorBoundaryEvent != null)
                {                    
                    logger.LogInformation("Found matching Error Boundary Event {BoundaryEventId} attached to scope {ScopeActivityId}. Handling error.", errorBoundaryEvent.Id, scopeSubProcess.Id);
                    
                    // Terminate the scope execution and its contents
                    var reason = $"Scope interrupted by Error Boundary Event {errorBoundaryEvent.Id}";
                    if (scopeExecution != null) // Ensure scopeExecution is not null
                    {
                        await TerminateScopeContentsAsync(scopeExecution, true, reason, cancellationToken); // Terminate scope itself
                    }
                    else { logger.LogWarning("Scope execution was null when trying to terminate for Error Boundary Event {BoundaryEventId}", errorBoundaryEvent.Id); } 
                    // scopeExecution?.Terminate(reason); // Replaced by TerminateScopeContentsAsync
                    // TODO: Cancel ActivityInstances within the scope. // Handled by TerminateScopeContentsAsync

                    try { await StartExecutionAtNodeAsync(failedExecution.ProcessInstanceId, errorBoundaryEvent.Id, failedExecution.Id, cancellationToken); handled = true; }
                    catch (Exception startEx) { logger.LogError(startEx, "Failed to start execution from Error Boundary Event {BoundaryEventId}", errorBoundaryEvent.Id); handled = false; }
                }
            }

            // 3. Search for Event Subprocesses with matching Error Start Events within the current scope
            if (!handled)
            {
                var eventSubProcesses = elementsInScope.OfType<SubProcess>().Where(sp => sp.TriggeredByEvent); 
                foreach(var esp in eventSubProcesses)
                {
                     var errorStartEvent = esp.FlowElement?.OfType<StartEvent>()
                         .FirstOrDefault(se => se.EventDefinition.OfType<ErrorEventDefinition>().Any(eed => eed.ErrorRef?.Name == errorCode));

                     if (errorStartEvent != null)
                     {
                          logger.LogInformation("Found matching Error Start Event {ErrorStartEventId} in Event Subprocess {EventSubProcessId}. Handling error.", errorStartEvent.Id, esp.Id);
                          
                          // Terminate other active executions within the scope defined by the event subprocess's parent
                          var reason = $"Scope interrupted by Error Start Event {errorStartEvent.Id} in Event Subprocess {esp.Id}";
                          if (scopeExecution != null) // Ensure scopeExecution (parent scope) is not null
                          {
                              await TerminateScopeContentsAsync(scopeExecution, false, reason, cancellationToken); // DO NOT terminate the scope (subprocess) itself
                          }
                          else { logger.LogWarning("Parent scope execution was null when trying to terminate for Error Start Event {ErrorStartEventId}", errorStartEvent.Id); } 
                          // TODO: Terminate other active executions within the scope. // Handled by TerminateScopeContentsAsync
                          
                          try { 
                              await StartExecutionAtNodeAsync(failedExecution.ProcessInstanceId, errorStartEvent.Id, failedExecution.Id, cancellationToken); 
                              handled = true; 
                          }
                          catch (Exception startEx) { 
                              logger.LogError(startEx, "Failed to start execution from Error Start Event {ErrorStartEventId}", errorStartEvent.Id); 
                              handled = false; 
                          }
                          break; // Stop searching Event Subprocesses in this scope once one is found
                     }
                }
            }

            // 4. If not handled, move to the parent scope
            if (!handled)
            {
                Execution? parent = null;
                if (scopeExecution != null)
                {
                    if (scopeExecution.ParentExecution == null && scopeExecution.ParentExecutionId.HasValue)
                    {
                        logger.LogDebug("ParentExecution object is null for ScopeExecution {ScopeExecutionId}, loading ParentExecution {ParentExecutionId}.", scopeExecution.Id, scopeExecution.ParentExecutionId.Value);
                        parent = await executionRepo.GetByIdAsync(scopeExecution.ParentExecutionId.Value, cancellationToken);
                        if (parent == null)
                        {
                             logger.LogError("Failed to load ParentExecution {ParentExecutionId} during error scope traversal.", scopeExecution.ParentExecutionId.Value);
                             // Stop propagation if parent cannot be loaded
                             break; 
                        }
                    }
                    else
                    {
                        parent = scopeExecution.ParentExecution;
                    }
                }
                currentSearchExecution = parent;
            }
        } // End while loop

        // 5. If loop completes and not handled, log final status
        if (!handled)
        {
            logger.LogWarning("No suitable error handler found anywhere in the process hierarchy for ErrorCode '{ErrorCode}'.", errorCode);
            // TODO: Optionally fail the ProcessInstance itself.
        }

        await unitOfWork.SaveChangesAsync(cancellationToken); // Save any state changes made during handling
        return handled; 
    }

    /// <summary>
    /// Searches for and executes an appropriate Escalation Boundary Event or Escalation Start Event.
    /// </summary>
    public async Task<bool> FindAndHandleEscalationAsync(Execution originatingExecution, Definitions definitions, string escalationCode, string? escalatedAtNodeId, CancellationToken cancellationToken)
    {
        logger.LogDebug("Searching for escalation handler for EscalationCode '{EscalationCode}' starting propagation from Execution {ExecutionId}. Escalated at node: {EscalatedAtNodeId}", 
                        escalationCode, originatingExecution.Id, escalatedAtNodeId ?? "N/A");

        var currentSearchExecution = originatingExecution;
        var handled = false;
        var executionRepo = unitOfWork.GetRepository<Execution>(); // Get repo instance outside loop

        while (currentSearchExecution != null && !handled)
        {
            var scopeExecution = currentSearchExecution;
            object? scopeElement = null; // Use object for Process or SubProcess
            
            // Find the nearest parent (or self) that IsScope
            while (scopeExecution != null && !scopeExecution.IsScope)
            {
                // TODO: Ensure ParentExecution is loaded efficiently if needed.
                scopeExecution = scopeExecution.ParentExecution; 
                if (scopeExecution == null && currentSearchExecution.ParentExecutionId.HasValue) {
                     logger.LogWarning("Parent execution object was not loaded during escalation propagation search for Execution {ExecId}. Stopping search at this level.", currentSearchExecution.Id);
                     // Break or handle based on loading strategy
                     break;
                }
            }

            if (scopeExecution == null) // Reached the top without finding a scope execution
            {
                // Assume we are in the main process context
                scopeElement = definitions.RootElement.OfType<Process>().FirstOrDefault();
                logger.LogDebug("Searching for escalation handler within main process scope.");
            }
            else
            {
                // Use ScopeFlowNodeId to find the SubProcess element
                var scopeActivityId = scopeExecution.ScopeFlowNodeId; 
                if (!string.IsNullOrEmpty(scopeActivityId))
                {
                    scopeElement = definitions.FindFlowElementById<SubProcess>(scopeActivityId);
                    // Corrected log message
                    logger.LogDebug("Searching for escalation handler within scope of SubProcess {ScopeActivityId}", scopeActivityId);
                }
                else
                {
                    logger.LogWarning("Scope Execution {ScopeExecutionId} is missing ScopeFlowNodeId. Falling back to main process search.", scopeExecution.Id);
                    scopeElement = definitions.RootElement.OfType<Process>().FirstOrDefault();
                }
            }

            if (scopeElement == null) { /* log error, break loop */ break; }

            var elementsInScope = Enumerable.Empty<FlowElement>();
            if (scopeElement is Process proc) { elementsInScope = proc.FlowElement ?? elementsInScope; }
            else if (scopeElement is SubProcess sub) { elementsInScope = sub.FlowElement ?? elementsInScope; }
            
            // 2. Search for Escalation Boundary Events attached to the scope activity (only if scope is SubProcess)
            if (scopeElement is SubProcess scopeSubProcess)
            {
                var escalationBoundaryEvent = definitions.RootElement.OfType<Process>().SelectMany(p => p.FlowElement ?? Enumerable.Empty<FlowElement>()) // Search globally for boundary events
                    .OfType<BoundaryEvent>()
                    .FirstOrDefault(be => be.AttachedToRef?.Name == scopeSubProcess.Id &&
                                           be.EventDefinition.OfType<EscalationEventDefinition>().Any(eed => eed.EscalationRef?.Name == escalationCode));
                
                if (escalationBoundaryEvent != null)
                {                    
                    logger.LogInformation("Found matching Escalation Boundary Event {BoundaryEventId} attached to scope {ScopeActivityId}. Handling escalation.", escalationBoundaryEvent.Id, scopeSubProcess.Id);
                    
                    // Terminate the scope execution and its contents (Escalation Boundary Events are always interrupting)
                    var reason = $"Scope interrupted by Escalation Boundary Event {escalationBoundaryEvent.Id}";
                    if (scopeExecution != null) // Ensure scopeExecution is not null
                    {
                        await TerminateScopeContentsAsync(scopeExecution, true, reason, cancellationToken); // Terminate scope itself
                    }
                    else { logger.LogWarning("Scope execution was null when trying to terminate for Escalation Boundary Event {BoundaryEventId}", escalationBoundaryEvent.Id); }
                    // scopeExecution?.Terminate(reason); // Replaced by TerminateScopeContentsAsync
                    // TODO: Cancel ActivityInstances within the scope. // Handled by TerminateScopeContentsAsync

                    try { await StartExecutionAtNodeAsync(originatingExecution.ProcessInstanceId, escalationBoundaryEvent.Id, originatingExecution.Id, cancellationToken); handled = true; }
                    catch (Exception startEx) { logger.LogError(startEx, "Failed to start execution from Escalation Boundary Event {BoundaryEventId}", escalationBoundaryEvent.Id); handled = false; }
                }
            }

            // 3. Search for Event Subprocesses with matching Escalation Start Events within the current scope
            if (!handled)
            {
                var eventSubProcesses = elementsInScope.OfType<SubProcess>().Where(sp => sp.TriggeredByEvent); 
                foreach(var esp in eventSubProcesses)
                {
                     var escalationStartEvent = esp.FlowElement?.OfType<StartEvent>()
                         .FirstOrDefault(se => se.EventDefinition.OfType<EscalationEventDefinition>().Any(eed => eed.EscalationRef?.Name == escalationCode));
                      
                      if (escalationStartEvent != null)
                      {
                           var isInterrupting = escalationStartEvent.IsInterrupting; 
                           logger.LogInformation("Found matching Escalation Start Event {StartEventId} in Event Subprocess {EventSubProcessId} (Interrupting: {IsInterrupting}). Handling escalation.", 
                                                 escalationStartEvent.Id, esp.Id, isInterrupting);
                           
                           // If interrupting, terminate other active executions within the scope
                           if (isInterrupting)
                           {
                               var reason = $"Scope interrupted by Escalation Start Event {escalationStartEvent.Id} in Event Subprocess {esp.Id}";
                               if (scopeExecution != null) // Ensure scopeExecution (parent scope) is not null
                               {
                                    await TerminateScopeContentsAsync(scopeExecution, false, reason, cancellationToken); // DO NOT terminate the scope itself
                               }
                               else { logger.LogWarning("Parent scope execution was null when trying to terminate for interrupting Escalation Start Event {EscalationStartEventId}", escalationStartEvent.Id); }
                           }
                           // TODO: If interrupting, terminate other active executions. // Handled by TerminateScopeContentsAsync
                           
                           try { 
                               await StartExecutionAtNodeAsync(originatingExecution.ProcessInstanceId, escalationStartEvent.Id, originatingExecution.Id, cancellationToken); 
                               handled = true; 
                           }
                           catch (Exception startEx) { 
                               logger.LogError(startEx, "Failed to start execution from Escalation Start Event {StartEventId}", escalationStartEvent.Id); 
                               handled = false; 
                           }
                           break; // Stop searching Event Subprocesses in this scope once one is found
                      }
                }
            }

            // 4. If not handled, move to the parent scope for the NEXT iteration
            if (!handled)
            {                
                Execution? nextParent = null;
                if (scopeExecution != null) // Use the scopeExecution found for this iteration
                {
                     if (scopeExecution.ParentExecution == null && scopeExecution.ParentExecutionId.HasValue)
                     {
                         logger.LogDebug("ParentExecution object is null for ScopeExecution {ScopeExecutionId}, loading ParentExecution {ParentExecutionId} for next iteration.", scopeExecution.Id, scopeExecution.ParentExecutionId.Value);
                         nextParent = await executionRepo.GetByIdAsync(scopeExecution.ParentExecutionId.Value, cancellationToken);
                         if (nextParent == null)
                         {
                              logger.LogError("Failed to load ParentExecution {ParentExecutionId} for next iteration.", scopeExecution.ParentExecutionId.Value);
                              // Stop propagation upwards if parent cannot be loaded
                         }
                     }
                     else
                     {
                         nextParent = scopeExecution.ParentExecution;
                     }
                }
                currentSearchExecution = nextParent; // Set for the next loop iteration
            }
        } // End while loop

        // ... final log and return ...
        return handled; 
    }

    /// <summary>
    /// Terminates all active executions and cancels active ActivityInstances within a given scope,
    /// typically triggered by an interrupting boundary or event subprocess handler.
    /// </summary>
    /// <param name="scopeExecution">The execution defining the scope (e.g., the scope execution of a SubProcess).</param>
    /// <param name="terminateScopeItself">If true, the scopeExecution itself will also be terminated.</param>
    /// <param name="reason">Reason for termination.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task TerminateScopeContentsAsync(Execution scopeExecution, bool terminateScopeItself, string reason, CancellationToken cancellationToken)
    {
        if (scopeExecution == null)
        {
            logger.LogWarning("TerminateScopeContentsAsync called with null scopeExecution.");
            return;
        }

        logger.LogDebug("Terminating contents of scope defined by Execution {ScopeExecutionId}. Terminate scope itself: {TerminateScopeItself}. Reason: {Reason}",
                       scopeExecution.Id, terminateScopeItself, reason);

        var executionRepo = unitOfWork.GetRepository<Execution>();
        var activityInstanceRepo = unitOfWork.GetRepository<ActivityInstance>();

        // 1. Find all executions within the process instance to build the hierarchy
        // Consider including related entities if lazy loading is disabled or unreliable
        var allExecutions = await executionRepo.ListAsync(e => e.ProcessInstanceId == scopeExecution.ProcessInstanceId, cancellationToken);

        var executionMap = allExecutions.ToDictionary(e => e.Id);
        var childrenMap = allExecutions
            .Where(e => e.ParentExecutionId.HasValue)
            .GroupBy(e => e.ParentExecutionId!.Value)
            .ToDictionary(g => g.Key, g => g.ToList());

        var executionsToTerminateIds = new HashSet<Guid>();
        var activeDescendantsToTerminate = new List<Execution>();
        var queue = new Queue<Guid>(); // Use Guid in queue for simplicity

        // Add initial children of the scope execution ID to the queue
        if (childrenMap.TryGetValue(scopeExecution.Id, out var directChildren))
        {
            foreach (var child in directChildren)
            {
                queue.Enqueue(child.Id);
            }
        }

        // Breadth-first search to find all active descendant IDs
        while (queue.Count > 0)
        {
            var currentId = queue.Dequeue();
            
            // Ensure the execution exists in our map and hasn't been processed
            if (executionMap.TryGetValue(currentId, out var currentExecution) && executionsToTerminateIds.Add(currentId))
            {
                if (currentExecution.IsActive)
                {
                    activeDescendantsToTerminate.Add(currentExecution); // Add the actual execution object
                    // Add its children to the queue
                    if (childrenMap.TryGetValue(currentId, out var grandchildren))
                    {
                        foreach (var grandchild in grandchildren)
                        {
                            queue.Enqueue(grandchild.Id);
                        }
                    }
                }
            }
        }

        // 2. Terminate the active descendant executions found
        foreach (var execToTerminate in activeDescendantsToTerminate)
        {
            logger.LogInformation("Terminating descendant Execution {ExecutionId} due to scope interruption. Reason: {Reason}", execToTerminate.Id, reason);
            execToTerminate.Terminate(reason);
        }

        // 3. Optionally terminate the scope execution itself
        // Get the tracked instance from the map again
        if (executionMap.TryGetValue(scopeExecution.Id, out var actualScopeExecution))
        {
            if (terminateScopeItself && actualScopeExecution.IsActive)
            {
                 logger.LogInformation("Terminating scope Execution {ScopeExecutionId} itself. Reason: {Reason}", actualScopeExecution.Id, reason);
                 actualScopeExecution.Terminate(reason);
                 executionsToTerminateIds.Add(actualScopeExecution.Id); // Ensure scope ID is in the set
            }
            else if (!executionsToTerminateIds.Contains(actualScopeExecution.Id)) // Add scope ID if not already added (e.g., if it wasn't active but descendants were)
            {
                 executionsToTerminateIds.Add(actualScopeExecution.Id);
            }
        }
        else
        {
             logger.LogWarning("Could not find the scope execution {ScopeExecutionId} in the loaded map during termination.", scopeExecution.Id);
             // Add the original scope ID just in case
             executionsToTerminateIds.Add(scopeExecution.Id);
        }

        // 4. Find and Cancel related Active Activity Instances
        if (executionsToTerminateIds.Count > 0)
        {
            // Fetch only potentially relevant activities
            var activeActivities = await activityInstanceRepo.ListAsync(
                ai => ai.ProcessInstanceId == scopeExecution.ProcessInstanceId 
                      && ai.EndTime == null 
                      && ai.ExecutionId.HasValue 
                      && executionsToTerminateIds.Contains(ai.ExecutionId.Value), // Filter by terminated execution IDs
                cancellationToken);

            if (activeActivities.Any())
            {
                logger.LogInformation("Canceling {Count} active ActivityInstances associated with terminated executions in scope {ScopeExecutionId}.",
                                     activeActivities.Count, scopeExecution.Id);
                var utcNow = DateTime.UtcNow; // Use consistent time
                foreach (var activityToCancel in activeActivities)
                {
                    // Consider adding a 'State' field like 'Canceled' to ActivityInstance?
                    // For now, just mark as ended.
                    activityToCancel.EndTime = utcNow;
                }
            }
            else
            {
                logger.LogDebug("No active ActivityInstances found associated with the terminated executions in scope {ScopeExecutionId}.", scopeExecution.Id);
            }
        }

        // 5. Save all changes (Executions terminated, ActivityInstances ended)
        // SaveChangesAsync might be called by the caller (e.g., FindAndHandleErrorAsync), 
        // but saving here ensures changes within this method are persisted.
        // Consider if double-saving is an issue or if the caller should always save.
        // Let's save here for atomicity of this operation.
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogDebug("Finished terminating contents for scope {ScopeExecutionId}. SaveChanges called.", scopeExecution.Id);
    }

    public async Task TriggerBoundaryTimerEventAsync(EventSubscription subscription, bool cancelActivity, CancellationToken cancellationToken)
    {
        Guid? originatingExecutionId = subscription.ExecutionId;
        var boundaryEventNodeId = subscription.ActivityId;
        var attachedToActivityId = subscription.AttachedToActivityId;
        var processInstanceId = subscription.ProcessInstanceId;

        logger.LogInformation("Boundary Timer Event triggered. Subscription: {SubscriptionId}, BoundaryNode: {BoundaryNodeId}, AttachedTo: {AttachedToActivityId}, Interrupting: {IsInterrupting}, OriginatingExecution: {OriginatingExecutionId}",
                            subscription.Id, boundaryEventNodeId, attachedToActivityId ?? "N/A", cancelActivity, originatingExecutionId?.ToString() ?? "N/A");

        if (originatingExecutionId == null || string.IsNullOrEmpty(attachedToActivityId))
        {            
            // Loglamayı biraz daha detaylandıralım
            logger.LogError("Cannot trigger boundary timer event for Subscription {SubscriptionId} on ProcessInstance {ProcessInstanceId}: Originating Execution ID ({OriginatingExecutionId}) or AttachedToActivityId ('{AttachedToActivityId}') is missing. BoundaryNode: {BoundaryNodeId}. Subscription will be ignored.", 
                            subscription.Id, processInstanceId, originatingExecutionId?.ToString() ?? "NULL", attachedToActivityId ?? "NULL", boundaryEventNodeId);
            
            // TODO: Implement an Incident creation mechanism to record this configuration error. For now, ignoring the subscription trigger.
            // Eski TODO: Handle this error - maybe delete subscription or create incident?
            return;
        }

        var executionRepo = unitOfWork.GetRepository<Execution>();
        var activityInstanceRepo = unitOfWork.GetRepository<ActivityInstance>();
        var processInstanceRepo = unitOfWork.GetRepository<ProcessInstance>();

        // Load process instance and definition
        var processInstance = await processInstanceRepo.GetByIdAsync(processInstanceId, cancellationToken);
        if (processInstance == null) { /* Log error and return */ return; }

        try
        {
            var xmlContent = await GetProcessDefinitionXmlAsync(processInstance.ProcessDefinitionId, cancellationToken);
            var definitions = bpmnXmlService.Import(xmlContent);
        }
        catch (Exception ex) { /* Log error and return */ return; }

        try
        {
            if (cancelActivity) // --- Interrupting Logic --- 
            {
                logger.LogDebug("Handling interrupting boundary timer for AttachedActivity {AttachedActivityId}", attachedToActivityId);

                // 1. Find the active execution(s) currently AT the activity the timer is attached to.
                // This execution might be different from the originatingExecutionId if multi-instance or complex flows are involved.
                var executionsOnActivity = await executionRepo.ListAsync(e => 
                    e.ProcessInstanceId == processInstanceId && 
                    e.CurrentFlowNodeId == attachedToActivityId && 
                    e.IsActive,
                    cancellationToken);
                
                if (!executionsOnActivity.Any())
                {
                    logger.LogWarning("Interrupting boundary timer {BoundaryNodeId} triggered, but no active execution found on attached activity {AttachedActivityId}. Subscription {SubscriptionId} might be obsolete or flow already progressed.",
                                      boundaryEventNodeId, attachedToActivityId, subscription.Id);
                    // Optional: Clean up the subscription here?
                    return; // Nothing to interrupt
                }
                
                // For simplicity, let's assume we interrupt ALL active executions on that node.
                // More complex logic might be needed for specific multi-instance scenarios.
                foreach (var executionToInterrupt in executionsOnActivity)
                {
                    var reason = $"Interrupted by Boundary Timer Event {boundaryEventNodeId}";
                    logger.LogInformation("Interrupting Execution {ExecutionId} on Activity {ActivityId}. Reason: {Reason}", 
                                        executionToInterrupt.Id, attachedToActivityId, reason);
                    
                    // Terminate the execution
                    executionToInterrupt.Terminate(reason);
                    
                    // Cancel the corresponding ActivityInstance
                    var activityInstance = await activityInstanceRepo.FirstOrDefaultAsync(
                        ai => ai.ExecutionId == executionToInterrupt.Id && 
                              ai.FlowNodeId == attachedToActivityId && 
                              ai.EndTime == null, cancellationToken);
                    if (activityInstance != null)
                    {
                        activityInstance.Cancel(); // Assuming Cancel() method exists or just set EndTime
                        logger.LogDebug("Cancelled ActivityInstance {ActivityInstanceId} for interrupted execution {ExecutionId}.", activityInstance.Id, executionToInterrupt.Id);
                    }
                    else { logger.LogWarning("Could not find active ActivityInstance for interrupted Execution {ExecutionId} on Activity {ActivityId}", executionToInterrupt.Id, attachedToActivityId); }
                }
                
                // 2. Start new execution from the boundary event node.
                // Use the *original* execution ID from the subscription as the initiator for tracking purposes.
                logger.LogDebug("Starting new execution from Interrupting Boundary Event {BoundaryNodeId}", boundaryEventNodeId);
                await StartExecutionAtNodeAsync(processInstanceId, boundaryEventNodeId, originatingExecutionId, cancellationToken);
            }
            else // --- Non-interrupting Logic --- 
            {
                logger.LogDebug("Handling non-interrupting boundary timer for AttachedActivity {AttachedActivityId}", attachedToActivityId);
                
                // 1. Execution(s) on the attached activity continue normally.
                // 2. Start new parallel execution from the boundary event node.
                // Use the execution ID from the subscription as the initiator.
                logger.LogDebug("Starting new parallel execution from Non-Interrupting Boundary Event {BoundaryNodeId}", boundaryEventNodeId);
                await StartExecutionAtNodeAsync(processInstanceId, boundaryEventNodeId, originatingExecutionId, cancellationToken);
            }

            // Save changes (execution termination, activity cancellation)
            await unitOfWork.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Finished processing Boundary Timer Event trigger for Subscription {SubscriptionId}", subscription.Id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error processing Boundary Timer Event trigger for Subscription {SubscriptionId}", subscription.Id);
            // What happens on error? State might be inconsistent. Maybe create incident?
        }
    }
}