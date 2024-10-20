using Sugar.Interpreter.Tokens.Enums;

namespace Sugar.Interpreter.Tokens
{
    internal sealed class Identifier : Token
    {
        public override TokenType Type { get => TokenType.Identifier; }
    
        public Identifier(string _value) : base(_value, SyntaxKind.Identifier)
        {}
    }
}

