using System.Text;
using Sugar.Interpreter.Exceptions;
using Sugar.Interpreter.Tokens;
using Sugar.Interpreter.Tokens.Constants;

namespace Sugar.Interpreter.Lexing
{
    internal sealed class Lexer
    {
        private static Lexer instance;
        public static Lexer Instance { get => instance; }
        
        public static void Initialise()
        {
            instance = new Lexer();
        }
        
        private int index = 0;
        private string source;
        
        private char LookAhead { get => index == source.Length ? source[source.Length - 1] : source[index + 1]; }

        public Lexer()
        {
            index = 0;
            source = "";
        }
        
        public List<Token> Lex(string input)
        {
            index = 0;
            source = input;
            
            var tokens = new List<Token>();
            var s = new StringBuilder();
          
            for(index = 0; index < source.Length; index++)
            {
                switch(source[index])
                {
                    case ' ':
                        break;
                    case '+':
                        tokens.Add(Operator.Addition.Clone());
                        break;
                    case '-':
                        tokens.Add(Operator.Subtraction.Clone());
                        break;
                    case '*':
                        tokens.Add(Operator.Multiplication.Clone());
                        break;
                    case '/':
                        tokens.Add(Operator.Division.Clone());
                        break;
                    case '%':
                        tokens.Add(Operator.Remainder.Clone());
                        break;
                    case '=':
                        if(LookAhead == '>')
                        {
                            index++;
                            tokens.Add(Seperator.Lamda.Clone());
                        }
                        else if (LookAhead == '=')
                        {
                            index++;
                            tokens.Add(Operator.Equal.Clone());
                        }
                        else
                            tokens.Add(Operator.Assignment.Clone());
                        break;
                    case '>':
                        if (LookAhead == '=')
                        {
                            index++;
                            tokens.Add(Operator.GreaterThanEquals.Clone());
                        }
                        else
                            tokens.Add(Operator.GreaterThan.Clone());
                        break;
                    case '<':
                        if (LookAhead == '=')
                        {
                            index++;
                            tokens.Add(Operator.LessThanEquals.Clone());
                        }
                        else
                            tokens.Add(Operator.LesserThan.Clone());
                        break;
                    case '|':
                        if (LookAhead == '|')
                        {
                            index++;
                            tokens.Add(Operator.Or.Clone());
                        }
                        else
                            tokens.Add(Operator.BitwiseOr.Clone());
                        break;
                    case '&':
                        if (LookAhead == '&')
                        {
                            index++;
                            tokens.Add(Operator.And.Clone());
                        }
                        else
                            tokens.Add(Operator.BitwiseAnd.Clone());
                        break;
                    case '^':
                        tokens.Add(Operator.BitwiseXor.Clone());
                        break;
                    case '!':
                        if (LookAhead == '=')
                        {
                            index++;
                            tokens.Add(Operator.NotEqual.Clone());
                        }
                        else
                            throw new CompileException($"invalid character {source[index]}");
                        break;
                    case '(':
                        tokens.Add(Seperator.OpenBracket.Clone());
                        break;
                    case ')':
                        tokens.Add(Seperator.CloseBracket.Clone());
                        break;
                    case '[':
                        tokens.Add(Seperator.BoxOpenBracket.Clone());
                        break;
                    case ']':
                       
                        tokens.Add(Seperator.BoxCloseBracket.Clone());
                        break;
                    case '{':
                        tokens.Add(Seperator.FlowerOpenBracket.Clone());
                        break;
                    case '}':
                        tokens.Add(Seperator.FlowerCloseBracket.Clone());
                        break;
                    case ',':
                        tokens.Add(Seperator.Comma.Clone());
                        break; 
                   case '1':
                   case '2':
                   case '3':
                   case '4':
                   case '5':
                   case '6':
                   case '7':
                   case '8':
                   case '9':
                   case '0':
                   case '.':
                        tokens.Add(ReadNumber());
                        break;
                  default:
                        if (source[index].ToString() == Environment.NewLine)
                            tokens.Add(NewLine.New.Clone());
                        else
                            tokens.Add(ReadEntity());
                        break;
                }
            }

            return tokens;
        }

        private Token ReadNumber()
        {
            StringBuilder s = new StringBuilder();
            
            while(index < source.Length)
            {
                if (char.IsNumber(source[index]))
                    s.Append(source[index]);
                else
                {
                    index--;
                    break;
                }

                index++;
            }

            return new Constant(s.ToString());
        }
                
        private Token ReadEntity()
        {
            StringBuilder s = new StringBuilder();

            while (index < source.Length)
            { 
                var current = source[index];

                if (char.IsWhiteSpace(current))
                    break;

                switch(current)
                {
                    case '=':
                    case '+':
                    case '-':
                    case '*':
                    case '/':
                    case '(':
                    case ')':
                    case '[':
                    case ']':
                    case '{':
                    case '}':
                    case '<':
                    case '>':
                    case '&':
                    case '|':
                    case '!':
                    case '^':
                    case '~':
                    case '?':
                    case '%':
                        index--;
                        return Extract();
                    default:
                        s.Append(current);
                        break;
                }

                index++;
            }

            return Extract();

            Token Extract()
            {
                var value = s.ToString();

                if (value == Keyword.If.Value)
                    return Keyword.If.Clone();
                if (value == Keyword.Loop.Value)
                    return Keyword.Loop.Clone();
                if (value == Keyword.Input.Value)
                    return Keyword.Input.Clone();
                if (value == Keyword.Output.Value)
                    return Keyword.Output.Clone();
                if (value == Keyword.Function.Value)
                    return Keyword.Function.Clone();

                return new Identifier(value);
            }
        }
    }
}

