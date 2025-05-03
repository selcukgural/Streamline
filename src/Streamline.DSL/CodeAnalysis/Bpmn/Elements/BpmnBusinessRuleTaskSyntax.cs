namespace Streamline.DSL.CodeAnalysis.Bpmn.Elements;

/// <summary>
/// Represents a BPMN Business Rule Task element in the DSL syntax tree.
/// </summary>
public sealed class BpmnBusinessRuleTaskSyntax : BpmnElementSyntax
{
    /// <summary>
    /// Gets the token representing the 'BusinessRuleTask' keyword.
    /// </summary>
    public SyntaxToken BusinessRuleTaskElement { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BpmnBusinessRuleTaskSyntax"/> class.
    /// </summary>
    /// <param name="businessRuleTaskElement">The 'BusinessRuleTask' keyword token.</param>
    /// <param name="openParenthesisToken">The opening parenthesis token.</param>
    /// <param name="attributes">The list of parameters for the Business Rule Task.</param>
    /// <param name="closeParenthesisToken">The closing parenthesis token.</param>
    public BpmnBusinessRuleTaskSyntax(
        SyntaxToken businessRuleTaskElement,
        SyntaxToken openParenthesisToken,
        IDictionary<string,BpmnAttributeSyntax> attributes,
        SyntaxToken closeParenthesisToken)
        : base(businessRuleTaskElement, openParenthesisToken, attributes, closeParenthesisToken)
    {
        BusinessRuleTaskElement = businessRuleTaskElement;
    }

    /// <summary>
    /// Gets the syntax kind of this node.
    /// </summary>
    public override SyntaxKind Kind => SyntaxKind.BusinessRuleTaskElement;
}