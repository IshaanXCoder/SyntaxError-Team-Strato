using System;

using Sugar.Interpreter.Tokens.Enums;

namespace Sugar.Interpreter.Tokens
{
    internal sealed class NewLine : Token
    {
        public override TokenType Type { get => TokenType.NewLine; }

        public static readonly NewLine New = new NewLine();

        private NewLine() : base("\n", SyntaxKind.NewLine)
        { }

        public Token Clone() => new NewLine();
    }
}

