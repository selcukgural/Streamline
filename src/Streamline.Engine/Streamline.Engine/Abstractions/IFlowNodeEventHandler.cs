// TNode için

// FlowNodeHandlerContext için
// Task için
// CancellationToken için
using Streamline.Domain.Abstractions;
using Streamline.Domain.Runtime; // Added back Runtime namespace
using Streamline.Domain.Schema.Common;
using Streamline.Domain.Schema.Events;
using Streamline.Domain.Schema.Flow; // Added for SequenceFlow

namespace Streamline.Engine.Abstractions;

/// <summary>
/// Represents the context for handling a flow node execution.
/// Contains information about the current execution, definitions, and access to services.
/// </summary>
/// <param name="Execution">The current execution token.</param>
/// <param name="Definitions">The parsed BPMN definitions.</param>
/// <param name="UnitOfWork">The Unit of Work for database operations.</param>
/// <param name="ExecutionFlowManager">The manager responsible for continuing the flow.</param>
/// <param name="ArrivedViaFlow">The sequence flow through which the execution arrived at the current node (if applicable).</param>
public record FlowNodeHandlerContext(
    Execution Execution,
    Definitions Definitions,
    IUnitOfWork UnitOfWork,
    IExecutionFlowManager ExecutionFlowManager,
    SequenceFlow? ArrivedViaFlow = null // Added optional property
);


/// <summary>
/// Defines the contract for handlers that execute logic when a specific type of FlowNode is entered or left.
/// </summary>
/// <typeparam name="TNode">The specific type of FlowNode this handler deals with (e.g., StartEvent, ServiceTask).</typeparam>
public interface IFlowNodeEventHandler<in TNode> : IFlowNodeEventHandler where TNode : FlowNode
{
    /// <summary>
    /// Executes the logic when the execution flow enters the specified node.
    /// </summary>
    /// <param name="node">The specific flow node instance being entered.</param>
    /// <param name="context">The context containing execution details and services.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task HandleEnterAsync(TNode node, FlowNodeHandlerContext context, CancellationToken cancellationToken);

    // Optional: Add HandleLeaveAsync if needed for specific nodes
    // Task HandleLeaveAsync(TNode node, FlowNodeHandlerContext context, CancellationToken cancellationToken);
}

/// <summary>
/// Non-generic marker interface for dependency injection and type discovery (e.g., using Scrutor).
/// Allows retrieving handlers without knowing the specific TNode type beforehand.
/// </summary>
public interface IFlowNodeEventHandler
{
    // This interface is primarily used for type scanning and factory retrieval.
    // It doesn't define any members itself, relying on the generic version for the actual contract.
} 