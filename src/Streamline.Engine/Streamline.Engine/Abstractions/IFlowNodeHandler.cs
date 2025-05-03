// Definitions - Not accurate, Definitions is likely elsewhere or this using is not needed here
// Use Common for FlowNode
// using Streamline.Engine.Schema.Flow;   // FlowNode - Removed, FlowNode is in Common

// using MediatR; // Removed using for IMediator
// Task için
using Streamline.Domain.Schema.Common; // CancellationToken için

namespace Streamline.Engine.Abstractions;

/// <summary>
/// Generic interface for handling specific flow node types.
/// </summary>
/// <typeparam name="TNode">The specific type of FlowNode.</typeparam>
public interface IFlowNodeHandler<in TNode> : IFlowNodeHandler where TNode : FlowNode
{
    // Specific methods for the generic type can be added here if needed,
    // but often the non-generic ExecuteAsync is sufficient.
}

/// <summary>
/// Non-generic base interface for all flow node handlers.
/// Used by the factory to create and manage handlers.
/// </summary>
public interface IFlowNodeHandler
{
    /// <summary>
    /// Executes the primary logic for the flow node.
    /// </summary>
    /// <param name="node">The flow node instance (passed as base FlowNode).</param>
    /// <param name="context">The execution context.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task ExecuteAsync(FlowNode node, FlowNodeHandlerContext context, CancellationToken cancellationToken);
}