namespace Sugar.Interpreter.Tokens.Enums
{
    [Flags]
    internal enum SeperatorKind
    {
        Lambda = 1,
    
        OpenBracket = 2,
        CloseBracket = 4,
    
        BoxOpenBracket = 8,
        BoxCloseBracket = 16,
    
        FlowerOpenBracket = 32,
        FlowerCloseBracket = 64,
    
        Comma = 128,
        
        None = 256
    }
}

