namespace Streamline.DSL.CodeAnalysis.Bpmn.Elements;

/// <summary>
/// Represents a BPMN Event element in the DSL syntax tree.
/// </summary>
public sealed class BpmnEventSyntax : BpmnElementSyntax
{
    /// <summary>
    /// Gets the token representing the 'Event' keyword.
    /// </summary>
    public SyntaxToken EventElement { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BpmnEventSyntax"/> class.
    /// </summary>
    /// <param name="eventElement">The 'Event' keyword token.</param>
    /// <param name="openParenthesisToken">The opening parenthesis token.</param>
    /// <param name="attributes">The list of parameters for the Event.</param>
    /// <param name="closeParenthesisToken">The closing parenthesis token.</param>
    public BpmnEventSyntax(
        SyntaxToken eventElement,
        SyntaxToken openParenthesisToken,
        IDictionary<string,BpmnAttributeSyntax> attributes,
        SyntaxToken closeParenthesisToken)
        : base(eventElement, openParenthesisToken, attributes, closeParenthesisToken)
    {
        EventElement = eventElement;
    }

    /// <summary>
    /// Gets the syntax kind of this node.
    /// </summary>
    public override SyntaxKind Kind => SyntaxKind.EventElement;
}