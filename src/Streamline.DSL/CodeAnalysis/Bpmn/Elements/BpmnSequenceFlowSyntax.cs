namespace Streamline.DSL.CodeAnalysis.Bpmn.Elements;

/// <summary>
/// Represents a BPMN Sequence Flow element in the DSL syntax tree.
/// </summary>
public sealed class BpmnSequenceFlowSyntax : BpmnElementSyntax
{
    /// <summary>
    /// Gets the token representing the 'SequenceFlow' keyword.
    /// </summary>
    public SyntaxToken SequenceFlowElement { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BpmnSequenceFlowSyntax"/> class.
    /// </summary>
    /// <param name="sequenceFlowElement">The 'SequenceFlow' keyword token.</param>
    /// <param name="openParenthesisToken">The opening parenthesis token.</param>
    /// <param name="attributes">The list of parameters for the Sequence Flow.</param>
    /// <param name="closeParenthesisToken">The closing parenthesis token.</param>
    public BpmnSequenceFlowSyntax(
        SyntaxToken sequenceFlowElement,
        SyntaxToken openParenthesisToken,
        IDictionary<string,BpmnAttributeSyntax> attributes,
        SyntaxToken closeParenthesisToken)
        : base(sequenceFlowElement, openParenthesisToken, attributes, closeParenthesisToken)
    {
        SequenceFlowElement = sequenceFlowElement;
    }

    /// <summary>
    /// Gets the syntax kind of this node.
    /// </summary>
    public override SyntaxKind Kind => SyntaxKind.SequenceFlowElement;
}