namespace Streamline.Domain.Runtime;

/// <summary>
/// Represents a runtime incident that occurred during process execution,
/// often related to a failed job or an unhandled error.
/// </summary>
public sealed class Incident : EntityBase
{
    /// <summary>
    /// Type of incident (e.g., "failedJob", "configurationError", "unhandledEscalation").
    /// </summary>
    public string IncidentType { get; init; } = null!;

    /// <summary>
    /// Timestamp when the incident occurred.
    /// </summary>
    public DateTime IncidentTimestamp { get; init; }

    /// <summary>
    /// ID of the Execution where the incident occurred.
    /// </summary>
    public Guid ExecutionId { get; init; }
    public Execution Execution { get; init; } // Navigation property

    /// <summary>
    /// ID of the Process Instance where the incident occurred.
    /// </summary>
    public Guid ProcessInstanceId { get; init; }
    // No direct navigation needed

    /// <summary>
    /// ID of the activity (Flow Node) where the incident is rooted.
    /// </summary>
    public string ActivityId { get; init; }

    /// <summary>
    /// ID of the specific Job that failed, if applicable.
    /// </summary>
    public Guid? JobId { get; init; }
    public Job? Job { get; init; } // Navigation property
        
    /// <summary>
    /// ID of the process definition.
    /// </summary>
    public string ProcessDefinitionId { get; init; }

    /// <summary>
    /// A message describing the incident.
    /// </summary>
    public string Message { get; init; } = null!;

    /// <summary>
    /// Detailed information, potentially a stack trace or configuration details.
    /// </summary>
    public string? Details { get; init; }
        
    /// <summary>
    /// Optional: Tenant ID if using multi-tenancy.
    /// </summary>
    public string? TenantId { get; init; }

    // Private constructor for EF Core
    private Incident() 
    { 
        // No explicit initialization needed with init setters
        // IncidentType = null!;
        // Execution = null!;
        // ActivityId = null!;
        // ProcessDefinitionId = null!;
        // Message = null!;
    }

    // Public constructor
    public Incident(string incidentType, Guid executionId, Guid processInstanceId, string activityId, string processDefinitionId, string message, Guid? jobId = null, string? details = null, string? tenantId = null)
    {
        IncidentType = incidentType;
        ExecutionId = executionId;
        ProcessInstanceId = processInstanceId;
        ActivityId = activityId;
        ProcessDefinitionId = processDefinitionId;
        Message = message;
        JobId = jobId;
        Details = details;
        TenantId = tenantId;
        IncidentTimestamp = DateTime.UtcNow;
        // Execution and Job properties will be set by EF Core or explicitly if needed.
    }
}