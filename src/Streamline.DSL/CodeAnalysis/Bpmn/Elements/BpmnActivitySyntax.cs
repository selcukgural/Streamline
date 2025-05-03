namespace Streamline.DSL.CodeAnalysis.Bpmn.Elements;

public abstract class BpmnActivitySyntax(
    SyntaxToken element,
    SyntaxToken openParenthesisToken,
    IDictionary<string, BpmnAttributeSyntax> attributes,
    SyntaxToken closeParenthesisToken,
    SyntaxToken? openCurlyBracketToken = null,
    List<BpmnElementSyntax>? elements = null,
    SyntaxToken? closeCurlyBracketToken = null)
    : BpmnElementSyntax(element, openParenthesisToken, attributes, closeParenthesisToken, openCurlyBracketToken,
        elements, closeCurlyBracketToken);