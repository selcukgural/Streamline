namespace Streamline.DSL.CodeAnalysis.Bpmn.Elements;

/// <summary>
/// Base class for all BPMN elements in the DSL syntax tree.
/// </summary>
public abstract class BpmnElementSyntax : SyntaxNode
{
    /// <summary>
    /// Gets the name token for this BPMN element.
    /// </summary>
    public SyntaxToken Element { get; }

    /// <summary>
    /// Gets the opening parenthesis token.
    /// </summary>
    public SyntaxToken OpenParenthesisToken { get; }

    /// <summary>
    /// Gets the collection of parameters defined for this BPMN element.
    /// </summary>
    public IDictionary<string, BpmnAttributeSyntax> Attributes { get; }

    /// <summary>
    /// Gets the closing parenthesis token.
    /// </summary>
    public SyntaxToken CloseParenthesisToken { get; }

    public SyntaxToken? OpenCurlyBracketToken { get; }
    public List<BpmnElementSyntax>? Elements { get; }
    public SyntaxToken? CloseCurlyBracketToken { get; }

    /// <summary>
    /// Initializes a new instance of the BpmnElementSyntax class.
    /// </summary>
    protected BpmnElementSyntax(
        SyntaxToken element,
        SyntaxToken openParenthesisToken,
        IDictionary<string, BpmnAttributeSyntax> attributes,
        SyntaxToken closeParenthesisToken, 
        SyntaxToken? openCurlyBracketToken = null,
        List<BpmnElementSyntax>? elements = null, 
        SyntaxToken? closeCurlyBracketToken = null)
    {
        Element = element;
        OpenParenthesisToken = openParenthesisToken;
        Attributes = attributes;
        CloseParenthesisToken = closeParenthesisToken;
        OpenCurlyBracketToken = openCurlyBracketToken;
        Elements = elements;
        CloseCurlyBracketToken = closeCurlyBracketToken;
    }

    /// <summary>
    /// Gets the children of this syntax node.
    /// </summary>
    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return Element;
        yield return OpenParenthesisToken;

        foreach (var parameter in Attributes)
        {
            yield return parameter.Value;
        }

        yield return CloseParenthesisToken;

        if (OpenCurlyBracketToken != null)
        {
            yield return OpenCurlyBracketToken;
        }
        
        if (Elements != null)
        {
            foreach (var element in Elements)
            {
                yield return element;
            }
        }
        if (CloseCurlyBracketToken != null)
        {
            yield return CloseCurlyBracketToken;
        }
    }
}