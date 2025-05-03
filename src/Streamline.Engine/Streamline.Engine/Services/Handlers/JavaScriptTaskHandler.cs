using Jint;
using Jint.Runtime;
using Microsoft.Extensions.Logging;
using Streamline.Domain.Runtime;
using Streamline.Domain.Schema.Activities;
using Streamline.Domain.Schema.Common;
using Streamline.Engine.Abstractions;
using Task = System.Threading.Tasks.Task;

namespace Streamline.Engine.Services.Handlers;

/// <summary>
/// Handles Script Tasks by executing embedded JavaScript scripts using Jint.
/// </summary>
public class JavaScriptTaskHandler(ILogger<JavaScriptTaskHandler> logger) : IFlowNodeHandler<ScriptTask>
{
    public async Task ExecuteAsync(FlowNode node, FlowNodeHandlerContext context, CancellationToken cancellationToken)
    {
        // This handler is currently synchronous due to Jint execution model.
        // The async Task signature is kept for interface consistency.
        // If truly async JS execution is needed later, this needs rethinking.

        if (node is not ScriptTask scriptTask)
        {
            logger.LogError("Node type mismatch. Expected ScriptTask but got {NodeType} for Node {NodeId}", node.GetType().Name, node.Id);
            context.Execution.Fail("Incorrect node type passed to JavaScriptTaskHandler");
            await context.UnitOfWork.SaveChangesAsync(cancellationToken);
            return;
        }

        // Correctly join the string array from Script.Text
        var scriptLines = scriptTask.Script?.Text;
        var scriptContent = scriptLines != null && scriptLines.Length > 0 
                            ? string.Join("\n", scriptLines) 
                            : null;

        var scriptFormat = scriptTask.ScriptFormat?.ToLowerInvariant().Trim();

        logger.LogInformation("Executing JavaScript ScriptTask handler for Node {NodeId} ({NodeName}) - Format: {Format}",
            scriptTask.Id, scriptTask.Name ?? "Unnamed ScriptTask", scriptFormat ?? "(none)");

        // Basic check if it looks like a JS format - Factory should ideally pre-filter
        if (scriptFormat != "text/javascript" && scriptFormat != "application/javascript" && scriptFormat != "javascript")
        {
             logger.LogWarning("JavaScriptTaskHandler received task with non-JS format '{Format}'. Skipping execution. Factory should prevent this.", scriptFormat, scriptTask.Id);
             await context.ExecutionFlowManager.ContinueExecutionAsync(context.Execution.Id, cancellationToken);
             return;
        }

        if (string.IsNullOrWhiteSpace(scriptContent))
        {
            logger.LogWarning("ScriptTask {NodeId} has no script content. Skipping execution.", scriptTask.Id);
            await context.ExecutionFlowManager.ContinueExecutionAsync(context.Execution.Id, cancellationToken);
            return;
        }

        try
        {
            // Pass the joined scriptContent
            ExecuteJavaScriptInternal(scriptTask, scriptContent, context);

            // Check if the internal execution failed the execution
            if (!context.Execution.IsActive)
            {
                logger.LogWarning("Execution {ExecutionId} became inactive during JavaScript execution for {NodeId}. Not continuing.", context.Execution.Id, scriptTask.Id);
                return; // Do not continue if failed
            }

            // If execution reaches here without an exception and still active, continue the flow.
            logger.LogInformation("JavaScript executed successfully for ScriptTask {NodeId}. Continuing execution.", scriptTask.Id);
            await context.ExecutionFlowManager.ContinueExecutionAsync(context.Execution.Id, cancellationToken);
        }
        catch (Exception ex) // Catch exceptions re-thrown by helper method
        {
            // Check if already failed to avoid double logging/saving
             if (context.Execution.IsActive) // Corrected: Check IsActive instead of non-existent IsFailed
             {
                 logger.LogError(ex, "Unhandled exception caught after JavaScript execution attempt for ScriptTask {NodeId}. Failing execution.", scriptTask.Id);
                 context.Execution.Fail("Unhandled JavaScript execution error", ex.Message);
                 // Use Wait() as the outer method isn't fully async for the JS path yet
                 try { await context.UnitOfWork.SaveChangesAsync(cancellationToken); }
                 catch (Exception saveEx) { logger.LogError(saveEx, "Failed to save execution state after unhandled JS script error."); }
             }
             // Do not continue execution
        }
    }

    private void ExecuteJavaScriptInternal(ScriptTask scriptTask, string scriptContent, FlowNodeHandlerContext context)
    {
        try
        {
            // Now scriptContent is guaranteed to be a single string
            logger.LogDebug("Preparing to execute JavaScript for ScriptTask {NodeId}:\n{Script}", scriptTask.Id, scriptContent);
            var engine = new Jint.Engine(options =>
            {
                options.AllowClr(typeof(Execution).Assembly);
                options.TimeoutInterval(TimeSpan.FromSeconds(10));
                options.Strict();
            });

            engine.SetValue("execution", context.Execution);
            // Consider exposing a logger: engine.SetValue("log", new SimpleJsLogger(logger));

            var result = engine.Evaluate(scriptContent);

            logger.LogDebug("JavaScript for {NodeId} executed. Return value: {ReturnValue}", scriptTask.Id, result ?? "(null)");
        }
        catch (JavaScriptException ex)
        {
            // Correctly access Line/Column via ex.Location.Start
            var locationInfo = "(Location info unavailable)";
            if (ex.Location != null) // Check if Location is available
            { 
                // Position struct (Start) defaults to 0,0 if not set
                var line = ex.Location.Start.Line;
                var col = ex.Location.Start.Column;
                if (line > 0) // Check if line number is valid
                {
                    locationInfo = $"Line: {line}, Column: {col}";
                }
            }
            
            var errorDetails = ex.Error?.ToString() ?? ex.Message;
            
            logger.LogError(ex, "JavaScript execution error for ScriptTask {NodeId} at {Location}: {JsError}",
                           scriptTask.Id, locationInfo, errorDetails);
                           
            context.Execution.Fail($"JavaScript execution error: {errorDetails}", locationInfo);
            context.UnitOfWork.SaveChangesAsync().Wait(); 
            throw; 
        }
        catch (Exception ex) // Catch other potential errors
        {
            logger.LogError(ex, "Error setting up or executing JavaScript for ScriptTask {NodeId}", scriptTask.Id);
            context.Execution.Fail("JavaScript engine or setup failed", ex.Message); // No line/column info here
            context.UnitOfWork.SaveChangesAsync().Wait();
            throw; // Re-throw
        }
    }

    // Example logger wrapper if needed later:
    // public class SimpleJsLogger(ILogger baseLogger) { ... }
} 