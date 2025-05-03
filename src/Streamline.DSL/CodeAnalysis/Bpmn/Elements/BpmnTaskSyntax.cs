namespace Streamline.DSL.CodeAnalysis.Bpmn.Elements;

/// <summary>
/// Represents a BPMN Task element in the DSL syntax tree.
/// </summary>
public sealed class BpmnTaskSyntax(
    SyntaxToken taskElement,
    SyntaxToken openParenthesisToken,
    IDictionary<string, BpmnAttributeSyntax> attributes,
    SyntaxToken closeParenthesisToken)
    : BpmnElementSyntax(taskElement, openParenthesisToken, attributes, closeParenthesisToken)
{
    public SyntaxToken TaskElement { get; } = taskElement;

    public override SyntaxKind Kind => SyntaxKind.TaskElement;
}