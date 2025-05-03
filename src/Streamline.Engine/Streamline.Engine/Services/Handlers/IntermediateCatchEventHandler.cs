using Microsoft.Extensions.Logging;
using Streamline.Domain.Schema.Events; // For IntermediateCatchEvent
using Streamline.Domain.Schema.Common;
using Streamline.Domain.Runtime; // Added for EventSubscription
using Streamline.Engine.Abstractions;
using Streamline.Domain.Abstractions; // Changed from Application.Abstractions
// For parsing
// Added for XmlConvert
using NodaTime; // Added NodaTime
using NodaTime.Text; // Added for parsing

namespace Streamline.Engine.Services.Handlers;

/// <summary>
/// Handles Intermediate Catch Events based on their event definition.
/// Creates Event Subscriptions for waiting events like Signal, Message, Timer.
/// </summary>
public class IntermediateCatchEventHandler(
    ILogger<IntermediateCatchEventHandler> logger,
    ITimerJobScheduler timerJobScheduler // Now using ITimerJobScheduler from Domain
    ) : IFlowNodeHandler<IntermediateCatchEvent>
{
    public async Task ExecuteAsync(FlowNode node, FlowNodeHandlerContext context, CancellationToken cancellationToken)
    {
        if (node is not IntermediateCatchEvent catchEvent)
        {
            logger.LogError("Node type mismatch. Expected IntermediateCatchEvent but got {NodeType} for Node {NodeId}", node.GetType().Name, node.Id);
            context.Execution.Fail("Incorrect node type passed to IntermediateCatchEventHandler");
            await context.UnitOfWork.SaveChangesAsync(cancellationToken);
            return;
        }

        logger.LogInformation("Executing IntermediateCatchEvent handler for Node {NodeId} ({NodeName}) - Execution {ExecutionId}", 
            catchEvent.Id, catchEvent.Name ?? "Unnamed IntermediateCatchEvent", context.Execution.Id);

        var eventDefinition = catchEvent.EventDefinition.FirstOrDefault();
        var subscriptionRepo = context.UnitOfWork.GetRepository<EventSubscription>();
        string? eventName = null;
        string? eventType = null;
        string? configuration = null;
        string? timerDefinitionValue = null; // Extracted value
        string? timerDefinitionType = null;  // Extracted type (TimeDate, TimeDuration, TimeCycle)
        var shouldWait = true; // Assume waiting unless it's a Link event

        switch (eventDefinition)
        {
            case LinkEventDefinition linkDef:
                logger.LogInformation("IntermediateCatchEvent {NodeId} is a Link Event (Name: '{LinkName}'). Continuing flow.", 
                                   catchEvent.Id, linkDef.Name ?? "(No Name)");
                shouldWait = false; // Link events don't wait
                break;

            case SignalEventDefinition signalDef:
                eventType = "Signal";
                eventName = signalDef.SignalRef?.Name;
                if (string.IsNullOrWhiteSpace(eventName))
                {
                    logger.LogError("SignalEventDefinition for Catch Event {NodeId} is missing required Signal name reference.", catchEvent.Id);
                    context.Execution.Fail("Signal Catch Event is missing Signal name.");
                    await context.UnitOfWork.SaveChangesAsync(cancellationToken);
                    return; // Stop processing this handler
                }
                logger.LogInformation("IntermediateCatchEvent {NodeId} is waiting for Signal '{SignalName}'.", catchEvent.Id, eventName);
                break;

            case MessageEventDefinition msgDef:
                 eventType = "Message";
                 eventName = msgDef.MessageRef?.Name;
                 if (string.IsNullOrWhiteSpace(eventName))
                 {
                     logger.LogError("MessageEventDefinition for Catch Event {NodeId} is missing required Message name reference.", catchEvent.Id);
                     context.Execution.Fail("Message Catch Event is missing Message name.");
                     await context.UnitOfWork.SaveChangesAsync(cancellationToken);
                     return; // Stop processing
                 }
                 // TODO: Extract correlation key information if defined in the model (e.g., via extensions or data associations)
                 // configuration = ExtractCorrelationInfo(...);
                 logger.LogInformation("IntermediateCatchEvent {NodeId} is waiting for Message '{MessageName}'. Correlation logic TBD.", catchEvent.Id, eventName);
                 break;
                 
            case TimerEventDefinition timeDef:
                 eventType = "Timer";
                 eventName = catchEvent.Id; 
                 
                 // --- NodaTime Parsing --- 
                 try 
                 {
                     if (timeDef.TimeDate?.Text?.FirstOrDefault() != null) {
                         timerDefinitionValue = timeDef.TimeDate.Text.First();
                         timerDefinitionType = "TimeDate";
                         // Use NodaTime for robust ISO 8601 DateTime parsing
                         var parseResult = InstantPattern.ExtendedIso.Parse(timerDefinitionValue);
                         if (!parseResult.Success) {
                             throw parseResult.Exception; // Throw parsing exception
                         }
                     }
                     else if (timeDef.TimeDuration?.Text?.FirstOrDefault() != null) {
                         timerDefinitionValue = timeDef.TimeDuration.Text.First();
                         timerDefinitionType = "TimeDuration";
                         // Use NodaTime for robust ISO 8601 Duration parsing
                         var parseResult = DurationPattern.Roundtrip.Parse(timerDefinitionValue);
                         if (!parseResult.Success) {
                             throw parseResult.Exception;
                         }
                     }
                     else if (timeDef.TimeCycle?.Text?.FirstOrDefault() != null) {
                         timerDefinitionValue = timeDef.TimeCycle.Text.First();
                         timerDefinitionType = "TimeCycle";
                         // NodaTime doesn't have direct ISO cycle parsing. 
                         // We store the string and expect the scheduler to handle full parsing.
                         // Basic validation remains.
                         if (!timerDefinitionValue.Contains('/') || !timerDefinitionValue.StartsWith("R")) {
                            logger.LogWarning("TimeCycle format '{TimerValue}' for Catch Event {NodeId} does not appear to be a standard ISO 8601 repeating interval.", timerDefinitionValue, catchEvent.Id);
                         }
                     }
                     else {
                         throw new InvalidOperationException("Timer definition (TimeDate/TimeDuration/TimeCycle) is missing.");
                     }
                 }
                 catch (Exception ex) // Catch parsing errors from NodaTime or validation
                 {
                      logger.LogError(ex, "Invalid timer definition format for Catch Event {NodeId}. Value: '{TimerValue}'", catchEvent.Id, timerDefinitionValue ?? "(null)");
                      context.Execution.Fail("Invalid Timer definition format.");
                      await context.UnitOfWork.SaveChangesAsync(cancellationToken);
                      return; // Stop processing
                 }
                 // ------------------------
                 
                 configuration = $"{timerDefinitionType}:{timerDefinitionValue}"; 
                 logger.LogInformation("IntermediateCatchEvent {NodeId} is waiting for a Timer ({TimerType}: {TimerValue}).", 
                                    catchEvent.Id, timerDefinitionType, timerDefinitionValue);
                 break;
                 
            case null: // None Intermediate Catch Event - effectively a wait state without specific trigger
            default: // Includes unknown/unhandled event definition types
                 var defTypeName = eventDefinition?.GetType().Name ?? "None";
                 eventType = defTypeName;
                 eventName = catchEvent.Id;
                 logger.LogWarning("IntermediateCatchEvent {NodeId} is a {DefinitionType} type. Treating as a generic wait state. Requires specific implementation or trigger mechanism.", 
                                catchEvent.Id, defTypeName, context.Execution.Id);
                 break;
        }

        if (shouldWait)
        {
            if (!string.IsNullOrEmpty(eventType) && !string.IsNullOrEmpty(eventName))
            {
                 if (context.Execution == null) 
                 {
                      logger.LogError("Execution context is null, cannot create EventSubscription for {NodeId}", catchEvent.Id);
                      return; 
                 }
                 
                 // --- Calculate Initial Due Time (using NodaTime where applicable) --- 
                 DateTime? initialDueTime = null;
                 if (eventType == "Timer") 
                 {
                     try 
                     {
                         if (timerDefinitionType == "TimeDate") 
                         {
                             // Parse validated string using NodaTime
                             var dueInstant = InstantPattern.ExtendedIso.Parse(timerDefinitionValue!).Value;
                             initialDueTime = dueInstant.ToDateTimeUtc();
                         }
                         else if (timerDefinitionType == "TimeDuration")
                         {
                             // Parse validated string using NodaTime
                             var duration = DurationPattern.Roundtrip.Parse(timerDefinitionValue!).Value;
                             var now = SystemClock.Instance.GetCurrentInstant();
                             initialDueTime = (now + duration).ToDateTimeUtc();
                         }
                         else if (timerDefinitionType == "TimeCycle")
                         {
                             // Basic calculation for first interval
                             var parts = timerDefinitionValue!.Split('/');
                             if (parts.Length >= 2) 
                             {
                                 // Use NodaTime to parse the duration part
                                 var interval = DurationPattern.Roundtrip.Parse(parts[1]).Value; 
                                 var now = SystemClock.Instance.GetCurrentInstant();
                                 initialDueTime = (now + interval).ToDateTimeUtc();
                             } else {
                                 throw new FormatException("Invalid TimeCycle format for initial due time calculation.");
                             }
                         }
                     }
                     catch (Exception ex)
                     {
                         logger.LogError(ex, "Failed to calculate initial due time from timer configuration '{Config}' for node {NodeId}.", configuration, catchEvent.Id);
                         context.Execution.Fail("Invalid Timer configuration for scheduling.");
                         await context.UnitOfWork.SaveChangesAsync(cancellationToken);
                         return; // Stop processing
                     }
                 }
                 // ------------------------------------------------------------------

                 if (eventType == "Timer" && initialDueTime == null)
                 {
                      logger.LogError("Could not determine initial due time for timer {NodeId} with config {Config}", catchEvent.Id, configuration);
                      context.Execution.Fail("Failed to calculate timer due time.");
                      await context.UnitOfWork.SaveChangesAsync(cancellationToken);
                      return; // Stop processing
                 }

                 var subscription = new EventSubscription(
                     eventType: eventType,
                     eventName: eventName,
                     executionId: context.Execution.Id,
                     execution: context.Execution, 
                     processInstanceId: context.Execution.ProcessInstanceId, 
                     activityId: catchEvent.Id,
                     configuration: configuration
                 );
                 await subscriptionRepo.AddAsync(subscription, cancellationToken);
                 // Save subscription FIRST to get its ID before scheduling (important if scheduler needs it)
                 await context.UnitOfWork.SaveChangesAsync(cancellationToken); 
                 
                 logger.LogInformation("Created EventSubscription {SubscriptionId} for {EventType} '{EventName}'. Config: {Config}", 
                                     subscription.Id, eventType, eventName, configuration ?? "N/A");
                                                     
                 // --- Trigger External Scheduling --- 
                 if (eventType == "Timer") 
                 {                      
                      try
                      {
                          var jobId = await timerJobScheduler.ScheduleTimerJobAsync(subscription.Id, initialDueTime.Value);
                          logger.LogInformation("Scheduled timer job {JobId} for Subscription {SubscriptionId} to fire at {DueTimeUTC}", jobId, subscription.Id, initialDueTime.Value);
                          
                          // Store Job ID in subscription and update
                          subscription.JobId = jobId;
                          await subscriptionRepo.UpdateAsync(subscription, cancellationToken); 
                          await context.UnitOfWork.SaveChangesAsync(cancellationToken); // Save JobId update
                          logger.LogDebug("Stored JobId {JobId} in Subscription {SubscriptionId}", jobId, subscription.Id);
                      }
                      catch (Exception ex) 
                      {
                           logger.LogError(ex, "Failed to schedule timer job or store JobId for Subscription {SubscriptionId}", subscription.Id);
                           // Fail the execution if scheduling is critical
                           context.Execution.Fail("Failed to schedule timer job.");
                           await context.UnitOfWork.SaveChangesAsync(cancellationToken);
                           return; // Stop processing as timer cannot be set
                      }
                 }
            }

            context.Execution.ArriveAtConvergingGateway(catchEvent.Id); 
            logger.LogDebug("Execution {ExecutionId} is now waiting at {NodeId} for a {EventType} event.", context.Execution.Id, catchEvent.Id, eventType ?? "trigger");
        }
        else
        {
            context.Execution.Reactivate();
            context.Execution.LeaveConvergingGateway();
            await context.UnitOfWork.SaveChangesAsync(cancellationToken);
            await context.ExecutionFlowManager.ContinueExecutionAsync(context.Execution.Id, cancellationToken);
        }
    }
} 