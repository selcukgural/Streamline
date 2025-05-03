using Microsoft.AspNetCore.Mvc;
using Streamline.Domain.Abstractions; // IUnitOfWork
using Streamline.Domain.Runtime;    // UserTaskAssignment
// Include ve ToListAsync için
// Where, Select vb. için
using System.ComponentModel.DataAnnotations; // RequiredAttribute için

// IEnumerable<T> için

namespace Streamline.Api.Controllers;

// --- DTO for Claim Request --- 
public class ClaimTaskRequest
{
    [Required(AllowEmptyStrings = false)]
    public string UserId { get; set; }
}
// ---------------------------

// --- DTO for Assign Request --- 
public class AssignTaskRequest
{
    [Required(AllowEmptyStrings = false)]
    public string UserId { get; set; }
}
// ---------------------------

// --- DTO for SetDueDate Request --- 
public class SetDueDateRequest
{
    /// <summary>
    /// The new due date. Null to remove the due date.
    /// </summary>
    public DateTime? DueDate { get; set; }
}
// --------------------------------

// --- DTO for SetPriority Request --- 
public class SetPriorityRequest
{
    /// <summary>
    /// The new priority value. Typically a range like 0-100.
    /// </summary>
    [Required]
    [Range(0, 100)] // Example range, adjust as needed
    public int Priority { get; set; }
}
// ----------------------------------

