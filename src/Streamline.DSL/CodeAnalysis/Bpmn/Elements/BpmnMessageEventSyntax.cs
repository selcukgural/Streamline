namespace Streamline.DSL.CodeAnalysis.Bpmn.Elements;

/// <summary>
/// Represents a BPMN Message Event element in the DSL syntax tree.
/// </summary>
public sealed class BpmnMessageEventSyntax : BpmnElementSyntax
{
    /// <summary>
    /// Gets the token representing the 'MessageEvent' keyword.
    /// </summary>
    public SyntaxToken MessageEventElement { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BpmnMessageEventSyntax"/> class.
    /// </summary>
    /// <param name="messageEventElement">The 'MessageEvent' keyword token.</param>
    /// <param name="openParenthesisToken">The opening parenthesis token.</param>
    /// <param name="attributes">The list of parameters for the Message Event.</param>
    /// <param name="closeParenthesisToken">The closing parenthesis token.</param>
    public BpmnMessageEventSyntax(
        SyntaxToken messageEventElement,
        SyntaxToken openParenthesisToken,
        IDictionary<string,BpmnAttributeSyntax> attributes,
        SyntaxToken closeParenthesisToken)
        : base(messageEventElement, openParenthesisToken, attributes, closeParenthesisToken)
    {
        MessageEventElement = messageEventElement;
    }

    /// <summary>
    /// Gets the syntax kind of this node.
    /// </summary>
    public override SyntaxKind Kind => SyntaxKind.MessageEventElement;
}