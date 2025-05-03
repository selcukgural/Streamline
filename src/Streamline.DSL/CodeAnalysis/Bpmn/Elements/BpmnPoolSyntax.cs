namespace Streamline.DSL.CodeAnalysis.Bpmn.Elements;

/// <summary>
/// Represents a BPMN Pool element in the DSL syntax tree.
/// </summary>
public sealed class BpmnPoolSyntax(
    SyntaxToken poolElement,
    SyntaxToken openBraceToken,
    List<BpmnLaneSyntax> lanes,
    SyntaxToken closeBraceToken)
    : BpmnElementSyntax(poolElement, openBraceToken, new Dictionary<string, BpmnAttributeSyntax>(), closeBraceToken)
{
    public SyntaxToken PoolElement { get; } = poolElement;
    
    public List<BpmnLaneSyntax> Lanes { get; } = lanes;
    public override SyntaxKind Kind => SyntaxKind.PoolElement;
}