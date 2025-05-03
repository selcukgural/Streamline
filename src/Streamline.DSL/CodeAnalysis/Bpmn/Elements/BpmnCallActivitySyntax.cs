
namespace Streamline.DSL.CodeAnalysis.Bpmn.Elements;

/// <summary>
/// Represents a BPMN Call Activity element in the DSL syntax tree.
/// </summary>
public sealed class BpmnCallActivitySyntax : BpmnElementSyntax
{
    /// <summary>
    /// Gets the token representing the 'CallActivity' keyword.
    /// </summary>
    public SyntaxToken CallActivityElement { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BpmnCallActivitySyntax"/> class.
    /// </summary>
    /// <param name="callActivityElement">The 'CallActivity' keyword token.</param>
    /// <param name="openParenthesisToken">The opening parenthesis token.</param>
    /// <param name="attributes">The list of parameters for the Call Activity.</param>
    /// <param name="closeParenthesisToken">The closing parenthesis token.</param>
    public BpmnCallActivitySyntax(
        SyntaxToken callActivityElement,
        SyntaxToken openParenthesisToken,
        IDictionary<string,BpmnAttributeSyntax> attributes,
        SyntaxToken closeParenthesisToken)
        : base(callActivityElement, openParenthesisToken, attributes, closeParenthesisToken)
    {
        CallActivityElement = callActivityElement;
    }

    /// <summary>
    /// Gets the syntax kind of this node.
    /// </summary>
    public override SyntaxKind Kind => SyntaxKind.CallActivityElement;
}