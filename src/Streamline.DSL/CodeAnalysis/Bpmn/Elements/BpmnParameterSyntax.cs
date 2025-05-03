
namespace Streamline.DSL.CodeAnalysis.Bpmn.Elements;

public sealed class BpmnParameterSyntax(SyntaxToken nameToken, SyntaxToken equalsToken, SyntaxToken valueToken)
    : SyntaxNode
{
    public override SyntaxKind Kind { get; } = SyntaxKind.BpmnParameterToken;
    public SyntaxToken NameToken { get; } = nameToken;
    public SyntaxToken EqualsToken { get; } = equalsToken;
    public SyntaxToken ValueToken { get; } = valueToken;

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return NameToken;
        yield return EqualsToken;
        yield return ValueToken;
    }
}