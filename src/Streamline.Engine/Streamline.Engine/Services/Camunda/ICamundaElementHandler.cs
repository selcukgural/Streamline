using Streamline.Engine.Abstractions;
using System.Xml.Linq;

namespace Streamline.Engine.Services.Camunda;

/// <summary>
/// Interface for handling specific Camunda extension elements found within BPMN elements (like Service Tasks).
/// </summary>
public interface ICamundaElementHandler
{
    /// <summary>
    /// Handles a specific Camunda extension element.
    /// </summary>
    /// <param name="element">The Camunda XML extension element (e.g., <camunda:class>, <camunda:expression>).</param>
    /// <param name="context">The flow node handler context, providing execution details.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>
    /// A tuple containing:
    /// - Handled (bool): True if this handler recognized and processed the element (regardless of success).
    /// - Success (bool): True if the element was handled successfully, false otherwise (or if not handled).
    /// </returns>
    Task<(bool Handled, bool Success)> HandleAsync(XElement element, FlowNodeHandlerContext context, CancellationToken cancellationToken);
} 