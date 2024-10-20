using Sugar.Interpreter.Tokens.Enums;

namespace Sugar.Interpreter.Tokens
{
    internal sealed class Seperator : Token
    {
        private readonly SeperatorKind seperatorKind;
        public SeperatorKind SeperatorKind { get => seperatorKind; } 
    
        public override TokenType Type { get => TokenType.Separator; }
    
        public static readonly Seperator Lamda = new Seperator("=>", SeperatorKind.Lambda);
        
        public static readonly Seperator Comma = new Seperator(",", SeperatorKind.Comma);
    
        public static readonly Seperator OpenBracket = new Seperator("(", SeperatorKind.OpenBracket);
        public static readonly Seperator CloseBracket = new Seperator(")", SeperatorKind.CloseBracket);
    
        public static readonly Seperator BoxOpenBracket = new Seperator("[", SeperatorKind.BoxOpenBracket);
        public static readonly Seperator BoxCloseBracket = new Seperator("]", SeperatorKind.BoxCloseBracket);
    
        public static readonly Seperator FlowerOpenBracket = new Seperator("{", SeperatorKind.FlowerOpenBracket);
        public static readonly Seperator FlowerCloseBracket = new Seperator("}", SeperatorKind.FlowerCloseBracket);

        private Seperator(string _value, SeperatorKind _kind) : base(_value, (SyntaxKind)_kind)
        {
            seperatorKind = _kind;
        }
    
        public Token Clone() => new Seperator(value, seperatorKind);
    }
}