// --- DTO for Delegate Request --- 
public class DelegateTaskRequest
{
    [Required(AllowEmptyStrings = false)]
    public string TargetUserId { get; set; }
}
// -------------------------------

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IExecutionFlowManager _executionFlowManager;
    private readonly ILogger<TasksController> _logger;
    // private readonly ITaskService _taskService; // Application katmanında bir servis olabilir

    public TasksController(
        IUnitOfWork unitOfWork,
        IExecutionFlowManager executionFlowManager,
        ILogger<TasksController> logger
        // ITaskService taskService
        )
    {
        _unitOfWork = unitOfWork;
        _executionFlowManager = executionFlowManager;
        _logger = logger;
        // _taskService = taskService;
    }

    /// <summary>
    /// Gets a list of user task assignments based on optional filters.
    /// </summary>
    /// <param name="assignee">Filter tasks assigned to this user ID.</param>
    /// <param name="candidateUser">Filter tasks where this user ID is a candidate.</param>
    /// <param name="candidateGroup">Filter tasks where this group ID is a candidate group.</param>
    /// <param name="status">Filter tasks by status (Candidate or Assigned).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of user task assignments.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserTaskAssignment>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTasks(
        [FromQuery] string? assignee, 
        [FromQuery] string? candidateUser,
        [FromQuery] string? candidateGroup,
        [FromQuery] UserTaskStatus? status,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to get tasks. Filters - Assignee: {Assignee}, CandidateUser: {CandidateUser}, CandidateGroup: {CandidateGroup}, Status: {Status}", 
                               assignee ?? "Any", 
                               candidateUser ?? "Any",
                               candidateGroup ?? "Any",
                               status?.ToString() ?? "Any");

        try
        {
            var assignmentRepo = _unitOfWork.GetRepository<UserTaskAssignment>();
            
            // Get assignments - filter by status at DB level if possible
            // Only fetch Candidate or Assigned tasks unless status is specified otherwise (though Completed is excluded by default)
            var effectiveStatus = status ?? UserTaskStatus.Candidate; // Default to candidate if no status filter?
            bool filterByStatus = status.HasValue;

            // Fetch potentially relevant tasks (not Completed)
            var assignments = await assignmentRepo.ListAsync(uta => 
                 uta.Status != UserTaskStatus.Completed && 
                 (!filterByStatus || uta.Status == status), // Apply status filter if provided
                cancellationToken);

            // Filter in memory
            IEnumerable<UserTaskAssignment> filteredAssignments = assignments;

            // Filter by assignee
            if (!string.IsNullOrEmpty(assignee))
            {
                filteredAssignments = filteredAssignments.Where(uta => string.Equals(uta.Assignee, assignee, StringComparison.OrdinalIgnoreCase)); // Case-insensitive? Adjust if needed
            }

            // Filter by candidate user
            if (!string.IsNullOrEmpty(candidateUser))
            {
                filteredAssignments = filteredAssignments.Where(uta => 
                    uta.Status == UserTaskStatus.Candidate && // Only candidate tasks can match candidateUser
                    uta.CandidateUsers != null && 
                    uta.CandidateUsers.Split(',')
                                   .Select(u => u.Trim())
                                   .Contains(candidateUser, StringComparer.OrdinalIgnoreCase)); // Case-insensitive? Adjust if needed
            }

            // Filter by candidate group
            if (!string.IsNullOrEmpty(candidateGroup))
            {
                 // If a user is searching by group, they might also see tasks directly assigned to them?
                 // This logic assumes ONLY candidate group matching.
                 filteredAssignments = filteredAssignments.Where(uta => 
                    uta.Status == UserTaskStatus.Candidate && // Only candidate tasks can match candidateGroup
                    uta.CandidateGroups != null && 
                    uta.CandidateGroups.Split(',')
                                    .Select(g => g.Trim())
                                    .Contains(candidateGroup, StringComparer.OrdinalIgnoreCase)); // Case-insensitive? Adjust if needed
            }

            // Convert final filtered list
            var finalAssignments = filteredAssignments.ToList();

            _logger.LogInformation("Returning {Count} tasks matching the criteria.", finalAssignments.Count);
            // TODO: Map entities to DTOs before returning
            return Ok(finalAssignments);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while retrieving tasks.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while retrieving tasks.");
        }
    }

    /// <summary>
    /// Claims a candidate user task assignment for a specific user.
    /// </summary>
    /// <param name="assignmentId">The ID of the UserTaskAssignment to claim.</param>
    /// <param name="request">Request body containing the User ID claiming the task.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Ok if successful, otherwise an error response.</returns>
    [HttpPost("{assignmentId}/claim")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)] 
    [ProducesResponseType(StatusCodes.Status403Forbidden)] // Added for eligibility check failure
    public async Task<IActionResult> ClaimTask(Guid assignmentId, [FromBody] ClaimTaskRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request from User {UserId} to claim UserTaskAssignment {AssignmentId}", request.UserId, assignmentId);

        if (string.IsNullOrWhiteSpace(request.UserId))
        {
            return BadRequest("UserId must be provided in the request body.");
        }

        // --- Get Current User ID (Placeholder) ---
        // TODO: Replace this placeholder with actual logic to get the authenticated user's ID
        //       from HttpContext.User (e.g., using ClaimTypes.NameIdentifier)
        //       Requires authentication setup in the application.
        string? currentUserId = request.UserId; // TEMPORARY: Using the ID from the request body for now.
                                               // This is INSECURE and should be replaced.
        _logger.LogWarning("Using UserId from request body ({UserId}) as current user for claiming task {AssignmentId}. Replace with authenticated user context.", currentUserId, assignmentId);
        // if (string.IsNullOrEmpty(currentUserId)) { return Unauthorized(); } 
        // --- End Placeholder ---

        try
        {
            var assignmentRepo = _unitOfWork.GetRepository<UserTaskAssignment>();
            var assignment = await assignmentRepo.GetByIdAsync(assignmentId, cancellationToken);

            if (assignment == null)
            {
                _logger.LogWarning("UserTaskAssignment {AssignmentId} not found for claiming.", assignmentId);
                return NotFound($"User task assignment with ID {assignmentId} not found.");
            }

            if (assignment.Status != UserTaskStatus.Candidate)
            {
                _logger.LogWarning("UserTaskAssignment {AssignmentId} cannot be claimed by User {UserId}. Current status: {Status}", assignmentId, request.UserId, assignment.Status);
                return Conflict($"Task cannot be claimed. Current status: {assignment.Status}"); // 409 Conflict is appropriate here
            }

            // --- Eligibility Check --- 
            bool isEligible = false;
            // 1. Check Candidate Users
            if (!string.IsNullOrEmpty(assignment.CandidateUsers))
            {
                var candidateUsers = assignment.CandidateUsers.Split(',').Select(u => u.Trim());
                if (candidateUsers.Contains(currentUserId, StringComparer.OrdinalIgnoreCase)) // Case-insensitive check?
                {
                    isEligible = true;
                }
            }

            // 2. Check Candidate Groups (if not already eligible via user)
            if (!isEligible && !string.IsNullOrEmpty(assignment.CandidateGroups))
            {
                var candidateGroups = assignment.CandidateGroups.Split(',').Select(g => g.Trim());
                // TODO: Implement logic to check if currentUserId belongs to any of the candidateGroups.
                //       This likely requires querying an external Identity Management system 
                //       or having group membership information available.
                _logger.LogWarning("Candidate Group eligibility check for User {UserId} and Task {AssignmentId} is NOT IMPLEMENTED. Groups: {Groups}", 
                                 currentUserId, assignmentId, assignment.CandidateGroups);
                // For now, assume not eligible via group if check is missing.
                // isEligible = CheckGroupMembership(currentUserId, candidateGroups); 
            }
            
            // 3. If neither user nor group match, user is not eligible
            // TEMPORARY: Allow claim even if not eligible due to missing group check / placeholder user ID
            if (!isEligible) {
                _logger.LogWarning("User {UserId} is potentially not eligible to claim Task {AssignmentId} (CandidateUsers: '{CandidatesUser}', CandidateGroups: '{CandidatesGroup}'). Allowing claim due to incomplete eligibility check implementation.", 
                                 currentUserId, assignmentId, assignment.CandidateUsers ?? "None", assignment.CandidateGroups ?? "None");
                // Uncomment below to enforce eligibility check when implemented:
                // _logger.LogWarning("User {UserId} is not eligible to claim Task {AssignmentId}. Candidates - Users: '{CandUsers}', Groups: '{CandGroups}'", 
                //                  currentUserId, assignmentId, assignment.CandidateUsers ?? "None", assignment.CandidateGroups ?? "None");
                // return Forbid(); // 403 Forbidden
            }
            // --- End Eligibility Check ---
            
            // Use the Assignee from the request body for claim, ensuring it matches the verified user
            // (This check is slightly redundant if currentUserId is correctly obtained from context, but good practice)
            if (currentUserId != request.UserId) {
                 _logger.LogError("Mismatch between authenticated user ({AuthUser}) and user in request body ({RequestUser}) for claiming task {AssignmentId}. Denying claim.",
                                 currentUserId, request.UserId, assignmentId);
                 return BadRequest("User ID in request body does not match authenticated user.");
            }

            // Perform the claim operation using the verified currentUserId
            assignment.Claim(currentUserId); // Use verified ID
            _logger.LogInformation("UserTaskAssignment {AssignmentId} claimed by User {UserId}. Status changed to {Status}.", 
                                 assignment.Id, assignment.Assignee, assignment.Status);

            // Save changes
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Ok(); 
        }
        catch (InvalidOperationException ioex) // Thrown by assignment.Claim if status is wrong
        {
            _logger.LogWarning(ioex, "Invalid operation while claiming UserTaskAssignment {AssignmentId} for User {UserId}: {ErrorMessage}", assignmentId, request.UserId, ioex.Message);
            return BadRequest(ioex.Message); // Status might have changed between check and claim
        }
        catch (Exception ex)
        {        
            _logger.LogError(ex, "An unexpected error occurred while User {UserId} claiming UserTaskAssignment {AssignmentId}", request.UserId, assignmentId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while claiming the task.");
        }
    }

    /// <summary>
    /// Unclaims an assigned user task assignment, making it available for candidates again.
    /// </summary>
    /// <param name="assignmentId">The ID of the UserTaskAssignment to unclaim.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Ok if successful, otherwise an error response.</returns>
    [HttpPost("{assignmentId}/unclaim")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)] // Added for authorization check failure
    public async Task<IActionResult> UnclaimTask(Guid assignmentId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to unclaim UserTaskAssignment {AssignmentId}", assignmentId);

        // --- Get Current User ID (Placeholder) ---
        // TODO: Replace this placeholder with actual logic to get the authenticated user's ID
        //       from HttpContext.User (e.g., using ClaimTypes.NameIdentifier)
        //       Requires authentication setup in the application.
        string? currentUserId = null; // Placeholder - Set to null or a test value
        // Example: string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        _logger.LogWarning("Current user check for unclaiming is using a placeholder (currently NULL). Implement proper user retrieval."); 
        if (string.IsNullOrEmpty(currentUserId))
        {
            // If we cannot determine the user, we cannot verify if they are the assignee.
            // Depending on security policy, either deny or proceed with caution (as we do now).
             _logger.LogWarning("Cannot determine current user for unclaim request {AssignmentId}. Proceeding without assignee check.", assignmentId);
            // return Unauthorized(); // Or Forbid()?
        }
        // --- End Placeholder ---

        try
        {
            var assignmentRepo = _unitOfWork.GetRepository<UserTaskAssignment>();
            var assignment = await assignmentRepo.GetByIdAsync(assignmentId, cancellationToken);

            if (assignment == null)
            {
                _logger.LogWarning("UserTaskAssignment {AssignmentId} not found for unclaiming.", assignmentId);
                return NotFound($"User task assignment with ID {assignmentId} not found.");
            }

            if (assignment.Status != UserTaskStatus.Assigned)
            {
                _logger.LogWarning("UserTaskAssignment {AssignmentId} cannot be unclaimed. Current status: {Status}", assignmentId, assignment.Status);
                return Conflict($"Task cannot be unclaimed. Current status: {assignment.Status}"); 
            }
            
            // --- Authorization Check: Is caller the assignee? ---
            // Only proceed with the check if we could determine the current user
            if (!string.IsNullOrEmpty(currentUserId))
            {
                if (assignment.Assignee != currentUserId)
                {
                     _logger.LogWarning("User {CurrentUserId} attempted to unclaim task {AssignmentId} assigned to {AssigneeId}. Forbidden.", 
                                      currentUserId, assignmentId, assignment.Assignee);
                     return Forbid(); // 403 Forbidden - User is not the assignee
                }
            }
            else
            {
                // Log that the check is skipped because we couldn't get the current user
                 _logger.LogWarning("Skipping assignee check for unclaim request {AssignmentId} because current user could not be determined.", assignmentId);
            }
            // --- End Authorization Check ---
            
            assignment.Unclaim();
            _logger.LogInformation("UserTaskAssignment {AssignmentId} unclaimed. Assignee removed, status changed to {Status}.", 
                                 assignment.Id, assignment.Status);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Ok(); 
        }
        catch (InvalidOperationException ioex)
        {
            _logger.LogWarning(ioex, "Invalid operation while unclaiming UserTaskAssignment {AssignmentId}: {ErrorMessage}", assignmentId, ioex.Message);
            return BadRequest(ioex.Message);
        }
        catch (Exception ex)
        {        
            _logger.LogError(ex, "An unexpected error occurred while unclaiming UserTaskAssignment {AssignmentId}", assignmentId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while unclaiming the task.");
        }
    }

    /// <summary>
    /// Assigns a user task assignment directly to a specific user.
    /// </summary>
    /// <remarks>This typically requires administrative privileges.</remarks>
    /// <param name="assignmentId">The ID of the UserTaskAssignment to assign.</param>
    /// <param name="request">Request body containing the User ID to assign the task to.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Ok if successful, otherwise an error response.</returns>
    [HttpPost("{assignmentId}/assign")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // TODO: Add authorization check - Requires admin/manager role?
    public async Task<IActionResult> AssignTask(Guid assignmentId, [FromBody] AssignTaskRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to assign UserTaskAssignment {AssignmentId} to User {UserId}", assignmentId, request.UserId);

        if (string.IsNullOrWhiteSpace(request.UserId))
        {
            return BadRequest("UserId must be provided in the request body.");
        }
        
        // TODO: Authorization check: Verify if the calling user has permission to assign tasks.

        try
        {
            var assignmentRepo = _unitOfWork.GetRepository<UserTaskAssignment>();
            var assignment = await assignmentRepo.GetByIdAsync(assignmentId, cancellationToken);

            if (assignment == null)
            {
                _logger.LogWarning("UserTaskAssignment {AssignmentId} not found for assigning.", assignmentId);
                return NotFound($"User task assignment with ID {assignmentId} not found.");
            }

            // Prevent assignment if already completed
            if (assignment.Status == UserTaskStatus.Completed)
            {
                _logger.LogWarning("UserTaskAssignment {AssignmentId} cannot be assigned. It is already completed.", assignmentId);
                return BadRequest("Cannot assign a completed task.");
            }

            // Perform the assignment
            string? previousAssignee = assignment.Assignee;
            assignment.Assignee = request.UserId;
            assignment.Status = UserTaskStatus.Assigned;
            // If it was claimed before, ClaimedTime remains. If not, set it?
            if (assignment.ClaimedTime == null) { 
                assignment.ClaimedTime = DateTime.UtcNow; // Set claim time if wasn't claimed before
            }
            
            _logger.LogInformation("UserTaskAssignment {AssignmentId} assigned to User {UserId}. Previous Assignee: {PreviousAssignee}, Status changed to {Status}.", 
                                 assignment.Id, assignment.Assignee, previousAssignee ?? "None", assignment.Status);

            // Save changes
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Ok(); 
        }
        catch (Exception ex)
        {        
            _logger.LogError(ex, "An unexpected error occurred while assigning UserTaskAssignment {AssignmentId} to User {UserId}", assignmentId, request.UserId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while assigning the task.");
        }
    }

    /// <summary>
    /// Delegates an assigned user task to another user.
    /// </summary>
    /// <remarks>
    /// Typically, only the current assignee can delegate the task.
    /// </remarks>
    /// <param name="assignmentId">The ID of the UserTaskAssignment to delegate.</param>
    /// <param name="request">Request body containing the target User ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Ok if successful, otherwise an error response.</returns>
    [HttpPost("{assignmentId}/delegate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)] // If task is not assigned
    [ProducesResponseType(StatusCodes.Status403Forbidden)] // If the caller is not the assignee
    public async Task<IActionResult> DelegateTask(Guid assignmentId, [FromBody] DelegateTaskRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to delegate UserTaskAssignment {AssignmentId} to User {TargetUserId}", assignmentId, request.TargetUserId);

        if (string.IsNullOrWhiteSpace(request.TargetUserId))
        {
            return BadRequest("TargetUserId must be provided in the request body.");
        }

        // TODO: Get the current user ID from the request context (e.g., HttpContext.User)
        string? currentUserId = null; // Placeholder - Needs implementation based on authentication
        // if (currentUserId == null) { return Unauthorized("Cannot determine the current user."); }
        _logger.LogWarning("Current user check for delegation is not implemented. Proceeding without checking if caller is the assignee."); // Placeholder Warning

        try
        {
            var assignmentRepo = _unitOfWork.GetRepository<UserTaskAssignment>();
            var assignment = await assignmentRepo.GetByIdAsync(assignmentId, cancellationToken);

            if (assignment == null)
            {
                _logger.LogWarning("UserTaskAssignment {AssignmentId} not found for delegation.", assignmentId);
                return NotFound($"User task assignment with ID {assignmentId} not found.");
            }

            // Check current status - must be Assigned
            if (assignment.Status != UserTaskStatus.Assigned)
            {
                _logger.LogWarning("UserTaskAssignment {AssignmentId} cannot be delegated. Current status: {Status}", assignmentId, assignment.Status);
                return Conflict($"Task cannot be delegated. Current status: {assignment.Status}");
            }

            // Check if the caller is the current assignee (if currentUserId could be retrieved)
            // if (assignment.Assignee != currentUserId)
            // {
            //     _logger.LogWarning("User {CurrentUserId} attempted to delegate task {AssignmentId} but is not the current assignee ({AssigneeId}).", currentUserId, assignmentId, assignment.Assignee);
            //     return Forbid(); // 403 Forbidden
            // }
            
            // Prevent delegating to oneself
            if (assignment.Assignee == request.TargetUserId)
            {
                _logger.LogWarning("User {UserId} attempted to delegate task {AssignmentId} to themselves.", request.TargetUserId, assignmentId);
                return BadRequest("Cannot delegate task to the current assignee.");
            }

            // Perform the delegation
            string? previousAssignee = assignment.Assignee;
            assignment.Assignee = request.TargetUserId;
            // Status remains Assigned
            _logger.LogInformation("UserTaskAssignment {AssignmentId} delegated from {PreviousAssignee} to {NewAssignee}.", 
                                 assignment.Id, previousAssignee ?? "(unassigned?)", assignment.Assignee);

            // Save changes
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Ok(); 
        }
        catch (Exception ex)
        {        
            _logger.LogError(ex, "An unexpected error occurred while delegating UserTaskAssignment {AssignmentId} to User {TargetUserId}", assignmentId, request.TargetUserId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while delegating the task.");
        }
    }

    /// <summary>
    /// Completes a user task assignment.
    /// </summary>
    /// <param name="assignmentId">The ID of the UserTaskAssignment to complete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Ok if successful, otherwise an error response.</returns>
    [HttpPost("{assignmentId}/complete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CompleteTask(Guid assignmentId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to complete UserTaskAssignment {AssignmentId}", assignmentId);

        // TODO: Implement the logic to find the assignment, complete it, find the execution, and continue it.
        
        var assignmentRepo = _unitOfWork.GetRepository<UserTaskAssignment>();
        var executionRepo = _unitOfWork.GetRepository<Execution>();

        var assignment = await assignmentRepo.GetByIdAsync(assignmentId, cancellationToken);

        if (assignment == null)
        {
            _logger.LogWarning("UserTaskAssignment {AssignmentId} not found.", assignmentId);
            return NotFound($"User task assignment with ID {assignmentId} not found.");
        }

        if (assignment.Status != UserTaskStatus.Assigned)
        {
             _logger.LogWarning("UserTaskAssignment {AssignmentId} cannot be completed. Current status: {Status}", assignmentId, assignment.Status);
             // Potentially return a different status code like Conflict (409) or BadRequest (400)
             return BadRequest($"Task cannot be completed. Current status: {assignment.Status}");
        }

        try
        {
            // 1. Complete the assignment entity
            assignment.Complete();
            _logger.LogInformation("UserTaskAssignment {AssignmentId} marked as Completed.", assignment.Id);

            // 2. Find the waiting execution
            var execution = await executionRepo.GetByIdAsync(assignment.ExecutionId, cancellationToken);

            if (execution == null)
            {
                _logger.LogError("Execution {ExecutionId} associated with UserTaskAssignment {AssignmentId} not found. Cannot continue flow.", assignment.ExecutionId, assignment.Id);
                // This indicates an inconsistency. Maybe return 500 Internal Server Error?
                return StatusCode(StatusCodes.Status500InternalServerError, "Associated execution not found.");
            }

            // 3. Validate execution state
            if (!execution.IsActive || execution.CurrentFlowNodeId != assignment.TaskDefinitionId)
            {
                 _logger.LogError("Execution {ExecutionId} is not active or not waiting at the expected User Task node {ExpectedNodeId}. Current Node: {CurrentNodeId}, IsActive: {IsActive}. Cannot continue flow.", 
                                execution.Id, assignment.TaskDefinitionId, execution.CurrentFlowNodeId, execution.IsActive);
                 return StatusCode(StatusCodes.Status500InternalServerError, "Execution state is inconsistent.");
            }

            // 4. Save assignment completion FIRST
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Saved completed status for UserTaskAssignment {AssignmentId}. Triggering execution continuation...", assignment.Id);

            // 5. Trigger execution continuation
            // This might potentially fail, but the task is already marked complete.
            // Consider transaction scope or compensating actions if needed.
            await _executionFlowManager.ContinueExecutionAsync(execution.Id, cancellationToken);
            _logger.LogInformation("Continuation triggered for Execution {ExecutionId} after UserTask completion.", execution.Id);

            return Ok(); 
        }
        catch (InvalidOperationException ioex)
        {
            _logger.LogWarning(ioex, "Invalid operation while completing UserTaskAssignment {AssignmentId}: {ErrorMessage}", assignmentId, ioex.Message);
            return BadRequest(ioex.Message);
        }
        catch (Exception ex)
        {        
            _logger.LogError(ex, "An unexpected error occurred while completing UserTaskAssignment {AssignmentId}", assignmentId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
        }
    }

    /// <summary>
    /// Sets the due date for a user task assignment.
    /// </summary>
    /// <param name="assignmentId">The ID of the UserTaskAssignment to update.</param>
    /// <param name="request">Request body containing the new due date.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Ok if successful, otherwise an error response.</returns>
    [HttpPut("{assignmentId}/duedate")] // Using PUT for update
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // TODO: Add authorization check?
    public async Task<IActionResult> SetDueDate(Guid assignmentId, [FromBody] SetDueDateRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to set DueDate for UserTaskAssignment {AssignmentId} to {DueDate}", assignmentId, request.DueDate?.ToString() ?? "NULL");
        
        // Optional: Validate DueDate (e.g., not in the past)?

        try
        {
            var assignmentRepo = _unitOfWork.GetRepository<UserTaskAssignment>();
            var assignment = await assignmentRepo.GetByIdAsync(assignmentId, cancellationToken);

            if (assignment == null)
            {
                _logger.LogWarning("UserTaskAssignment {AssignmentId} not found for setting due date.", assignmentId);
                return NotFound($"User task assignment with ID {assignmentId} not found.");
            }

            // Prevent update if already completed
            if (assignment.Status == UserTaskStatus.Completed)
            {
                _logger.LogWarning("UserTaskAssignment {AssignmentId} cannot be updated. It is already completed.", assignmentId);
                return BadRequest("Cannot update a completed task.");
            }

            // Perform the update
            assignment.DueDate = request.DueDate;
             _logger.LogInformation("DueDate for UserTaskAssignment {AssignmentId} set to {DueDate}.", 
                                 assignment.Id, assignment.DueDate?.ToString() ?? "NULL");

            // Save changes
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Ok(); 
        }
        catch (Exception ex)
        {        
            _logger.LogError(ex, "An unexpected error occurred while setting DueDate for UserTaskAssignment {AssignmentId}", assignmentId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while setting the due date.");
        }
    }

    /// <summary>
    /// Sets the priority for a user task assignment.
    /// </summary>
    /// <param name="assignmentId">The ID of the UserTaskAssignment to update.</param>
    /// <param name="request">Request body containing the new priority value.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Ok if successful, otherwise an error response.</returns>
    [HttpPut("{assignmentId}/priority")] // Using PUT for update
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // TODO: Add authorization check?
    public async Task<IActionResult> SetPriority(Guid assignmentId, [FromBody] SetPriorityRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received request to set Priority for UserTaskAssignment {AssignmentId} to {Priority}", assignmentId, request.Priority);

        // Validate priority range server-side as well (though DTO validation should catch it)
        if (request.Priority < 0 || request.Priority > 100) // Example range check
        {
            return BadRequest("Priority must be between 0 and 100.");
        }

        try
        {
            var assignmentRepo = _unitOfWork.GetRepository<UserTaskAssignment>();
            var assignment = await assignmentRepo.GetByIdAsync(assignmentId, cancellationToken);

            if (assignment == null)
            {
                _logger.LogWarning("UserTaskAssignment {AssignmentId} not found for setting priority.", assignmentId);
                return NotFound($"User task assignment with ID {assignmentId} not found.");
            }

            // Prevent update if already completed
            if (assignment.Status == UserTaskStatus.Completed)
            {
                _logger.LogWarning("UserTaskAssignment {AssignmentId} cannot be updated. It is already completed.", assignmentId);
                return BadRequest("Cannot update a completed task.");
            }

            // Perform the update
            assignment.Priority = request.Priority;
             _logger.LogInformation("Priority for UserTaskAssignment {AssignmentId} set to {Priority}.", 
                                 assignment.Id, assignment.Priority);

            // Save changes
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Ok(); 
        }
        catch (Exception ex)
        {        
            _logger.LogError(ex, "An unexpected error occurred while setting Priority for UserTaskAssignment {AssignmentId}", assignmentId);
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while setting the priority.");
        }
    }

    // TODO: Add other endpoints (e.g., AssignTask)
} 