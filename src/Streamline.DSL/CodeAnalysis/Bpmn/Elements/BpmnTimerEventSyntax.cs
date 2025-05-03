namespace Streamline.DSL.CodeAnalysis.Bpmn.Elements;

/// <summary>
/// Represents a BPMN Timer Event element in the DSL syntax tree.
/// </summary>
public sealed class BpmnTimerEventSyntax : BpmnElementSyntax
{
    /// <summary>
    /// Gets the token representing the 'TimerEvent' keyword.
    /// </summary>
    public SyntaxToken TimerEventElement { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BpmnTimerEventSyntax"/> class.
    /// </summary>
    /// <param name="timerEventElement">The 'TimerEvent' keyword token.</param>
    /// <param name="openParenthesisToken">The opening parenthesis token.</param>
    /// <param name="attributes">The list of parameters for the Timer Event.</param>
    /// <param name="closeParenthesisToken">The closing parenthesis token.</param>
    public BpmnTimerEventSyntax(
        SyntaxToken timerEventElement,
        SyntaxToken openParenthesisToken,
        IDictionary<string,BpmnAttributeSyntax> attributes,
        SyntaxToken closeParenthesisToken)
        : base(timerEventElement, openParenthesisToken, attributes, closeParenthesisToken)
    {
        TimerEventElement = timerEventElement;
    }

    /// <summary>
    /// Gets the syntax kind of this node.
    /// </summary>
    public override SyntaxKind Kind => SyntaxKind.TimerEventElement;
}