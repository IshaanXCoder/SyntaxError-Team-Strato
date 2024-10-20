using System;

using Sugar.Interpreter.Exceptions;

using Sugar.Interpreter.Tokens;
using Sugar.Interpreter.Tokens.Enums;
using Sugar.Interpreter.Tokens.Constants;

using Sugar.Interpreter.Nodes;

using Sugar.Interpreter.Nodes.Enums;

using Sugar.Interpreter.Nodes.Values;

using Sugar.Interpreter.Nodes.Functions;

using Sugar.Interpreter.Nodes.Interfaces;

using Sugar.Interpreter.Nodes.Conditionals;

using Sugar.Interpreter.Nodes.Operations;
using Sugar.Interpreter.Nodes.Operations.Bitwise;
using Sugar.Interpreter.Nodes.Operations.Logical;
using Sugar.Interpreter.Nodes.Operations.Arithmetic;
using Sugar.Interpreter.Nodes.Operations.Relational;

namespace Sugar.Interpreter.Parsing
{
    internal sealed class Parser
    {
        public Token Current { get => index >= tokens.Count - 1 ? tokens[tokens.Count - 1] : tokens[index]; }
        public Token LookAhead { get => index >= tokens.Count - 1 ? tokens[tokens.Count - 1] : tokens[index + 1]; }
        
        private int index;
        private List<Token> tokens;
        
        public Parser(List<Token> inTokens)
        {
            tokens = inTokens;
            index = 0;
        }
        
        private bool ForceMatchTokenType(Token token, TokenType match, bool increment = false)
        {
            if (!MatchTokenType(token, match, increment))
                throw new CompileException($"invalid {token.Value} detected");
            
            return true;
        }

        private bool MatchTokenType(Token token, TokenType match, bool increment = false)
        {
            var result = (token.Type & match) == token.Type;
            if (result && increment)
                index++;

            return result;
        }

        private bool ForceMatchToken(Token token, Token match, bool increment = false)
        {
            if (!MatchToken(token, match, increment))
                throw new CompileException($"invalid token detected {match.Value}");

            return true;
        }

        private bool MatchToken(Token token, Token match, bool increment = false)
        {
           var result = token.SyntaxKind == match.SyntaxKind;
           if (result && increment)
               index++;

           return result;
        } 
        
        public Node Parse(SeperatorKind breakOutSeperators = SeperatorKind.None)
        {
            var collection = new NodeCollection();
            
            for (; index < tokens.Count; index++)
            {
                var current = Current;

                bool breakOut = false;
                switch (current.Type)
                {
                    case TokenType.NewLine:
                        break;
                    case TokenType.Keyword:
                        collection.Add(ParseKeyword());
                        break;
                    case TokenType.Identifier:
                        collection.Add(ParseExpression());
                        break;
                    case TokenType.Separator:
                        var seperatorType = (SeperatorKind)current.SyntaxKind;
                        if ((seperatorType & breakOutSeperators) == seperatorType)
                            breakOut = true;
                        break;
                    default:
                        throw new CompileException($"invalid character {current.Value} detected");
                }
                
                if(breakOut)
                    break;
            }
            
            return collection;
        }

        private INode ParseScope()
        {
            ForceMatchToken(Current, Seperator.FlowerOpenBracket, true);
            
            var collection = new NodeCollection();
            for (; index < tokens.Count; index++)
            {
                var node = ParseStatement(SeperatorKind.FlowerCloseBracket);
                if(MatchToken(Current, Seperator.FlowerCloseBracket))
                    break;
                
                collection.Add(node);
            }

            return collection;
        }
        
        private INode ParseStatement(SeperatorKind breakOutSeperators = SeperatorKind.None)
        {
            var current = Current;
            
            switch (current.Type)
            {
                case TokenType.NewLine:
                    index++;
                    return ParseStatement(breakOutSeperators);
                case TokenType.Keyword:
                    return ParseKeyword();
                case TokenType.Identifier:
                    return ParseExpression(breakOutSeperators);
                case TokenType.Separator:
                    var seperatorType = (SeperatorKind)current.SyntaxKind;
                    if ((seperatorType & breakOutSeperators) == seperatorType)
                        return null;
                    break;
            }
            
            throw new CompileException($"invalid character {current.Value} detected");
        }

