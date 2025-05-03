namespace Streamline.DSL.CodeAnalysis.Bpmn.Elements;

/// <summary>
/// Represents a BPMN Event Subprocess element in the DSL syntax tree.
/// </summary>
public sealed class BpmnEventSubprocessSyntax : BpmnElementSyntax
{
    /// <summary>
    /// Gets the token representing the 'EventSubprocess' keyword.
    /// </summary>
    public SyntaxToken EventSubprocessElement { get; }

    /// <summary>
    /// Gets the opening curly bracket token.
    /// </summary>
    public SyntaxToken OpenCurlyBracketToken { get; }

    /// <summary>
    /// Gets the list of BPMN elements within the Event Subprocess.
    /// </summary>
    public List<BpmnElementSyntax> Elements { get; }

    /// <summary>
    /// Gets the closing curly bracket token.
    /// </summary>
    public SyntaxToken CloseCurlyBracketToken { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BpmnEventSubprocessSyntax"/> class.
    /// </summary>
    /// <param name="eventSubprocessElement">The 'EventSubprocess' keyword token.</param>
    /// <param name="openParenthesisToken">The opening parenthesis token.</param>
    /// <param name="attributes">The list of parameters for the Event Subprocess.</param>
    /// <param name="closeParenthesisToken">The closing parenthesis token.</param>
    /// <param name="openCurlyBracketToken">The opening curly bracket token.</param>
    /// <param name="elements">The list of BPMN elements within the Event Subprocess.</param>
    /// <param name="closeCurlyBracketToken">The closing curly bracket token.</param>
    public BpmnEventSubprocessSyntax(
        SyntaxToken eventSubprocessElement,
        SyntaxToken openParenthesisToken,
        IDictionary<string,BpmnAttributeSyntax> attributes,
        SyntaxToken closeParenthesisToken,
        SyntaxToken openCurlyBracketToken,
        List<BpmnElementSyntax> elements,
        SyntaxToken closeCurlyBracketToken)
        : base(eventSubprocessElement, openParenthesisToken, attributes, closeParenthesisToken)
    {
        EventSubprocessElement = eventSubprocessElement;
        OpenCurlyBracketToken = openCurlyBracketToken;
        Elements = elements;
        CloseCurlyBracketToken = closeCurlyBracketToken;
    }

    /// <summary>
    /// Gets the syntax kind of this node.
    /// </summary>
    public override SyntaxKind Kind => SyntaxKind.EventSubprocessElement;

    /// <summary>
    /// Gets the children of this syntax node.
    /// </summary>
    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return EventSubprocessElement;
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