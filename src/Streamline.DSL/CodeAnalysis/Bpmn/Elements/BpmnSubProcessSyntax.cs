namespace Streamline.DSL.CodeAnalysis.Bpmn.Elements;

/// <summary>
/// Represents a BPMN Subprocess element in the DSL syntax tree.
/// </summary>
public sealed class BpmnSubprocessSyntax : BpmnElementSyntax
{
    /// <summary>
    /// Gets the token representing the 'Subprocess' keyword.
    /// </summary>
    public SyntaxToken SubprocessElement { get; }

    /// <summary>
    /// Gets the opening curly bracket token.
    /// </summary>
    public SyntaxToken OpenCurlyBracketToken { get; }

    /// <summary>
    /// Gets the list of BPMN elements within the subprocess.
    /// </summary>
    public List<BpmnElementSyntax> Elements { get; }

    /// <summary>
    /// Gets the closing curly bracket token.
    /// </summary>
    public SyntaxToken CloseCurlyBracketToken { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BpmnSubprocessSyntax"/> class.
    /// </summary>
    /// <param name="subprocessElement">The 'Subprocess' keyword token.</param>
    /// <param name="openParenthesisToken">The opening parenthesis token.</param>
    /// <param name="attributes">The list of parameters for the Subprocess.</param>
    /// <param name="closeParenthesisToken">The closing parenthesis token.</param>
    /// <param name="openCurlyBracketToken">The opening curly bracket token.</param>
    /// <param name="elements">The list of BPMN elements within the subprocess.</param>
    /// <param name="closeCurlyBracketToken">The closing curly bracket token.</param>
    public BpmnSubprocessSyntax(
        SyntaxToken subprocessElement,
        SyntaxToken openParenthesisToken,
        IDictionary<string,BpmnAttributeSyntax> attributes,
        SyntaxToken closeParenthesisToken,
        SyntaxToken openCurlyBracketToken,
        List<BpmnElementSyntax> elements,
        SyntaxToken closeCurlyBracketToken)
        : base(subprocessElement, openParenthesisToken, attributes, closeParenthesisToken)
    {
        SubprocessElement = subprocessElement;
        OpenCurlyBracketToken = openCurlyBracketToken;
        Elements = elements;
        CloseCurlyBracketToken = closeCurlyBracketToken;
    }

    /// <summary>
    /// Gets the syntax kind of this node.
    /// </summary>
    public override SyntaxKind Kind => SyntaxKind.SubprocessElement;

    /// <summary>
    /// Gets the children of this syntax node.
    /// </summary>
    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return SubprocessElement;
        yield return OpenParenthesisToken;

        foreach (var parameter in Attributes)
            yield return parameter.Value;

        yield return CloseParenthesisToken;
        yield return OpenCurlyBracketToken;

        foreach (var element in Elements)
            yield return element;

        yield return CloseCurlyBracketToken;
    }
}