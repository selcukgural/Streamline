namespace Streamline.DSL.CodeAnalysis.Bpmn.Elements;

/// <summary>
/// Represents a BPMN Exclusive Gateway element in the DSL syntax tree.
/// </summary>
public sealed class BpmnExclusiveGatewaySyntax : BpmnElementSyntax
{
    /// <summary>
    /// Gets the token representing the 'ExclusiveGateway' keyword.
    /// </summary>
    public SyntaxToken ExclusiveGatewayElement { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BpmnExclusiveGatewaySyntax"/> class.
    /// </summary>
    /// <param name="exclusiveGatewayElement">The 'ExclusiveGateway' keyword token.</param>
    /// <param name="openParenthesisToken">The opening parenthesis token.</param>
    /// <param name="attributes">The list of parameters for the Exclusive Gateway.</param>
    /// <param name="closeParenthesisToken">The closing parenthesis token.</param>
    public BpmnExclusiveGatewaySyntax(
        SyntaxToken exclusiveGatewayElement,
        SyntaxToken openParenthesisToken,
        IDictionary<string,BpmnAttributeSyntax> attributes,
        SyntaxToken closeParenthesisToken)
        : base(exclusiveGatewayElement, openParenthesisToken, attributes, closeParenthesisToken)
    {
        ExclusiveGatewayElement = exclusiveGatewayElement;
    }

    /// <summary>
    /// Gets the syntax kind of this node.
    /// </summary>
    public override SyntaxKind Kind => SyntaxKind.ExclusiveGatewayElement;
}