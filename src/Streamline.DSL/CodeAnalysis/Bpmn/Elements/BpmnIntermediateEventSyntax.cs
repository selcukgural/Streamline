namespace Streamline.DSL.CodeAnalysis.Bpmn.Elements;

/// <summary>
/// Represents a BPMN Intermediate Event element in the DSL syntax tree.
/// </summary>
public sealed class BpmnIntermediateEventSyntax : BpmnElementSyntax
{
    /// <summary>
    /// Gets the token representing the 'IntermediateEvent' keyword.
    /// </summary>
    public SyntaxToken IntermediateEventElement { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BpmnIntermediateEventSyntax"/> class.
    /// </summary>
    /// <param name="intermediateEventElement">The 'IntermediateEvent' keyword token.</param>
    /// <param name="openParenthesisToken">The opening parenthesis token.</param>
    /// <param name="attributes">The list of parameters for the Intermediate Event.</param>
    /// <param name="closeParenthesisToken">The closing parenthesis token.</param>
    public BpmnIntermediateEventSyntax(
        SyntaxToken intermediateEventElement,
        SyntaxToken openParenthesisToken,
        IDictionary<string,BpmnAttributeSyntax> attributes,
        SyntaxToken closeParenthesisToken)
        : base(intermediateEventElement, openParenthesisToken, attributes, closeParenthesisToken)
    {
        IntermediateEventElement = intermediateEventElement;
    }

    /// <summary>
    /// Gets the syntax kind of this node.
    /// </summary>
    public override SyntaxKind Kind => SyntaxKind.IntermediateEventElement;
}