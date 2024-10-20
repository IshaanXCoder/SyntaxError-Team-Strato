using Sugar.Interpreter.Lexing;

using Sugar.Interpreter.Parsing;

using Sugar.Interpreter.Caching;

using Sugar.Interpreter.Nodes;
using Sugar.Interpreter.Nodes.Enums;
using Sugar.Interpreter.Nodes.Interfaces;

namespace Sugar.Interpreter
{
    public sealed class Interpreter
    {
        private static Interpreter instance;
        public static Interpreter Instance { get => instance; }
    
        public static void Initialise()
        {
            instance = new Interpreter();
        }

        private Interpreter()
        {
            Lexer.Initialise();
            GlobalCache.Initalise();
        }

        public int[] Interpret(int[] input, int outputCount, string source)
        {
            GlobalCache.Instance.InitialiseCache(input, outputCount);
            var tokens = Lexer.Instance.Lex(source);
            
            Node node = new Parser(tokens).Parse();

            if ((node.Type & NodeType.ValueNode) == node.Type)
                ((IValueNode)node).Value(GlobalCache.Instance);
            else if((node.Type & NodeType.ExecutableNode) == node.Type)
                ((IExecutableNode)node).Execute(GlobalCache.Instance);

            int[] output = new int[outputCount];
            for (int i = 0; i < outputCount; i++)
                output[i] = GlobalCache.Instance.GetOutputCache(i);
            
            return output;
        }
    }
}

