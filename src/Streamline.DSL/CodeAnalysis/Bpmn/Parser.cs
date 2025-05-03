using Streamline.DSL.CodeAnalysis.Bpmn.Elements;
using Streamline.DSL.CodeAnalysis.Exception;

namespace Streamline.DSL.CodeAnalysis.Bpmn;

public sealed class Parser
{
    private readonly List<SyntaxToken> _tokens;
    private int _position;
    private SyntaxToken Current => _position < _tokens.Count ? _tokens[_position] : _tokens[^1];

    public Parser(string text)
    {
        var lexer = new Lexer(text);
        var tokens = new List<SyntaxToken>();
        SyntaxToken token;
        do
        {
            token = lexer.NextToken();
            if (token.Kind != SyntaxKind.WhitespaceToken && token.Kind != SyntaxKind.BadToken)
            {
                tokens.Add(token);
            }
        } while (token.Kind != SyntaxKind.EndOfFileToken);

        _tokens = tokens;
    }

    public BpmnProcessSyntax Parse()
    {
        if (Current.Kind != SyntaxKind.BpmnProcessElement)
        {
            throw new BpmnProcessException(Current.Kind);
        }

        var processKeyword = NextToken();
        var openParenthesis = MatchToken(SyntaxKind.OpenParenthesisToken);
        var parameters = ParseAttributes();
        var closeParenthesis = MatchToken(SyntaxKind.CloseParenthesisToken);
        var openBrace = MatchToken(SyntaxKind.OpenBraceToken);
        var elements = ParseElements();
        var closeBrace = MatchToken(SyntaxKind.CloseBraceToken);

        return new BpmnProcessSyntax(
            processKeyword,
            openParenthesis,
            parameters,
            closeParenthesis,
            openBrace,
            elements,
            closeBrace);
    }
    private SyntaxToken NextToken()
    {
        var current = Current;
        _position++;
        return current;
    }
    private List<BpmnElementSyntax> ParseElements()
    {
        var elements = new List<BpmnElementSyntax>();
        while (Current.Kind != SyntaxKind.CloseBraceToken && Current.Kind != SyntaxKind.EndOfFileToken)
        {
            elements.Add(ParseElement());
        }
    
        return elements;
    }
    
    private BpmnElementSyntax ParseElement()
    {
        if (Current.Kind == SyntaxKind.TaskElement)
        {
            var taskToken = NextToken();
            var openParen = MatchToken(SyntaxKind.OpenParenthesisToken);
            var parameters = ParseAttributes();
            var closeParen = MatchToken(SyntaxKind.CloseParenthesisToken);
            return new BpmnTaskSyntax(taskToken, openParen, parameters, closeParen);
        }

        if (Current.Kind == SyntaxKind.StartEventElement)
        {
            var startToken = NextToken();
            var openParen = MatchToken(SyntaxKind.OpenParenthesisToken);
            var parameters = ParseAttributes();
            var closeParen = MatchToken(SyntaxKind.CloseParenthesisToken);
            if (Current.Kind != SyntaxKind.OpenBraceToken)
            {
                return new BpmnStartEventSyntax(startToken, openParen, parameters, closeParen);
            }
            
            var openBrace = NextToken();
            var elements = ParseElements();
            var closeBrace = MatchToken(SyntaxKind.CloseBraceToken);
            return new BpmnStartEventSyntax(startToken, openParen, parameters, closeParen, openBrace, elements, closeBrace);
        }
        
        if (Current.Kind == SyntaxKind.EndEventElement)
        {
            var endToken = NextToken();
            var openParen = MatchToken(SyntaxKind.OpenParenthesisToken);
            var parameters = ParseAttributes();
            var closeParen = MatchToken(SyntaxKind.CloseParenthesisToken);
            return new BpmnEndEventSyntax(endToken, openParen, parameters, closeParen);
        }
        
        throw new BpmnProcessException(Current.Kind);
    }

    private Dictionary<string, BpmnAttributeSyntax> ParseAttributes()
    {
        var attributes = new Dictionary<string, BpmnAttributeSyntax>(StringComparer.OrdinalIgnoreCase);

        while (Current.Kind != SyntaxKind.CloseParenthesisToken && Current.Kind != SyntaxKind.EndOfFileToken)
        {
            var identifier = MatchToken(SyntaxKind.BpmnAttributeToken);
            var equal = MatchToken(SyntaxKind.EqualsToken);
            var value = MatchToken(SyntaxKind.StringToken);
            
            if (attributes.ContainsKey(identifier.Text))
            {
                throw new AttributeAlreadyExistsException(identifier.Text);
            }
            
            attributes.Add(identifier.Text, new BpmnAttributeSyntax(identifier, equal, value));
            if (Current.Kind == SyntaxKind.CommaToken)
            {
                NextToken();
            }
        }

        return attributes;
    }

    private SyntaxToken MatchToken(SyntaxKind kind)
    {
        if (Current.Kind == kind)
        {
            return NextToken();
        }

        throw new System.Exception($"Expected {kind}, but found {Current.Kind}");
    }
}