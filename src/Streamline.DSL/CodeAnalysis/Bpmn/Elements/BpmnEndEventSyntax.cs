namespace Streamline.DSL.CodeAnalysis.Bpmn.Elements;

/// <summary>
/// Represents a BPMN End Event element in the DSL syntax tree.
/// </summary>
public sealed class BpmnEndEventSyntax : BpmnElementSyntax
{
    /// <summary>
    /// Gets the token representing the 'EndEvent' keyword.
    /// </summary>
    public SyntaxToken EndEventElement { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BpmnEndEventSyntax"/> class.
    /// </summary>
    /// <param name="endEventElement">The 'EndEvent' keyword token.</param>
    /// <param name="openParenthesisToken">The opening parenthesis token.</param>
    /// <param name="attributes">The list of parameters for the End Event.</param>
    /// <param name="closeParenthesisToken">The closing parenthesis token.</param>
    public BpmnEndEventSyntax(
        SyntaxToken endEventElement,
        SyntaxToken openParenthesisToken,
        IDictionary<string,BpmnAttributeSyntax> attributes,
        SyntaxToken closeParenthesisToken)
        : base(endEventElement, openParenthesisToken, attributes, closeParenthesisToken)
    {
        EndEventElement = endEventElement;
    }

    /// <summary>
    /// Gets the syntax kind of this node.
    /// </summary>
    public override SyntaxKind Kind => SyntaxKind.EndEventElement;
}