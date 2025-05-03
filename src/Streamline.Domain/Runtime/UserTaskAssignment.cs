using System.ComponentModel.DataAnnotations;

namespace Streamline.Domain.Runtime;

/// <summary>
/// Represents the assignment and state of a User Task within a process instance.
/// This entity is typically managed by a Tasklist or similar mechanism.
/// </summary>
public class UserTaskAssignment : EntityBase
{
    /// <summary>
    /// ID of the Process Instance this task belongs to.
    /// </summary>
    public Guid ProcessInstanceId { get; init; }

    /// <summary>
    /// ID of the Execution path waiting at the User Task.
    /// </summary>
    public Guid ExecutionId { get; init; }

    /// <summary>
    /// ID of the User Task Flow Node in the BPMN definition.
    /// </summary>
    [MaxLength(255)] // Assuming IDs have a reasonable max length
    public string TaskDefinitionId { get; init; }

    /// <summary>
    /// Name of the User Task (copied from BPMN definition).
    /// </summary>
    [MaxLength(500)]
    public string? TaskName { get; set; }

    /// <summary>
    /// Optional description or instructions for the task.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// User ID of the person assigned to this task. Null if unassigned or assigned to a group.
    /// </summary>
    [MaxLength(100)]
    public string? Assignee { get; set; }

    // Storing candidate groups/users as comma-separated strings or JSON might be 
    // simpler than complex relations if not heavily queried.
    // Alternatively, use separate related entities.
    // Let's use simple strings for now.
    
    /// <summary>
    /// Comma-separated list of group IDs that are candidates to claim this task.
    /// </summary>
    public string? CandidateGroups { get; set; }

    /// <summary>
    /// Comma-separated list of user IDs that are candidates to claim this task.
    /// </summary>
    public string? CandidateUsers { get; set; }

    /// <summary>
    /// Current status of the task.
    /// </summary>
    public UserTaskStatus Status { get; set; }

    /// <summary>
    /// Timestamp when the task was created.
    /// </summary>
    public DateTime CreatedTime { get; init; }

    /// <summary>
    /// Timestamp when the task was claimed or assigned.
    /// </summary>
    public DateTime? ClaimedTime { get; set; }

    /// <summary>
    /// Timestamp when the task was completed.
    /// </summary>
    public DateTime? CompletedTime { get; set; }

    /// <summary>
    /// Optional due date for the task.
    /// </summary>
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// Priority of the task.
    /// </summary>
    public int Priority { get; set; } = 50; // Default priority

    // Private constructor for EF Core
    private UserTaskAssignment() 
    { 
        TaskDefinitionId = null!; // Required init property
    }

    public UserTaskAssignment(
        Guid processInstanceId, 
        Guid executionId, 
        string taskDefinitionId, 
        string? taskName = null, 
        string? description = null,
        string? assignee = null,
        string? candidateGroups = null,
        string? candidateUsers = null,
        DateTime? dueDate = null,
        int priority = 50
        )
    {
        ProcessInstanceId = processInstanceId;
        ExecutionId = executionId;
        TaskDefinitionId = taskDefinitionId;
        TaskName = taskName;
        Description = description;
        Assignee = assignee;
        CandidateGroups = candidateGroups;
        CandidateUsers = candidateUsers;
        DueDate = dueDate;
        Priority = priority;

        Status = string.IsNullOrEmpty(assignee) ? UserTaskStatus.Candidate : UserTaskStatus.Assigned;
        CreatedTime = DateTime.UtcNow;
    }

    public void Claim(string userId)
    {
        if (Status != UserTaskStatus.Candidate)
        {
             throw new InvalidOperationException($"Task cannot be claimed. Current status: {Status}");
        }
        // TODO: Check if userId is in CandidateUsers or belongs to CandidateGroups?
        Assignee = userId;
        Status = UserTaskStatus.Assigned;
        ClaimedTime = DateTime.UtcNow;
    }

    public void Unclaim()
    {
         if (Status != UserTaskStatus.Assigned)
        {
             throw new InvalidOperationException($"Task cannot be unclaimed. Current status: {Status}");
        }
        Assignee = null;
        Status = UserTaskStatus.Candidate;
        ClaimedTime = null;
    }

    public void Complete()
    {
         if (Status != UserTaskStatus.Assigned)
        {
             throw new InvalidOperationException($"Task cannot be completed. Current status: {Status}");
        }
        Status = UserTaskStatus.Completed;
        CompletedTime = DateTime.UtcNow;
    }

    // Add other methods as needed (e.g., SetAssignee, SetDueDate, Delegate)
}

/// <summary>
/// Represents the possible statuses of a User Task Assignment.
/// </summary>
public enum UserTaskStatus
{
    /// <summary>Task is created and available for candidates to claim.</summary>
    Candidate = 0,
    /// <summary>Task is assigned to a specific user.</summary>
    Assigned = 1,
    /// <summary>Task has been completed.</summary>
    Completed = 2,
    // /// <summary>Task has been cancelled or is no longer relevant.</summary>
    // Cancelled = 3 
} 