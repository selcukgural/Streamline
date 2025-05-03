namespace Streamline.DSL.CodeAnalysis.Bpmn.Elements;

public sealed class BpmnAttributeSyntax(SyntaxToken identifier, SyntaxToken equal, SyntaxToken value)
    : SyntaxNode
{
    public override SyntaxKind Kind { get; } = SyntaxKind.BpmnAttributeToken;
    public SyntaxToken Identifier { get; } = identifier;
    public SyntaxToken Equal { get; } = equal;
    public SyntaxToken Value { get; } = value;

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return Identifier;
        yield return Equal;
        yield return Value;
    }
}