        private INode ParseKeyword()
        {
            var current = Current;
            
            switch (current.SyntaxKind)
            {
                case SyntaxKind.If:
                    return ParseIf();
                case SyntaxKind.While:
                    return ParseWhile();
                case SyntaxKind.Input:
                case SyntaxKind.Output:
                    return ParseExpression();
                default://function
                    return ParseFunction();
            }
        }

        private IValueNode ParseExpression(SeperatorKind breakOutSeperators = SeperatorKind.None)
        {
            var output = new Stack<IValueNode>();
            var stack = new Stack<Token>();

            bool breakOut = false;
            TokenType expected = TokenType.Identifier | TokenType.Keyword | TokenType.Constant; 
            for (; index < tokens.Count; index++)
            { 
                var current = Current;
                ForceMatchTokenType(current, expected);
                
                switch (current.Type)
                {
                    case TokenType.NewLine:
                        breakOut = true;
                        break;
                    case TokenType.Keyword:
                        switch (current.SyntaxKind)
                        {
                            case SyntaxKind.Input:
                                index += 2;
                                output.Push(ParseInputNode());
                                break;
                            case SyntaxKind.Output:
                                index += 2;
                                output.Push(ParseOutputNode());
                                break;
                            default:
                                throw new CompileException($"invalid keyword {current.Value} detected");
                        }

                        expected = TokenType.Operator | TokenType.NewLine;
                        if(!MatchToken(LookAhead, Seperator.OpenBracket))
                            expected |= TokenType.Separator;
                        break;
                    case TokenType.Constant:
                        output.Push(new ConstantNode(int.Parse(((Constant)current).Value)));
                        expected = TokenType.Operator | TokenType.NewLine;
                        
                        if(!MatchToken(LookAhead, Seperator.OpenBracket))
                            expected |= TokenType.Separator;
                        break;
                    case TokenType.Identifier:
                        var identifier = new IdentifierNode((Identifier)current);
                        if (MatchToken(LookAhead, Seperator.OpenBracket))
                            output.Push(ParseFunctionCall(identifier));
                        else
                            output.Push(identifier);
                        
                        expected = TokenType.Operator | TokenType.NewLine;
                        if (!MatchToken(LookAhead, Seperator.OpenBracket))
                            expected |= TokenType.Separator;
                        break;
                    case TokenType.Operator:
                        if(stack.Count > 0 && stack.Peek().Type == TokenType.Operator)
                        {
                            var cur = (Operator)current;
                            var top = (Operator)stack.Pop();

                            if (cur.Precedence < top.Precedence || (top.Precedence == cur.Precedence && !cur.LeftAssociative))
                                stack.Push(top);
                            else
                                output.Push(ParseExpression(top, output));
                        }
                      
                        stack.Push(current);
                        expected = TokenType.Keyword | TokenType.Identifier | TokenType.Constant;
                        if (MatchToken(LookAhead, Seperator.OpenBracket))
                            expected |= TokenType.Separator;
                        break;
                    case TokenType.Separator:
                        var seperatorType = (SeperatorKind)current.SyntaxKind;
                        if ((seperatorType & breakOutSeperators) == seperatorType)
                        {
                            breakOut = true;
                            break;
                        }

                        if (MatchToken(current, Seperator.OpenBracket, true))
                        {
                            output.Push(ParseExpression(SeperatorKind.CloseBracket));
                            expected = TokenType.Operator | TokenType.NewLine;
                            
                            if(!MatchToken(LookAhead, Seperator.OpenBracket))
                                expected |= TokenType.Separator;
                        }
                        else
                            throw new CompileException($"invalid keyword {current.Value} detected");
                        break;
                }

                if (breakOut)
                    break;
            }
            
            while(stack.Count > 0)
                output.Push(ParseExpression(stack.Pop(), output));

            if(output.Count == 1)
                return output.Pop();
            
            return new Null();
        }

