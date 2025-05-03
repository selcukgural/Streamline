// Added for IServiceProvider
using Microsoft.Extensions.Logging;
using Streamline.Domain.Schema.Activities; // For ServiceTask
using Streamline.Domain.Schema.Common;
using Streamline.Engine.Abstractions;
// For Type, Activator
// For FirstOrDefault, Any
// For XElement related checks if needed
using Task = System.Threading.Tasks.Task;

// For Task

namespace Streamline.Engine.Services.Handlers;

/// <summary>
/// Handler for Service Tasks. Delegates execution based on the task's implementation detail.
/// </summary>
public class ServiceTaskHandler : IFlowNodeHandler<ServiceTask>
{
    private readonly ILogger<ServiceTaskHandler> _logger;
    private readonly IServiceProvider _serviceProvider; // Inject IServiceProvider for resolving class delegates
    // TODO: Inject HttpClientFactory for HTTP tasks
    // TODO: Inject ExpressionEvaluator service for expression tasks

    public ServiceTaskHandler(ILogger<ServiceTaskHandler> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task ExecuteAsync(FlowNode node, FlowNodeHandlerContext context, CancellationToken cancellationToken)
    {
        if (node is not ServiceTask serviceTask)
        {
            _logger.LogError("Node type mismatch. Expected ServiceTask but got {NodeType} for Node {NodeId}", node.GetType().Name, node.Id);
            context.Execution.Fail("Incorrect node type passed to ServiceTaskHandler");
            await context.UnitOfWork.SaveChangesAsync(cancellationToken); 
            return;
        }

        _logger.LogInformation("Executing ServiceTask handler for Node {NodeId} ({NodeName}) - Execution {ExecutionId}",
            serviceTask.Id, serviceTask.Name ?? "Unnamed ServiceTask", context.Execution.Id);

        var implementationDetail = serviceTask.Implementation; 
        var implementationType = "Unknown"; // Default type

        try
        {
            // --- Determine Implementation Type and Detail ---
            // Priority: Extension Elements (Camunda style) > Standard Implementation Attribute

            string? camundaClass = null;
            string? camundaExpression = null;
            string? camundaDelegateExpression = null;
            // TODO: Add check for camunda:connector

            if (serviceTask.ExtensionElements?.Any != null)
            {
                foreach (var element in serviceTask.ExtensionElements.Any)
                {
                    if (element.LocalName == "class" && element.NamespaceURI.Contains("camunda"))
                    {
                        camundaClass = element.InnerText?.Trim();
                        implementationType = "CamundaClass";
                        implementationDetail = camundaClass;
                        _logger.LogDebug("Found Camunda Class implementation: {Class}", camundaClass);
                        break; // Assume only one primary implementation
                    }
                    if (element.LocalName == "expression" && element.NamespaceURI.Contains("camunda"))
                    {
                        camundaExpression = element.InnerText?.Trim();
                        implementationType = "CamundaExpression";
                        implementationDetail = camundaExpression;
                        _logger.LogDebug("Found Camunda Expression implementation: {Expression}", camundaExpression);
                         break;
                    }
                    if (element.LocalName == "delegateExpression" && element.NamespaceURI.Contains("camunda"))
                    {
                        camundaDelegateExpression = element.InnerText?.Trim();
                        implementationType = "CamundaDelegateExpression";
                        implementationDetail = camundaDelegateExpression;
                        _logger.LogDebug("Found Camunda Delegate Expression implementation: {DelegateExpression}", camundaDelegateExpression);
                         break;
                    }
                    // TODO: Check for camunda:connector and its details
                }
            }

            // If no Camunda extension found, check standard implementation attribute
            if (implementationType == "Unknown" && !string.IsNullOrWhiteSpace(serviceTask.Implementation))
            {
                implementationDetail = serviceTask.Implementation.Trim();
                if (implementationDetail.StartsWith("##")) // Standard types like ##WebService, ##unspecified
                {
                    implementationType = implementationDetail;
                     _logger.LogDebug("Found standard implementation type: {Implementation}", implementationDetail);
                }
                else if (Uri.IsWellFormedUriString(implementationDetail, UriKind.Absolute))
                {
                    implementationType = "Uri"; // Could be a web service WSDL or similar
                     _logger.LogDebug("Found URI implementation: {Uri}", implementationDetail);
                }
                else
                {
                    // Assume it *might* be a class name if not a standard type or URI (less common in pure standard)
                    implementationType = "PotentialClass"; // Or treat as Unspecified/Error?
                    _logger.LogWarning("Non-standard, non-URI implementation attribute found: '{Implementation}'. Assuming PotentialClass or Unspecified.", implementationDetail);
                }
            }
            else if (implementationType == "Unknown")
            { 
                implementationType = "Unspecified";
                implementationDetail = "##unspecified";
                _logger.LogWarning("ServiceTask {NodeId} has no specific implementation defined. Treating as Unspecified.", serviceTask.Id);
            }

            // --- Execute based on Implementation Type ---
            _logger.LogInformation("Attempting execution for ServiceTask {NodeId} with Type: {ImplementationType}, Detail: '{ImplementationDetail}'",
                                 serviceTask.Id, implementationType, implementationDetail ?? "N/A");

            var executionSuccessful = false;
            switch (implementationType)
            {
                case "CamundaClass":
                    executionSuccessful = await ExecuteClassDelegateAsync(camundaClass!, context, cancellationToken);
                    break;
                case "CamundaDelegateExpression":
                     executionSuccessful = await ExecuteDelegateExpressionAsync(camundaDelegateExpression!, context, cancellationToken);
                     break;
                case "CamundaExpression":
                     executionSuccessful = await ExecuteExpressionAsync(camundaExpression!, context, cancellationToken);
                     break;
                case "##WebService":
                case "Uri":
                     // TODO: Implement HTTP/WebService call logic
                     _logger.LogWarning("WebService/URI implementation for ServiceTask {NodeId} is not yet implemented.", serviceTask.Id);
                     executionSuccessful = true; // Placeholder: Mark as success for now
                     break;
                case "PotentialClass": // Handle potentially standard class name
                     executionSuccessful = await ExecuteClassDelegateAsync(implementationDetail!, context, cancellationToken);
                     break;
                case "Unspecified":
                case "##unspecified":
                     _logger.LogInformation("ServiceTask {NodeId} is Unspecified. No action taken.", serviceTask.Id);
                     executionSuccessful = true; // Unspecified tasks usually just pass through
                     break;
                default:
                     _logger.LogError("Unsupported ServiceTask implementation type '{ImplementationType}' for Node {NodeId}", implementationType, serviceTask.Id);
                    context.Execution.Fail($"Unsupported ServiceTask implementation: {implementationType}");
                    await context.UnitOfWork.SaveChangesAsync(cancellationToken); // Save failure state
                    return; // Stop further processing
            }

            if (!executionSuccessful)
            {
                 _logger.LogError("Execution failed for ServiceTask {NodeId} (Type: {ImplementationType}). See previous logs.", serviceTask.Id, implementationType);
                 // context.Execution should already be marked as failed by the specific execution method
                 // Ensure SaveChangesAsync is called in the specific method or here if needed.
                 // Optionally: throw new InvalidOperationException(...) to halt flow immediately
                 return; // Stop before marking complete
            }

            // --- Mark ActivityInstance as Completed --- 
            var activityInstance = context.Execution.ProcessInstance.ActivityInstances
                                    .FirstOrDefault(ai => ai.ExecutionId == context.Execution.Id && ai.FlowNodeId == serviceTask.Id && ai.EndTime == null);
            if (activityInstance != null)
            {
                activityInstance.Complete();
                _logger.LogInformation("Marked ActivityInstance {ActivityInstanceId} for ServiceTask {NodeId} as completed.", activityInstance.Id, serviceTask.Id);
            }
            else
            {
                // This might indicate an issue if an active instance wasn't found when expected.
                 _logger.LogWarning("Could not find active ActivityInstance for ServiceTask {NodeId} (Execution {ExecutionId}) to mark as completed.", serviceTask.Id, context.Execution.Id);
                 // Decide if this should be a failure or just a warning. For now, warning.
            }
            
            await context.UnitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("ServiceTask {NodeId} handler completed successfully. Execution continues.", serviceTask.Id);
        }
        catch (Exception ex)
        {
             _logger.LogError(ex, "Error executing ServiceTask {NodeId} (Type: {ImplementationType}, Detail: {ImplementationDetail}): {ErrorMessage}", 
                              serviceTask.Id, implementationType, implementationDetail ?? "N/A", ex.Message);
            context.Execution.Fail($"ServiceTask execution failed: {ex.Message}");
            await context.UnitOfWork.SaveChangesAsync(cancellationToken);
            throw; 
        }
    }

    // --- Placeholder Execution Methods ---

    private async Task<bool> ExecuteClassDelegateAsync(string className, FlowNodeHandlerContext context, CancellationToken cancellationToken)
    {
         _logger.LogDebug("Attempting to execute class delegate: {ClassName}", className);
         try
         {
            // Attempt to resolve the type first via IServiceProvider (Dependency Injection)
            var delegateType = Type.GetType(className, throwOnError: false); // Don't throw immediately if not found
            object? delegateInstance = null;

            if (delegateType != null)
            {
                delegateInstance = _serviceProvider.GetService(delegateType);
                if (delegateInstance != null) {
                     _logger.LogDebug("Resolved delegate {ClassName} via IServiceProvider.", className);
                }
            }

            // If not resolved via DI or type wasn't found initially, try direct activation (less common for delegates)
            if (delegateInstance == null)
            {
                if (delegateType == null) // Try again with assembly qualified name potentially?
                {
                    // This part is tricky without knowing assembly context. 
                    // For simplicity, assume type name is enough or it's registered in DI.
                    throw new TypeLoadException($"Could not load type specified: {className}. Ensure it's assembly-qualified or registered in DI.");
                }
                 _logger.LogWarning("Delegate {ClassName} not found in IServiceProvider. Attempting direct activation.", className);
                delegateInstance = Activator.CreateInstance(delegateType);
            }

            if (delegateInstance == null)
            {
                 throw new InvalidOperationException($"Could not create instance of delegate class: {className}");
            }

            // Check if it implements a known interface (e.g., IServiceTaskDelegate)
            if (delegateInstance is IServiceTaskDelegate serviceDelegate)
            {
                 await serviceDelegate.ExecuteAsync(context, cancellationToken);
                 _logger.LogInformation("Successfully executed IServiceTaskDelegate: {ClassName}", className);
                 return true;
            }
            // TODO: Add checks for other possible delegate types/interfaces (e.g., JavaDelegate equivalent?)
            else
            {
                 throw new InvalidOperationException($"Class {className} does not implement the expected IServiceTaskDelegate interface.");
            }
         }
         catch (Exception ex)
         {
             _logger.LogError(ex, "Failed to execute class delegate {ClassName}", className);
             context.Execution.Fail($"Delegate execution failed: {ex.Message}");
             await context.UnitOfWork.SaveChangesAsync(cancellationToken); // Save failure state
             return false;
         }
    }

    private async Task<bool> ExecuteDelegateExpressionAsync(string expression, FlowNodeHandlerContext context, CancellationToken cancellationToken)
    {
        _logger.LogWarning("Delegate Expression execution for ServiceTask {NodeId} ('{Expression}') is not yet implemented.", context.Execution.CurrentFlowNodeId, expression);
        // TODO: Implement logic to parse expression (e.g., beanName.methodName), resolve bean via _serviceProvider, and invoke method.
        await Task.Delay(10, cancellationToken); // Placeholder
        // For now, assume success
        return true;
    }

    private async Task<bool> ExecuteExpressionAsync(string expression, FlowNodeHandlerContext context, CancellationToken cancellationToken)
    {
         _logger.LogWarning("Expression execution for ServiceTask {NodeId} ('{Expression}') is not yet implemented.", context.Execution.CurrentFlowNodeId, expression);
        // TODO: Implement expression evaluation logic using an expression language engine (JUEL, FEEL, C# Scripting?)
        //       Update context.Execution.ProcessInstance.Variables based on result.
        await Task.Delay(10, cancellationToken); // Placeholder
        // For now, assume success
         return true;
    }

    // TODO: Add ExecuteHttpAsync / ExecuteWebServiceAsync method
}

// --- Example Delegate Interface (Define appropriately) ---
public interface IServiceTaskDelegate
{
    Task ExecuteAsync(FlowNodeHandlerContext context, CancellationToken cancellationToken);
} 