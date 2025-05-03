using Streamline.Domain.Runtime;
using Streamline.Domain.Schema.Flow;
using Streamline.Domain.Schema.Events;

// Namespace updated to Domain.Abstractions
namespace Streamline.Domain.Abstractions; 

/// <summary>
/// Manages the flow of execution within a process instance based on the BPMN definition.
/// </summary>
public interface IExecutionFlowManager
{
    /// <summary>
    /// Continues the execution from its current state, determining and moving to the next node(s).
    /// </summary>
    /// <param name="executionId">The ID of the execution (token) to continue.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task ContinueExecutionAsync(Guid executionId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new execution for a specific outgoing sequence flow from a parallel gateway fork,
    /// places it at the target node, and triggers its continuation.
    /// The incoming execution should be terminated before calling this.
    /// </summary>
    /// <param name="originalExecution">The original execution arriving at the gateway (used for context).</param>
    /// <param name="definitions">The process definitions object.</param>
    /// <param name="flowToFollow">The specific sequence flow the new execution should follow.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task ForkAndContinueExecutionAsync(Execution originalExecution, Definitions definitions, SequenceFlow flowToFollow, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new execution starting directly at a specified flow node within a process instance,
    /// and immediately triggers its continuation.
    /// </summary>
    /// <param name="processInstanceId">The ID of the process instance.</param>
    /// <param name="targetFlowNodeId">The ID of the FlowNode where the new execution should start.</param>
    /// <param name="initiatingExecutionId">Optional: The ID of the execution that triggered this start (e.g., for non-interrupting boundary events).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The newly created and started Execution.</returns>
    Task<Execution> StartExecutionAtNodeAsync(Guid processInstanceId, string targetFlowNodeId, Guid? initiatingExecutionId = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Triggers the logic associated with a Boundary Timer Event firing.
    /// </summary>
    /// <param name="subscription">The timer event subscription that fired.</param>
    /// <param name="cancelActivity">Indicates if the boundary event is interrupting.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task TriggerBoundaryTimerEventAsync(EventSubscription subscription, bool cancelActivity, CancellationToken cancellationToken);

    /// <summary>
    /// Searches for and executes an appropriate Error Boundary Event or Error Start Event 
    /// within the current scope or parent scopes.
    /// </summary>
    /// <param name="failedExecution">The execution that failed and triggered the search.</param>
    /// <param name="definitions">The process definitions.</param>
    /// <param name="errorCode">The error code to search for.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the error was handled, false otherwise.</returns>
    Task<bool> FindAndHandleErrorAsync(Execution failedExecution, Definitions definitions, string errorCode, CancellationToken cancellationToken);

    /// <summary>
    /// Searches for and executes an appropriate Escalation Boundary Event or Escalation Start Event.
    /// </summary>
    /// <param name="originatingExecution">The execution that triggered the escalation.</param>
    /// <param name="definitions">The process definitions.</param>
    /// <param name="escalationCode">The escalation code to search for.</param>
    /// <param name="escalatedAtNodeId">The ID of the node where the escalation was originally triggered.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the escalation was handled, false otherwise.</returns>
    Task<bool> FindAndHandleEscalationAsync(Execution originatingExecution, Definitions definitions, string escalationCode, string? escalatedAtNodeId, CancellationToken cancellationToken);

    /// <summary>
    /// Terminates all active executions and cancels active ActivityInstances within a given scope,
    /// typically triggered by an interrupting boundary or event subprocess handler.
    /// </summary>
    /// <param name="scopeExecution">The execution defining the scope (e.g., the scope execution of a SubProcess).</param>
    /// <param name="terminateScopeItself">If true, the scopeExecution itself will also be terminated.</param>
    /// <param name="reason">Reason for termination.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task TerminateScopeContentsAsync(Execution scopeExecution, bool terminateScopeItself, string reason, CancellationToken cancellationToken);
} 