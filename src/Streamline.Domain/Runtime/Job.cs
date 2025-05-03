namespace Streamline.Domain.Runtime;

/// <summary>
/// Represents an executable job, typically for asynchronous tasks or timers.
/// Handled by a separate Job Executor component.
/// </summary>
public sealed class Job : EntityBase
{
    /// <summary>
    /// ID of the Execution this job belongs to.
    /// </summary>
    public Guid ExecutionId { get; init; }
    public required Execution Execution { get; init; } // Navigation property

    /// <summary>
    /// ID of the Process Instance this job belongs to.
    /// </summary>
    public Guid ProcessInstanceId { get; init; }
    // No direct navigation to ProcessInstance needed if accessible via Execution
        
    /// <summary>
    /// ID of the process definition this job belongs to.
    /// </summary>
    public required string ProcessDefinitionId { get; init; } 

    /// <summary>
    /// Type of job handler (e.g., "timer-intermediate-catch", "async-continuation", "message-boundary-event").
    /// </summary>
    public required string JobHandlerType { get; init; }

    /// <summary>
    /// Configuration specific to the job handler (e.g., timer definition, activity ID).
    /// </summary>
    public string? JobHandlerConfiguration { get; init; }

    /// <summary>
    /// When the job is due for execution.
    /// </summary>
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// Number of retries left for this job.
    /// </summary>
    public int Retries { get; set; } = 3; // Default retries

    /// <summary>
    /// Timestamp of the last retry or failure.
    /// </summary>
    public DateTime? LastFailureTime { get; set; }

    /// <summary>
    /// Error message from the last failure.
    /// </summary>
    public string? LastFailureMessage { get; set; }

    /// <summary>
    /// Stack trace or details from the last failure.
    /// </summary>
    public string? LastFailureDetails { get; set; }

    /// <summary>
    /// ID of the Job Executor instance that has locked this job.
    /// Null if not locked.
    /// </summary>
    public string? LockOwner { get; set; }

    /// <summary>
    /// Timestamp when the lock expires.
    /// Null if not locked.
    /// </summary>
    public DateTime? LockExpirationTime { get; set; }

    /// <summary>
    /// Optional: Tenant ID if using multi-tenancy.
    /// </summary>
    public string? TenantId { get; init; } 

    // Private constructor for EF Core
    private Job() 
    { 
        // No explicit initialization needed with init setters
        // Execution = null!;
        // ProcessDefinitionId = null!;
        // JobHandlerType = null!;
    }

    // Public constructor
    public Job(Guid executionId, Guid processInstanceId, string processDefinitionId, string jobHandlerType, string? jobHandlerConfiguration = null, DateTime? dueDate = null, string? tenantId = null, int initialRetries = 3)
    {
        ExecutionId = executionId;
        ProcessInstanceId = processInstanceId;
        ProcessDefinitionId = processDefinitionId;
        JobHandlerType = jobHandlerType;
        JobHandlerConfiguration = jobHandlerConfiguration;
        DueDate = dueDate ?? DateTime.UtcNow; // Default to now if not specified
        TenantId = tenantId;
        Retries = initialRetries;
        // Execution property will be set by EF Core or explicitly if needed.
    }

    public void RecordFailure(string message, string? details, int defaultRetries)
    {
        Retries--;
        LastFailureTime = DateTime.UtcNow;
        LastFailureMessage = message;
        LastFailureDetails = details;
        // Reset lock
        LockOwner = null;
        LockExpirationTime = null;
        // Potentially set DueDate for next retry or move to dead letter queue if Retries <= 0
        if (Retries > 0)
        {
            // Simple backoff strategy (e.g., 1 minute)
            DueDate = DateTime.UtcNow.AddMinutes(1); 
        }
        else
        {
            // Move to dead letter / mark as failed permanently
            DueDate = null; 
        }
    }

    public void AcquireLock(string owner, TimeSpan lockDuration)
    {
        LockOwner = owner;
        LockExpirationTime = DateTime.UtcNow.Add(lockDuration);
    }

    public void ReleaseLock()
    {
        LockOwner = null;
        LockExpirationTime = null;
    }

    /// <summary>
    /// Marks the job as permanently failed (no more retries) and potentially moves it to a dead letter state.
    /// </summary>
    public void MarkAsDeadLetter(string failureMessage, string? failureDetails = null)
    {
        Retries = 0;
        DueDate = null; // No longer due for execution
        LastFailureTime = DateTime.UtcNow;
        LastFailureMessage = failureMessage;
        LastFailureDetails = failureDetails;
        LockOwner = null; // Ensure lock is released
        LockExpirationTime = null;
        // Optionally, set a specific state property if you add one for DeadLetter
    }
}