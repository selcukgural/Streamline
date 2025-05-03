using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Streamline.Domain.Schema.Common;
using Streamline.Engine.Abstractions;

namespace Streamline.Engine.Services; // Correct namespace for the implementation

public class FlowNodeHandlerFactory(IServiceProvider serviceProvider, ILogger<FlowNodeHandlerFactory> logger) : IFlowNodeHandlerFactory // Implement correct interface
{
    /// <summary>
    /// Gets the handler for the specified flow node using generic type resolution.
    /// </summary>
    /// <typeparam name="TNode">The specific type of the flow node.</typeparam>
    /// <param name="node">The flow node instance.</param>
    /// <returns>The handler instance, or throws an exception if no handler is registered.</returns>
    public IFlowNodeHandler<TNode> GetHandler<TNode>(TNode node) where TNode : FlowNode
    {
        ArgumentNullException.ThrowIfNull(node);

        // Construct the specific handler interface type (e.g., IFlowNodeHandler<StartEvent>)
        var handlerType = typeof(IFlowNodeHandler<>).MakeGenericType(node.GetType());

        logger.LogDebug("Attempting to resolve handler for node type: {NodeType} (ID: {NodeId}) using handler interface type: {HandlerInterfaceType}", 
                         node.GetType().Name, node.Id, handlerType.Name);

        try
        {
            // Resolve the specific handler implementation from the DI container
            // This relies on handlers being registered correctly 
            var handler = serviceProvider.GetRequiredService(handlerType);
            
            if (handler is IFlowNodeHandler<TNode> typedHandler)
            {
                logger.LogDebug("Successfully resolved handler {HandlerImplementationType} for node type {NodeType} (ID: {NodeId})",
                                 handler.GetType().Name, node.GetType().Name, node.Id);
                return typedHandler;
            }
            else
            {
                logger.LogError("Resolved service for {HandlerInterfaceType} is not assignable to IFlowNodeHandler<{NodeType}>. Resolved type: {ResolvedType}", 
                                handlerType.Name, typeof(TNode).Name, handler?.GetType().Name ?? "null");
                throw new InvalidOperationException($"Resolved handler for {node.GetType().Name} is not of the expected type IFlowNodeHandler<{typeof(TNode).Name}>.");
            }
        }
        catch (Exception ex) // Catch DI resolution errors
        {
            logger.LogError(ex, "Error resolving handler of type {HandlerInterfaceType} for node type {NodeType} (ID: {NodeId}) from service provider.",
                           handlerType.Name, node.GetType().Name, node.Id);
            throw new NotSupportedException($"Handler for node type {node.GetType().Name} (Node ID: {node.Id}) could not be resolved. See inner exception for details.", ex);
        }
    }
} 