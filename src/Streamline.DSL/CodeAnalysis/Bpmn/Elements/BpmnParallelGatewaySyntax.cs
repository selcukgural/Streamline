namespace Streamline.DSL.CodeAnalysis.Bpmn.Elements;

/// <summary>
/// Represents a BPMN Parallel Gateway element in the DSL syntax tree.
/// </summary>
public class BpmnParallelGatewaySyntax : BpmnElementSyntax
{
    /// <summary>
    /// Gets the token representing the 'ParallelGateway' keyword.
    /// </summary>
    public SyntaxToken ParallelGatewayElement { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BpmnParallelGatewaySyntax"/> class.
    /// </summary>
    /// <param name="parallelGatewayElement">The 'ParallelGateway' keyword token.</param>
    /// <param name="openParenthesisToken">The opening parenthesis token.</param>
    /// <param name="attributes">The list of parameters for the Parallel Gateway.</param>
    /// <param name="closeParenthesisToken">The closing parenthesis token.</param>
    public BpmnParallelGatewaySyntax(
        SyntaxToken parallelGatewayElement,
        SyntaxToken openParenthesisToken,
        IDictionary<string,BpmnAttributeSyntax> attributes,
        SyntaxToken closeParenthesisToken)
        : base(parallelGatewayElement, openParenthesisToken, attributes, closeParenthesisToken)
    {
        ParallelGatewayElement = parallelGatewayElement;
    }

    /// <summary>
    /// Gets the syntax kind of this node.
    /// </summary>
    public override SyntaxKind Kind => SyntaxKind.ParallelGatewayElement;
}