using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Extensions.Logging;
using Streamline.Domain.Runtime; // For Execution
using Streamline.Domain.Schema.Activities; // For ScriptTask
using Streamline.Domain.Schema.Common;
using Streamline.Engine.Abstractions;
using Task = System.Threading.Tasks.Task;

namespace Streamline.Engine.Services.Handlers;

/// <summary>
/// Handles Script Tasks by executing embedded C# scripts using Roslyn.
/// </summary>
public class CSharpScriptTaskHandler(ILogger<CSharpScriptTaskHandler> logger) : IFlowNodeHandler<ScriptTask> // Renamed class and logger type
{
    private static readonly ScriptOptions _csharpScriptOptions = ScriptOptions.Default
        .WithImports("System", "System.Linq", "System.Collections.Generic")
        .WithReferences(typeof(Execution).Assembly);

    public async Task ExecuteAsync(FlowNode node, FlowNodeHandlerContext context, CancellationToken cancellationToken)
    {
        if (node is not ScriptTask scriptTask)
        {
            logger.LogError("Node type mismatch. Expected ScriptTask but got {NodeType} for Node {NodeId}", node.GetType().Name, node.Id);
            context.Execution.Fail("Incorrect node type passed to CSharpScriptTaskHandler");
            await context.UnitOfWork.SaveChangesAsync(cancellationToken);
            return;
        }

        var scriptLines = scriptTask.Script?.Text;
        var scriptContent = scriptLines != null && scriptLines.Length > 0 
                            ? string.Join("\n", scriptLines) 
                            : null;
                            
        var scriptFormat = scriptTask.ScriptFormat?.ToLowerInvariant().Trim();

        logger.LogInformation("Executing C# ScriptTask handler for Node {NodeId} ({NodeName}) - Format: {Format}",
            scriptTask.Id, scriptTask.Name ?? "Unnamed ScriptTask", scriptFormat ?? "(none)");

        // Basic check if it looks like a C# format - Factory should ideally pre-filter
        if (scriptFormat != "text/csharp" && scriptFormat != "application/csharp" && scriptFormat != "csharp")
        {
             logger.LogWarning("CSharpScriptTaskHandler received task with non-C# format '{Format}'. Skipping execution. Factory should prevent this.", scriptFormat, scriptTask.Id);
             // Don't fail, just skip and continue, assuming factory made a mistake or format is irrelevant for this handler
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
            await ExecuteCSharpScriptInternal(scriptTask, scriptContent, context, cancellationToken);

            // Check if the internal execution already failed the execution
            if (!context.Execution.IsActive)
            {
                 logger.LogWarning("Execution {ExecutionId} became inactive during C# script execution for {NodeId}. Not continuing.", context.Execution.Id, scriptTask.Id);
                 return; // Do not continue if failed
            }

            // If execution reaches here without an exception and still active, continue the flow.
            logger.LogInformation("C# Script executed successfully for ScriptTask {NodeId}. Continuing execution.", scriptTask.Id);
            await context.ExecutionFlowManager.ContinueExecutionAsync(context.Execution.Id, cancellationToken);
        }
        catch (Exception ex) // Catch exceptions re-thrown by helper method
        {
             // Check if already failed to avoid double logging/saving
             if (context.Execution.IsActive) // Corrected: Check IsActive instead of non-existent IsFailed
             {
                 logger.LogError(ex, "Unhandled exception caught after C# script execution attempt for ScriptTask {NodeId}. Failing execution.", scriptTask.Id);
                 context.Execution.Fail("Unhandled C# script execution error", ex.Message);
                 try { await context.UnitOfWork.SaveChangesAsync(cancellationToken); }
                 catch (Exception saveEx) { logger.LogError(saveEx, "Failed to save execution state after unhandled C# script error."); }
             }
             // Do not continue execution
        }
    }

    private async Task ExecuteCSharpScriptInternal(ScriptTask scriptTask, string scriptContent, FlowNodeHandlerContext context, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogDebug("Preparing to execute C# script for ScriptTask {NodeId}:\n{Script}", scriptTask.Id, scriptContent);
            var globals = new ScriptExecutionGlobals { Execution = context.Execution };
            var scriptState = await CSharpScript.RunAsync(scriptContent, _csharpScriptOptions, globals, typeof(ScriptExecutionGlobals), cancellationToken);

            if (scriptState.Exception != null)
            {
                throw scriptState.Exception;
            }
            logger.LogDebug("C# Script for {NodeId} executed. Return value: {ReturnValue}", scriptTask.Id, scriptState.ReturnValue ?? "(null)");
        }
        catch (CompilationErrorException ex)
        {
            var diagnostics = string.Join("\n", ex.Diagnostics.Select(d => d.ToString()));
            logger.LogError(ex, "C# Script compilation failed for ScriptTask {NodeId}:\n{Diagnostics}", scriptTask.Id, diagnostics);
            context.Execution.Fail("C# Script compilation failed", diagnostics);
            await context.UnitOfWork.SaveChangesAsync(cancellationToken);
            throw; // Re-throw
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error executing C# script for ScriptTask {NodeId}", scriptTask.Id);
            context.Execution.Fail("C# Script execution failed", ex.Message);
            await context.UnitOfWork.SaveChangesAsync(cancellationToken);
            throw; // Re-throw
        }
    }
}

internal class ScriptExecutionGlobals
{
    public Execution Execution { get; set; } = null!;
    // Add other global variables or methods as needed
}