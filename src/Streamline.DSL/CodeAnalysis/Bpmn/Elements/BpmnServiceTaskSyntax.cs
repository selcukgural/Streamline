namespace Streamline.DSL.CodeAnalysis.Bpmn.Elements;

/// <summary>
/// Represents a BPMN Service Task element in the DSL syntax tree.
/// </summary>
public sealed class BpmnServiceTaskSyntax : BpmnElementSyntax
{
    /// <summary>
    /// Gets the token representing the 'ServiceTask' keyword.
    /// </summary>
    public SyntaxToken ServiceTaskElement { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BpmnServiceTaskSyntax"/> class.
    /// </summary>
    /// <param name="serviceTaskElement">The 'ServiceTask' keyword token.</param>
    /// <param name="openParenthesisToken">The opening parenthesis token.</param>
    /// <param name="attributes">The list of parameters for the Service Task.</param>
    /// <param name="closeParenthesisToken">The closing parenthesis token.</param>
    public BpmnServiceTaskSyntax(
        SyntaxToken serviceTaskElement,
        SyntaxToken openParenthesisToken,
        IDictionary<string,BpmnAttributeSyntax> attributes,
        SyntaxToken closeParenthesisToken)
        : base(serviceTaskElement, openParenthesisToken, attributes, closeParenthesisToken)
    {
        ServiceTaskElement = serviceTaskElement;
    }

    /// <summary>
    /// Gets the syntax kind of this node.
    /// </summary>
    public override SyntaxKind Kind => SyntaxKind.ServiceTaskElement;
}