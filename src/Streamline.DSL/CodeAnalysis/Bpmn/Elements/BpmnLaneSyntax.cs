namespace Streamline.DSL.CodeAnalysis.Bpmn.Elements;

/// <summary>
/// Represents a BPMN Lane element in the DSL syntax tree.
/// </summary>
public sealed class BpmnLaneSyntax(
    SyntaxToken laneElement,
    SyntaxToken openBraceToken,
    IDictionary<string,BpmnAttributeSyntax> elements,
    SyntaxToken closeBraceToken)
    : BpmnElementSyntax(laneElement, openBraceToken, elements, closeBraceToken)
{
    public SyntaxToken LaneElement { get; } = laneElement;

    public override SyntaxKind Kind => SyntaxKind.LaneElement;
}