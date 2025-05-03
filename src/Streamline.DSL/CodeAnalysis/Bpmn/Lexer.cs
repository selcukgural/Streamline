namespace Streamline.DSL.CodeAnalysis.Bpmn;

internal sealed class Lexer(string text)
{
    private int _position;
    
    private char Current => _position >= text.Length ? '\0' : text[_position];
    private readonly List<string> _diagnostics = [];

    private void Next()
    {
        _position++;
    }

    public SyntaxToken NextToken()
    {
        if (_position >= text.Length)
        {
            return new SyntaxToken(SyntaxKind.EndOfFileToken, _position, "\0");
        }

        if (char.IsWhiteSpace(Current))
        {
            var start = _position;
            while (char.IsWhiteSpace(Current))
            {
               Next();
            }
            
            var token = text.Substring(start, _position - start);
            return new SyntaxToken(SyntaxKind.WhitespaceToken, start, token);
        }

        if (char.IsLetter(Current))
        {
            var start = _position;
            while (char.IsLetterOrDigit(Current) || Current == '_')
            {
                Next();
            }   
            
            var token = text.Substring(start, _position - start);
            var kind = SyntaxFactory.GetKind(token);
            return new SyntaxToken(kind, start, token);
        }

        if (char.IsDigit(Current))
        {
            var start = _position;
            while (char.IsDigit(Current))
            {
                Next();
            }
            
            var str = text.Substring(start, _position - start);
            if(!int.TryParse(str, out _))
            {
                _diagnostics.Add($"The number {text} isn't valid integer.");
            }
            return new SyntaxToken(SyntaxKind.NumberToken, start, str, int.Parse(str));
        }
        
        switch (Current)
        {
            case '{':
                return new SyntaxToken(SyntaxKind.OpenBraceToken, _position++ ,"{");
            case '}':
                return new SyntaxToken(SyntaxKind.CloseBraceToken, _position++, "}");
            case '(':
                return new SyntaxToken(SyntaxKind.OpenParenthesisToken, _position++, "(");
            case ')':
                return new SyntaxToken(SyntaxKind.CloseParenthesisToken, _position++, ")");
            case '=':
                return new SyntaxToken(SyntaxKind.EqualsToken, _position++, "=");
            case ',':
                return new SyntaxToken(SyntaxKind.CommaToken, _position++, ",");
            case '+':
                return new SyntaxToken(SyntaxKind.PlusToken, _position++, "+");
            case '-':
                return new  SyntaxToken(SyntaxKind.MinusToken, _position++, "-");
            case '*':
                return new SyntaxToken(SyntaxKind.MultiplyToken, _position++, "*");
            case '/':
                return new SyntaxToken(SyntaxKind.DivisionToken, _position++, "/");
            case '"':
                Next();
                var start = _position;
                while (Current != '"' && Current != '\0')
                {
                    Next();
                }
                var length = _position - start;
                var str = text.Substring(start, length);
    
                if (Current == '"')
                {
                    Next();
                }
    
                return new SyntaxToken(SyntaxKind.StringToken, start - 1, str, str);
            
            default:
                _diagnostics.Add($"ERROR: Invalid character input: '{Current}'");
                return new SyntaxToken(SyntaxKind.BadToken, _position++, text.Substring(_position - 1, 1));
                
        }
    }
}