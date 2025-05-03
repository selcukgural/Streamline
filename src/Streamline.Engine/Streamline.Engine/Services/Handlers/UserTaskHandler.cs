using Microsoft.Extensions.Logging;
using Streamline.Domain.Schema.Activities; // For UserTask
using Streamline.Domain.Schema.Common; // ResourceRole, HumanPerformer, PotentialOwner, FormalExpression için
using Streamline.Engine.Abstractions;
using Streamline.Domain.Runtime; // For UserTaskAssignment, ActivityInstance
// List<T> için
// OfType<T>, Select, Distinct için
using Task = System.Threading.Tasks.Task;

namespace Streamline.Engine.Services.Handlers;

/// <summary>
/// Handler for User Tasks. Creates a UserTaskAssignment record and waits for external completion.
/// </summary>
public class UserTaskHandler(ILogger<UserTaskHandler> logger) : IFlowNodeHandler<UserTask>
{
    public async Task ExecuteAsync(FlowNode node, FlowNodeHandlerContext context, CancellationToken cancellationToken)
    {
        if (node is not UserTask userTask)
        {
            logger.LogError("Node type mismatch. Expected UserTask but got {NodeType} for Node {NodeId}", node.GetType().Name, node.Id);
            context.Execution.Fail("Incorrect node type passed to UserTaskHandler");
            await context.UnitOfWork.SaveChangesAsync(cancellationToken);
            return;
        }

        logger.LogInformation("Executing UserTask handler for Node {NodeId} ({NodeName}) - Execution {ExecutionId}", 
            userTask.Id, userTask.Name ?? "Unnamed UserTask", context.Execution.Id);
        
        // --- Create UserTaskAssignment --- 
        var userTaskRepo = context.UnitOfWork.GetRepository<UserTaskAssignment>();

        // Check if assignment already exists for this execution and task def (prevent duplicates)
        var assignmentExists = await userTaskRepo.AnyAsync(
            uta => uta.ExecutionId == context.Execution.Id && uta.TaskDefinitionId == userTask.Id,
            cancellationToken
        );

        // Declare dueDate and priority outside the if block to widen their scope
        DateTime? dueDate = null;
        int? priority = null; 

        if (!assignmentExists)
        {
            // --- Extract Assignment Info --- 
            string? assignee = null;
            var candidateUserList = new List<string>();
            var candidateGroupList = new List<string>();
            var variableExpressionEncountered = false;

            if (userTask.ResourceRoleSpecified) // Check if the collection has elements
            {
                logger.LogDebug("Processing ResourceRoles for UserTask {UserTaskId}", userTask.Id);
                foreach (var resourceRole in userTask.ResourceRole)
                {
                    var expressionBase = resourceRole.ResourceAssignmentExpression?.Expression;
                    if (expressionBase?.Text == null || !expressionBase.Text.Any(t => !string.IsNullOrWhiteSpace(t)))
                    {
                        logger.LogWarning("ResourceRole (Type: {RoleType}) for UserTask {UserTaskId} has null or empty Expression.Text.", resourceRole.GetType().Name, userTask.Id);
                        continue; // Skip this resource role
                    }

                    var expressionText = string.Join("", expressionBase.Text).Trim();
                    var language = (expressionBase is FormalExpression fe) ? fe.Language : null;
                    if(language != null) logger.LogDebug("Found FormalExpression with Language: {Language} for Text: '{Text}'", language, expressionText);

                    // Check if it looks like a variable expression that we cannot evaluate
                    if (expressionText.Contains("${") || expressionText.Contains("#{")) // Common patterns for JUEL/FEEL
                    {                         
                        logger.LogWarning("Assignment expression '{Expression}' for Task {TaskId} appears to be a variable expression (Language: {Language}). Expression evaluation is not implemented; this assignment will be ignored.", 
                                        expressionText, userTask.Id, language ?? "N/A");
                        variableExpressionEncountered = true;
                        continue; // Skip evaluation for this expression
                    }

                    if (string.IsNullOrEmpty(expressionText)) continue;

                    // --- Simple Parsing Logic --- 
                    if (resourceRole is HumanPerformer)
                    {
                        if (assignee == null) 
                        {
                            assignee = ParseUserIdFromExpression(expressionText, userTask.Id);
                            if (assignee != null) logger.LogDebug("Parsed Assignee from HumanPerformer expression: {Assignee}", assignee);
                        }
                        else { logger.LogWarning("Multiple HumanPerformers found for UserTask {UserTaskId}. Using the first one found: {Assignee}", userTask.Id, assignee); }
                    }
                    else if (resourceRole is PotentialOwner)
                    {
                        ParsePotentialOwnerExpression(expressionText, candidateUserList, candidateGroupList, userTask.Id);
                    }
                    // --- End Simple Parsing Logic ---
                }
            }
            else
            {
                logger.LogDebug("No ResourceRoles specified for UserTask {UserTaskId}. Task will be created as candidate without specific assignments.", userTask.Id);
            }

            // Combine lists into comma-separated strings, ensuring uniqueness
            var candidateUsers = candidateUserList.Count > 0 ? string.Join(",", candidateUserList.Distinct()) : null;
            var candidateGroups = candidateGroupList.Count > 0 ? string.Join(",", candidateGroupList.Distinct()) : null;
            // --- End Extract Assignment Info ---

            // --- Extract DueDate and Priority from ExtensionElements ---
            if (userTask.ExtensionElements != null)
            {
                logger.LogDebug("Checking ExtensionElements for DueDate and Priority on UserTask {NodeId}", userTask.Id);
                foreach (var element in userTask.ExtensionElements.Any) // Assuming standard BPMN extensions
                {
                    // Camunda uses camunda:property elements
                    if (element.Name.EndsWith("property")) 
                    {
                        var nameAttr = element.GetAttribute("name");
                        var valueAttr = element.GetAttribute("value");

                        if (!string.IsNullOrEmpty(nameAttr) && !string.IsNullOrEmpty(valueAttr))
                        {
                            if (string.Equals(nameAttr, "dueDate", StringComparison.OrdinalIgnoreCase))
                            {
                                if (DateTime.TryParse(valueAttr, out var parsedDate))
                                {
                                    dueDate = parsedDate;
                                    logger.LogDebug("Parsed DueDate '{DueDate}' from extension element for Task {TaskId}", dueDate, userTask.Id);
                                }
                                else
                                {
                                    logger.LogWarning("Could not parse DueDate value '{Value}' for Task {TaskId}", valueAttr, userTask.Id);
                                }
                            }
                            else if (string.Equals(nameAttr, "priority", StringComparison.OrdinalIgnoreCase))
                            {
                                if (int.TryParse(valueAttr, out var parsedPriority))
                                {
                                    priority = parsedPriority;
                                    logger.LogDebug("Parsed Priority '{Priority}' from extension element for Task {TaskId}", priority, userTask.Id);
                                }
                                else
                                {
                                    logger.LogWarning("Could not parse Priority value '{Value}' for Task {TaskId}", valueAttr, userTask.Id);
                                }
                            }
                        }
                    }
                    // Add checks for other potential extension element structures if needed
                }
            }
            // --- End Extract DueDate and Priority ---

            // Extract other basic info
            var taskName = userTask.Name;
            var description = userTask.Documentation?.FirstOrDefault()?.Text != null 
                                  ? string.Join("", userTask.Documentation.First().Text) 
                                  : null;

            if(variableExpressionEncountered){
                 logger.LogWarning("Task {TaskId} assignment potentially incomplete due to unevaluated variable expressions.", userTask.Id);
            }

            var assignment = new UserTaskAssignment(
                processInstanceId: context.Execution.ProcessInstanceId,
                executionId: context.Execution.Id,
                taskDefinitionId: userTask.Id,
                taskName: taskName,
                description: description,
                assignee: assignee,
                candidateGroups: candidateGroups,
                candidateUsers: candidateUsers,
                dueDate: dueDate,
                priority: priority ?? 50
            );

            await userTaskRepo.AddAsync(assignment, cancellationToken);
            logger.LogInformation("Created UserTaskAssignment {AssignmentId} for Task {TaskDefinitionId}, Execution {ExecutionId}. Assignee: '{Assignee}', Candidates: (Users: '{CandidatesUser}', Groups: '{CandidatesGroup}'), Due: {DueDate}, Priority: {Priority}, Status: {Status}", 
                                assignment.Id, assignment.TaskDefinitionId, assignment.ExecutionId, assignment.Assignee ?? "None", assignment.CandidateUsers ?? "None", assignment.CandidateGroups ?? "None", assignment.DueDate?.ToString("o") ?? "N/A", assignment.Priority, assignment.Status);
        }
        else
        {
             logger.LogWarning("UserTaskAssignment already exists for Execution {ExecutionId} and TaskDefinition {TaskDefinitionId}. Skipping creation.",
                               context.Execution.Id, userTask.Id);
             // TODO: Should we still try to update ActivityInstance if assignment exists? 
             // For now, we assume if assignment exists, ActivityInstance was likely handled previously.
             // If needed, we could potentially re-fetch/update ActivityInstance here too.
        }
        
        // --- Ensure ActivityInstance exists (for tracking) --- 
        var activityInstance = context.Execution.ProcessInstance.ActivityInstances
                                .FirstOrDefault(ai => ai.ExecutionId == context.Execution.Id && ai.FlowNodeId == userTask.Id && ai.EndTime == null);

        if (activityInstance == null)
        {
            var taskActivity = new ActivityInstance(
                context.Execution.ProcessInstanceId,
                userTask.Id,
                context.Execution.Id,
                userTask.Name
            );
            taskActivity.ProcessInstance = context.Execution.ProcessInstance;
            context.Execution.ProcessInstance.AddActivityInstance(taskActivity);
            logger.LogInformation("Created ActivityInstance {ActivityInstanceId} for UserTask {NodeId}", taskActivity.Id, userTask.Id);
        }
        else
        {
            logger.LogDebug("Found existing active ActivityInstance {ActivityInstanceId} for UserTask {NodeId}", activityInstance.Id, userTask.Id);
        }

        // --- Update ActivityInstance with DueDate and Priority if available ---
        if (activityInstance != null && (dueDate.HasValue || priority.HasValue))
        {
            var changed = false;
            if(dueDate.HasValue && activityInstance.DueDate != dueDate)
            {
                activityInstance.DueDate = dueDate;
                changed = true;
            }
            if(priority.HasValue && activityInstance.Priority != priority)
            {
                activityInstance.Priority = priority;
                changed = true;
            }

            if(changed) 
            {
                logger.LogInformation("Updated ActivityInstance {ActivityInstanceId} with DueDate: {DueDate}, Priority: {Priority}", 
                                    activityInstance.Id, 
                                    activityInstance.DueDate?.ToString("o") ?? "N/A", 
                                    activityInstance.Priority?.ToString() ?? "N/A");
            }
        }
        
        // --- Save Changes (Assignment and ActivityInstance) --- 
        await context.UnitOfWork.SaveChangesAsync(cancellationToken);

        // --- Final Log --- 
        // Execution does NOT continue automatically.
        logger.LogInformation("UserTask {NodeId} is now waiting for external completion via its assignment (check logs for ID and details).", userTask.Id);
    }

