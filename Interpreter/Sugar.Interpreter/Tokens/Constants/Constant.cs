using Sugar.Interpreter.Tokens.Enums;

namespace Sugar.Interpreter.Tokens.Constants
{
    internal sealed class Constant : Token
    {
        public override TokenType Type { get => TokenType.Constant; }
        public Constant(string _value) : base(_value, Enums.SyntaxKind.Constant)
        {}
    }
}

