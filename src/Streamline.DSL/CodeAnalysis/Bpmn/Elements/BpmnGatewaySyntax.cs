namespace Streamline.DSL.CodeAnalysis.Bpmn.Elements;

/// <summary>
/// Represents a BPMN Gateway element in the DSL syntax tree.
/// </summary>
public sealed class GatewaySyntax : BpmnElementSyntax
{
    /// <summary>
    /// Gets the token representing the 'Gateway' keyword.
    /// </summary>
    public SyntaxToken GatewayElement { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GatewaySyntax"/> class.
    /// </summary>
    /// <param name="gatewayElement">The 'Gateway' keyword token.</param>
    /// <param name="openParenthesisToken">The opening parenthesis token.</param>
    /// <param name="attributes">The list of parameters for the Gateway.</param>
    /// <param name="closeParenthesisToken">The closing parenthesis token.</param>
    public GatewaySyntax(
        SyntaxToken gatewayElement,
        SyntaxToken openParenthesisToken,
        IDictionary<string,BpmnAttributeSyntax> attributes,
        SyntaxToken closeParenthesisToken)
        : base(gatewayElement, openParenthesisToken, attributes, closeParenthesisToken)
    {
        GatewayElement = gatewayElement;
    }

    /// <summary>
    /// Gets the syntax kind of this node.
    /// </summary>
    public override SyntaxKind Kind => SyntaxKind.GatewayElement;
}