    // --- Helper methods for simple parsing --- 
    private string? ParseUserIdFromExpression(string expression, string taskId)
    {
        expression = expression.Trim();
        if (expression.StartsWith("user(") && expression.EndsWith(")"))
        {
            return expression.Substring(5, expression.Length - 6).Trim();
        }
        // Assume direct User ID if not in user() format
        if (!expression.Contains("(") && !expression.Contains(")")) 
        {
             logger.LogDebug("Assuming direct User ID '{UserId}' from expression '{Expression}' for Task {TaskId}", expression, expression, taskId);
             return expression;
        }
        logger.LogWarning("Could not parse User ID from expression '{Expression}' for Task {TaskId}. Expected user(id) format or direct ID.", expression, taskId);
        return null;
    }

    private void ParsePotentialOwnerExpression(string expression, List<string> users, List<string> groups, string taskId)
    {
        expression = expression.Trim();
         if (expression.StartsWith("user(") && expression.EndsWith(")"))
        {
            var userList = expression.Substring(5, expression.Length - 6);
            var parsedUsers = userList.Split(',').Select(u => u.Trim()).Where(u => !string.IsNullOrEmpty(u)).ToList();
            if(parsedUsers.Any()) users.AddRange(parsedUsers);
            logger.LogDebug("Parsed Candidate Users from PotentialOwner expression '{Expression}': {Users}", expression, string.Join(",", parsedUsers));
        }
        else if (expression.StartsWith("group(") && expression.EndsWith(")"))
        {
            var groupList = expression.Substring(6, expression.Length - 7);
            var parsedGroups = groupList.Split(',').Select(g => g.Trim()).Where(g => !string.IsNullOrEmpty(g)).ToList();
            if(parsedGroups.Any()) groups.AddRange(parsedGroups);
            logger.LogDebug("Parsed Candidate Groups from PotentialOwner expression '{Expression}': {Groups}", expression, string.Join(",", parsedGroups));
        }
        // Assume comma-separated user list if format unknown
        else if (!expression.Contains("(") && !expression.Contains(")"))
        {
            logger.LogWarning("PotentialOwner expression '{Expression}' for Task {TaskId} does not match user(...) or group(...) format. Assuming comma-separated user list.", expression, taskId);
            var parsedUsers = expression.Split(',').Select(u => u.Trim()).Where(u => !string.IsNullOrEmpty(u)).ToList();
            if(parsedUsers.Any()) users.AddRange(parsedUsers);
        }
        else { 
             logger.LogWarning("Could not parse PotentialOwner expression '{Expression}' for Task {TaskId}. Expected user(...) or group(...) format, or comma-separated user list.", expression, taskId);
        }
    }
    // --------------------------------------
} 