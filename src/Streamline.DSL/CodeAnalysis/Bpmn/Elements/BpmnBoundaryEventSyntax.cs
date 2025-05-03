namespace Streamline.DSL.CodeAnalysis.Bpmn.Elements;

/// <summary>
/// Represents a BPMN Boundary Event element in the DSL syntax tree.
/// </summary>
public sealed class BpmnBoundaryEventSyntax : BpmnElementSyntax
{
    /// <summary>
    /// Gets the token representing the 'BoundaryEvent' keyword.
    /// </summary>
    public SyntaxToken BoundaryEventElement { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BpmnBoundaryEventSyntax"/> class.
    /// </summary>
    /// <param name="boundaryEventElement">The 'BoundaryEvent' keyword token.</param>
    /// <param name="openParenthesisToken">The opening parenthesis token.</param>
    /// <param name="attributes">The list of parameters for the Boundary Event.</param>
    /// <param name="closeParenthesisToken">The closing parenthesis token.</param>
    public BpmnBoundaryEventSyntax(
        SyntaxToken boundaryEventElement,
        SyntaxToken openParenthesisToken,
        IDictionary<string,BpmnAttributeSyntax> attributes,
        SyntaxToken closeParenthesisToken)
        : base(boundaryEventElement, openParenthesisToken, attributes, closeParenthesisToken)
    {
        BoundaryEventElement = boundaryEventElement;
    }

    /// <summary>
    /// Gets the syntax kind of this node.
    /// </summary>
    public override SyntaxKind Kind => SyntaxKind.BoundaryEventElement;
}