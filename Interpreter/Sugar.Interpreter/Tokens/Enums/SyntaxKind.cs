namespace Sugar.Interpreter.Tokens.Enums
{
    internal enum SyntaxKind
    {
        Lambda = SeperatorKind.Lambda,
    
        OpenBracke = SeperatorKind.OpenBracket,
        CloseBracket = SeperatorKind.CloseBracket,
    
        BoxOpenBracket = SeperatorKind.BoxOpenBracket,
        BoxCloseBracket = SeperatorKind.BoxCloseBracket,
    
        FlowerOpenBracket = SeperatorKind.FlowerOpenBracket,
        FlowerCloseBracket = SeperatorKind.FlowerCloseBracket,
    
        Comma = SeperatorKind.Comma,
        
        Addition,
        Subtraction,
        Multiplication,
        Division,
        Remainder,
    
        GreaterThan,
        GreaterThanEquals,
        LesserThan,
        LesserThanEquals,
    
        Or,
        And,
    
        Equals,
        NotEquals,
    
        BitwiseOr,
        BitwiseXor,
        BitwiseAnd,
    
        Assignment,
    
        If,
        While,
        Function,
    
        Constant,
        Identifier,
    
        Input,
        Output,
    
        NewLine
    }
}

