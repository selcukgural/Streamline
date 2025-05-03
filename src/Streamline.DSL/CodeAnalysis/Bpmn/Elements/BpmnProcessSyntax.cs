namespace Streamline.DSL.CodeAnalysis.Bpmn.Elements;

public sealed class BpmnProcessSyntax(
    SyntaxToken processKeyword,
    SyntaxToken openParenthesis,
    IDictionary<string, BpmnAttributeSyntax> attributes,
    SyntaxToken closeParenthesis,
    SyntaxToken openBrace,
    List<BpmnElementSyntax> elements,
    SyntaxToken closeBrace)
    : SyntaxNode
{
    public SyntaxToken ProcessKeyword { get; } = processKeyword;
    public SyntaxToken OpenParenthesis { get; } = openParenthesis;
    public IDictionary<string, BpmnAttributeSyntax> Attributes { get; } = attributes;
    public SyntaxToken CloseParenthesis { get; } = closeParenthesis;
    public SyntaxToken OpenBrace { get; } = openBrace;
    public List<BpmnElementSyntax> Elements { get; } = elements;
    public SyntaxToken CloseBrace { get; } = closeBrace;



    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return ProcessKeyword;
        yield return OpenParenthesis;
        foreach (var attribute in Attributes)
        {
            yield return attribute.Value;
        }

        yield return CloseParenthesis;
        yield return OpenBrace;
        foreach (var element in Elements)
        {
            yield return element;
        }
        yield return CloseBrace;
    }

    public override SyntaxKind Kind => SyntaxKind.BpmnProcessElement;
}