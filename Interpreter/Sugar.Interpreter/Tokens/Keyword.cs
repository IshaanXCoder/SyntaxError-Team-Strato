using Sugar.Interpreter.Tokens.Enums;

namespace Sugar.Interpreter.Tokens
{
    internal sealed class Keyword : Token
    {
        public override TokenType Type { get => TokenType.Keyword; }
    
        public static Keyword If = new Keyword("if", Enums.SyntaxKind.If);
        public static Keyword Loop = new Keyword("loop", Enums.SyntaxKind.While);
        public static Keyword Function = new Keyword("func", Enums.SyntaxKind.Function);
    
        public static Keyword Input = new Keyword("in", Enums.SyntaxKind.Input);
        public static Keyword Output = new Keyword("out", Enums.SyntaxKind.Output);
    
        private Keyword(string _value, SyntaxKind _kind) : base(_value, _kind)
        {}
    
        public Token Clone() => new Keyword(value, syntaxKind);
    }
}

