namespace Streamline.DSL.CodeAnalysis.Bpmn.Elements;

/// <summary>
/// Represents a BPMN Error Event element in the DSL syntax tree.
/// </summary>
public sealed class BpmnErrorEventSyntax : BpmnElementSyntax
{
    /// <summary>
    /// Gets the token representing the 'ErrorEvent' keyword.
    /// </summary>
    public SyntaxToken ErrorEventElement { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BpmnErrorEventSyntax"/> class.
    /// </summary>
    /// <param name="errorEventElement">The 'ErrorEvent' keyword token.</param>
    /// <param name="openParenthesisToken">The opening parenthesis token.</param>
    /// <param name="attributes">The list of parameters for the Error Event.</param>
    /// <param name="closeParenthesisToken">The closing parenthesis token.</param>
    public BpmnErrorEventSyntax(
        SyntaxToken errorEventElement,
        SyntaxToken openParenthesisToken,
        IDictionary<string,BpmnAttributeSyntax>attributes,
        SyntaxToken closeParenthesisToken)
        : base(errorEventElement, openParenthesisToken, attributes, closeParenthesisToken)
    {
        ErrorEventElement = errorEventElement;
    }

    /// <summary>
    /// Gets the syntax kind of this node.
    /// </summary>
    public override SyntaxKind Kind => SyntaxKind.ErrorEventElement;
}