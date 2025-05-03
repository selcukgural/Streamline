namespace Streamline.DSL.CodeAnalysis.Bpmn.Elements;

/// <summary>
/// Represents a BPMN Script Task element in the DSL syntax tree.
/// </summary>
public sealed class BpmnScriptTaskSyntax : BpmnElementSyntax
{
    /// <summary>
    /// Gets the token representing the 'ScriptTask' keyword.
    /// </summary>
    public SyntaxToken ScriptTaskElement { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BpmnScriptTaskSyntax"/> class.
    /// </summary>
    /// <param name="scriptTaskElement">The 'ScriptTask' keyword token.</param>
    /// <param name="openParenthesisToken">The opening parenthesis token.</param>
    /// <param name="attributes">The list of parameters for the Script Task.</param>
    /// <param name="closeParenthesisToken">The closing parenthesis token.</param>
    public BpmnScriptTaskSyntax(
        SyntaxToken scriptTaskElement,
        SyntaxToken openParenthesisToken,
        IDictionary<string,BpmnAttributeSyntax> attributes,
        SyntaxToken closeParenthesisToken)
        : base(scriptTaskElement, openParenthesisToken, attributes, closeParenthesisToken)
    {
        ScriptTaskElement = scriptTaskElement;
    }

    /// <summary>
    /// Gets the syntax kind of this node.
    /// </summary>
    public override SyntaxKind Kind => SyntaxKind.ScriptTaskElement;
}