using Streamline.Domain.Schema.Common;
using Streamline.Engine.Abstractions; // For IFlowNodeHandler

namespace Streamline.Engine.Services; // Or maybe Abstractions namespace?

/// <summary>
/// Factory responsible for providing the correct IFlowNodeHandler 
/// instance for a given FlowNode.
/// </summary>
public interface IFlowNodeHandlerFactory
{
    /// <summary>
    /// Gets the handler for the specified flow node.
    /// </summary>
    /// <typeparam name="TNode">The specific type of the flow node.</typeparam>
    /// <param name="node">The flow node instance.</param>
    /// <returns>The handler instance, or null if no handler is registered for the type.</returns>
    IFlowNodeHandler<TNode>? GetHandler<TNode>(TNode node) where TNode : FlowNode;
}