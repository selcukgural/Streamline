namespace Streamline.DSL.CodeAnalysis.Bpmn.Elements;

/// <summary>
/// Represents a BPMN process declaration in the DSL syntax tree.
/// </summary>
public sealed class BpmnProcessDeclarationSyntax : SyntaxNode
{
    /// <summary>
    /// Gets the syntax kind of this node.
    /// </summary>
    public override SyntaxKind Kind { get; } = SyntaxKind.BpmnProcessElement;

    /// <summary>
    /// Gets the token representing the 'Process' keyword.
    /// </summary>
    public SyntaxToken ProcessKeyword { get; }

    /// <summary>
    /// Gets the opening parenthesis token.
    /// </summary>
    public SyntaxToken OpenParenthesisToken { get; }

    /// <summary>
    /// Gets the list of parameters for the process declaration.
    /// </summary>
    public IDictionary<string,BpmnAttributeSyntax> Parameters { get; }

    /// <summary>
    /// Gets the closing parenthesis token.
    /// </summary>
    public SyntaxToken CloseParenthesisToken { get; }

    /// <summary>
    /// Gets the opening curly bracket token.
    /// </summary>
    public SyntaxToken OpenCurlyBracketToken { get; }

    /// <summary>
    /// Gets the list of BPMN elements within the process.
    /// </summary>
    public List<BpmnElementSyntax> Elements { get; }

    /// <summary>
    /// Gets the closing curly bracket token.
    /// </summary>
    public SyntaxToken CloseCurlyBracketToken { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BpmnProcessDeclarationSyntax"/> class.
    /// </summary>
    /// <param name="processKeyword">The 'Process' keyword token.</param>
    /// <param name="openParenthesisToken">The opening parenthesis token.</param>
    /// <param name="parameters">The list of parameters for the process.</param>
    /// <param name="closeParenthesisToken">The closing parenthesis token.</param>
    /// <param name="openCurlyBracketToken">The opening curly bracket token.</param>
    /// <param name="elements">The list of BPMN elements within the process.</param>
    /// <param name="closeCurlyBracketToken">The closing curly bracket token.</param>
    public BpmnProcessDeclarationSyntax(
        SyntaxToken processKeyword,
        SyntaxToken openParenthesisToken,
        IDictionary<string,BpmnAttributeSyntax> parameters,
        SyntaxToken closeParenthesisToken,
        SyntaxToken openCurlyBracketToken,
        List<BpmnElementSyntax> elements,
        SyntaxToken closeCurlyBracketToken)
    {
        ProcessKeyword = processKeyword;
        OpenParenthesisToken = openParenthesisToken;
        Parameters = parameters;
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
        yield return ProcessKeyword;
        yield return OpenParenthesisToken;

        foreach (var parameter in Parameters)
            yield return parameter.Value;

        yield return CloseParenthesisToken;
        yield return OpenCurlyBracketToken;

        foreach (var element in Elements)
            yield return element;

        yield return CloseCurlyBracketToken;
    }
}