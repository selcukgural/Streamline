using MediatR;
using Microsoft.Extensions.Logging;
using Streamline.Domain.Abstractions; // For IRepository
using Streamline.Domain.Events; // For ActivityFailedEvent
using Streamline.Domain.Runtime; // For Incident, ProcessInstance

namespace Streamline.Application.Features.Incidents.EventHandlers;

/// <summary>
/// Handles the ActivityFailedEvent to create a corresponding Incident record.
/// </summary>
public class ActivityFailedEventHandler(
    IRepository<Incident> incidentRepository,
    IRepository<ProcessInstance> processInstanceRepository,
    ILogger<ActivityFailedEventHandler> logger)
    : INotificationHandler<ActivityFailedEvent>
{
    private const string IncidentType = "ActivityFailed";

    public async Task Handle(ActivityFailedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling ActivityFailedEvent for ActivityInstanceId: {ActivityInstanceId}", notification.ActivityInstanceId);

        // Fetch the ProcessInstance to get the ProcessDefinitionId
        var processInstance = await processInstanceRepository.GetByIdAsync(notification.ProcessInstanceId, cancellationToken);

        if (processInstance == null)
        {
            logger.LogError("ProcessInstance with ID {ProcessInstanceId} not found for ActivityFailedEvent from ActivityInstanceId {ActivityInstanceId}. Cannot create Incident.", 
                notification.ProcessInstanceId, notification.ActivityInstanceId);
            return; // Stop processing if related process instance is missing
        }

        if (string.IsNullOrEmpty(processInstance.ProcessDefinitionId)) // Ensure ProcessDefinitionId exists
        {
             logger.LogError("ProcessDefinitionId is null or empty for ProcessInstance with ID {ProcessInstanceId}. Cannot create Incident for ActivityInstanceId {ActivityInstanceId}.", 
                notification.ProcessInstanceId, notification.ActivityInstanceId);
            return; // Stop processing if definition ID is missing
        }
        
        if (notification.ExecutionId == null)
        {
            logger.LogWarning("Cannot create Incident for ActivityInstanceId {ActivityInstanceId} because ExecutionId is null.", notification.ActivityInstanceId);
            // Depending on requirements, maybe create a different type of incident or log differently.
            return;
        }

        var incident = new Incident(
            incidentType: IncidentType, // Define a standard type
            executionId: notification.ExecutionId.Value, // We checked for null above
            processInstanceId: notification.ProcessInstanceId,
            activityId: notification.FlowNodeId,
            processDefinitionId: processInstance.ProcessDefinitionId, // Use the fetched ID
            message: notification.ErrorMessage ?? "Activity failed without a specific message.",
            details: notification.ErrorDetails,
            jobId: null,
            tenantId: null
        );

        try
        {
            await incidentRepository.AddAsync(incident, cancellationToken);
            // IMPORTANT: Do NOT call SaveChangesAsync here. The UnitOfWork/DbContext dispatch mechanism handles it.
            logger.LogInformation("Incident {IncidentId} created successfully for ActivityInstanceId: {ActivityInstanceId}", incident.Id, notification.ActivityInstanceId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to create Incident for ActivityInstanceId: {ActivityInstanceId}", notification.ActivityInstanceId);
            // Consider re-throwing, handling compensation, or other error strategies
            throw;
        }
    }
} 