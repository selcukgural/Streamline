namespace Streamline.DSL.CodeAnalysis.Bpmn;

public abstract class SyntaxNode
{
    public abstract SyntaxKind Kind { get; }

    public abstract IEnumerable<SyntaxNode> GetChildren();

}