        private IValueNode ParseExpression(Token top, Stack<IValueNode> output)
        { 
            var rhs = output.Pop();
            var lhs = output.Pop();
            
            switch(top.SyntaxKind)
            {
                case SyntaxKind.Addition:
                    return new AdditionNode(lhs, rhs);
                case SyntaxKind.Subtraction:
                    return new SubtractionNode(lhs, rhs);
                case SyntaxKind.Multiplication:
                    return new MultiplicationNode(lhs, rhs);
                case SyntaxKind.Division:
                    return new DivisionNode(lhs, rhs);
                case SyntaxKind.Remainder:
                    return new RemainderNode(lhs, rhs);
                case SyntaxKind.GreaterThan:
                    return new GreaterThanNode(lhs, rhs);
                case SyntaxKind.LesserThan:
                    return new LesserThanNode(lhs, rhs);
                case SyntaxKind.GreaterThanEquals:
                    return new GreaterThanEqualsNode(lhs, rhs);
                case SyntaxKind.LesserThanEquals:
                    return new LesserThanEqualsNode(lhs, rhs);
                case SyntaxKind.Or:
                    return new OrNode(lhs, rhs);
                case SyntaxKind.And:
                    return new AndNode(lhs, rhs);
                case SyntaxKind.Equals:
                    return new EqualsNode(lhs, rhs);
                case SyntaxKind.NotEquals:
                    return new NotEqualsNode(lhs, rhs);
                case SyntaxKind.BitwiseOr:
                    return new BitwiseOrNode(lhs, rhs);
                case SyntaxKind.BitwiseXor:
                    return new BitwiseXorNode(lhs, rhs);
                case SyntaxKind.BitwiseAnd:
                    return new BitwiseAndNode(lhs, rhs);
                case SyntaxKind.Assignment:
                    if(lhs.Type == NodeType.IdentifierNode)
                        return new AssignmentNode((IdentifierNode)lhs, rhs);
                    if (lhs.Type == NodeType.Output)
                        return new CacheAssignmentNode((OutputNode)lhs, rhs);
                    break;    
            }

            return new Null();
        }
        
        private IValueNode ParseInputNode()
        {
            var input = new InputNode(ParseExpression(SeperatorKind.BoxCloseBracket));
            ForceMatchToken(Current, Seperator.BoxCloseBracket);

            return input;
        } 

        private IValueNode ParseOutputNode()
        { 
            var output = new OutputNode(ParseExpression(SeperatorKind.BoxCloseBracket));
            ForceMatchToken(Current, Seperator.BoxCloseBracket);

            return output;
        }
        
        private FunctionNode ParseFunction()
        {
            ForceMatchToken(Current, Keyword.Function, true);
            
            var name = ((Identifier)Current).Value;
            index++;
            
            var parameters = new List<string>();
            while (index < tokens.Count)
            {
                if(MatchToken(Current, Seperator.Lamda))
                    break;
                
                ForceMatchTokenType(Current, TokenType.Identifier);
                parameters.Add(((Identifier)Current).Value);

                index++;
            }
            
            ForceMatchToken(Current, Seperator.Lamda, true);
            return new FunctionNode(name, parameters, ParseScope());
        }

        private FunctionCallNode ParseFunctionCall(IdentifierNode name)
        {
            index += 2;
            
            var function = new FunctionCallNode(name.Name);
            
            while (index < tokens.Count)
            {
                function.AddArgument(ParseExpression(SeperatorKind.Comma | SeperatorKind.CloseBracket));
                if(MatchToken(Current, Seperator.Comma, true))
                    continue;
                
                break;
            }
            
            ForceMatchToken(Current, Seperator.CloseBracket);
            return function;
        }

        private IfNode ParseIf()
        {
            ForceMatchToken(Current, Keyword.If, true);
            var expression = ParseExpression(SeperatorKind.Lambda);
            
            ForceMatchToken(Current, Seperator.Lamda, true);
            return new IfNode(expression, ParseScope());
        }
        
        private WhileLoopNode ParseWhile()
        {
            ForceMatchToken(Current, Keyword.Loop, true);
            var expression = ParseExpression(SeperatorKind.Lambda);
            
            ForceMatchToken(Current, Seperator.Lamda, true);
            return new WhileLoopNode(expression, ParseScope());
        }
    }
}
