namespace Streamline.DSL.CodeAnalysis.Bpmn.Elements;

/// <summary>
/// Represents a BPMN User Task element in the DSL syntax tree.
/// </summary>
public sealed class BpmnUserTaskSyntax : BpmnElementSyntax
{
    /// <summary>
    /// Gets the token representing the 'UserTask' keyword.
    /// </summary>
    public SyntaxToken UserTaskElement { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BpmnUserTaskSyntax"/> class.
    /// </summary>
    /// <param name="userTaskElement">The 'UserTask' keyword token.</param>
    /// <param name="openParenthesisToken">The opening parenthesis token.</param>
    /// <param name="attributes">The list of parameters for the User Task.</param>
    /// <param name="closeParenthesisToken">The closing parenthesis token.</param>
    public BpmnUserTaskSyntax(
        SyntaxToken userTaskElement,
        SyntaxToken openParenthesisToken,
        IDictionary<string,BpmnAttributeSyntax> attributes,
        SyntaxToken closeParenthesisToken)
        : base(userTaskElement, openParenthesisToken, attributes, closeParenthesisToken)
    {
        UserTaskElement = userTaskElement;
    }

    /// <summary>
    /// Gets the syntax kind of this node.
    /// </summary>
    public override SyntaxKind Kind => SyntaxKind.UserTaskElement;
}