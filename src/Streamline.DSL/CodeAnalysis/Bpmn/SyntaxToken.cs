namespace Streamline.DSL.CodeAnalysis.Bpmn;

public sealed class SyntaxToken(SyntaxKind kind, int position, string text, object? value = null) : SyntaxNode
{
    public override SyntaxKind Kind { get; } = kind;
    public string Text { get; } = text;
    public object? Value { get; } = value;
    public int Position { get; } = position;

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        return [];
    }
}