namespace Streamline.DSL.CodeAnalysis.Bpmn.Elements;

/// <summary>
/// Represents a BPMN Start Event element in the DSL syntax tree.
/// </summary>
public sealed class BpmnStartEventSyntax : BpmnElementSyntax
{
    /// <summary>
    /// Gets the token representing the 'StartEvent' keyword.
    /// </summary>
    public SyntaxToken StartEventElement { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BpmnStartEventSyntax"/> class.
    /// </summary>
    /// <param name="startEventElement">The 'StartEvent' keyword token.</param>
    /// <param name="openParenthesisToken">The opening parenthesis token.</param>
    /// <param name="attributes">The list of parameters for the Start Event.</param>
    /// <param name="closeParenthesisToken">The closing parenthesis token.</param>
    public BpmnStartEventSyntax(
        SyntaxToken startEventElement,
        SyntaxToken openParenthesisToken,
        IDictionary<string,BpmnAttributeSyntax> attributes,
        SyntaxToken closeParenthesisToken,
        SyntaxToken? openCurlyBracketToken = null,
        List<BpmnElementSyntax>? elements = null,
        SyntaxToken? closeCurlyBracketToken = null)
        : base(startEventElement, openParenthesisToken, attributes, closeParenthesisToken,
            openCurlyBracketToken, elements, closeCurlyBracketToken)
    {
        StartEventElement = startEventElement;
    }

    /// <summary>
    /// Gets the syntax kind of this node.
    /// </summary>
    public override SyntaxKind Kind => SyntaxKind.StartEventElement;
}