using Sugar.Interpreter.Tokens.Enums;

namespace Sugar.Interpreter.Tokens
{
    internal sealed class Operator : Token
{
    public override TokenType Type { get => TokenType.Operator; }
  
    private readonly int precedence;
    public int Precedence { get => precedence; }
    
    private readonly bool leftAssociative;
    public bool LeftAssociative { get => leftAssociative; }
    
    public static readonly Operator Addition = new Operator("+", 2, true, SyntaxKind.Addition);
    public static readonly Operator Subtraction = new Operator("-", 2, true, SyntaxKind.Subtraction);
    
    public static readonly Operator Multiplication = new Operator("*", 1, true, SyntaxKind.Multiplication);
    public static readonly Operator Division = new Operator("/", 1, true, SyntaxKind.Division);
    public static readonly Operator Remainder = new Operator("%", 1, true, SyntaxKind.Remainder);
    
    public static readonly Operator GreaterThan = new Operator(">", 3, true, SyntaxKind.GreaterThan);
    public static readonly Operator GreaterThanEquals = new Operator(">=", 3, true, SyntaxKind.GreaterThanEquals);
    public static readonly Operator LesserThan = new Operator("<", 3, true, SyntaxKind.LesserThan);
    public static readonly Operator LessThanEquals = new Operator("<=", 3, true, SyntaxKind.LesserThanEquals);
    
    public static readonly Operator Or = new Operator("||", 6, true, SyntaxKind.Or);
    public static readonly Operator And = new Operator("&&", 7, true, SyntaxKind.And);
    
    public static readonly Operator Equal = new Operator("==", 4, true, SyntaxKind.Equals);
    public static readonly Operator NotEqual = new Operator("!=", 4, true, SyntaxKind.NotEquals);
    
    public static readonly Operator BitwiseOr = new Operator("|", 5, true, SyntaxKind.BitwiseOr);
    public static readonly Operator BitwiseXor = new Operator("&", 5, true, SyntaxKind.BitwiseXor);
    public static readonly Operator BitwiseAnd = new Operator("^", 5, true, SyntaxKind.BitwiseAnd);
    
    public static readonly Operator Assignment = new Operator("=", 8, false, Enums.SyntaxKind.Assignment);
    
    private Operator(string _value, int _precedence, bool _leftAssociative, SyntaxKind _kind) : base(_value, _kind)
    {
        precedence = _precedence;
        leftAssociative = _leftAssociative;
    }
    
    public Token Clone() => new Operator(value, precedence, leftAssociative, syntaxKind);
}
}

