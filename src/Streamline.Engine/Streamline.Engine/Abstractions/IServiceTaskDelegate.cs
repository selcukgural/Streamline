// For FlowNodeHandlerContext, consider moving context later?

namespace Streamline.Engine.Abstractions;

/// <summary>
/// Interface for custom logic executed by a Service Task (e.g., via class or delegateExpression).
/// Implementations of this interface can be resolved via DI or activated directly.
/// </summary>
public interface IServiceTaskDelegate
{
    /// <summary>
    /// Executes the custom service task logic.
    /// </summary>
    /// <param name="context">Provides access to execution context, variables, etc.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="System.Exception">Exceptions thrown during execution will typically fail the service task.</exception>
    Task ExecuteAsync(FlowNodeHandlerContext context, CancellationToken cancellationToken);
} 