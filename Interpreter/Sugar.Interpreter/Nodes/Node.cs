using System;

using Sugar.Interpreter.Nodes.Enums;

using Sugar.Interpreter.Nodes.Interfaces;

namespace Sugar.Interpreter.Nodes
{
    internal abstract class Node : INode
    {
        public abstract NodeType Type { get; }
        
        public void Print(string indent, bool last)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("\\-");
                indent += "  ";
            }
            else
            {
                Console.Write("|-");
                indent += "| ";
            }

            Console.WriteLine(ToString());
            PrintChildren(indent);
        }

        protected abstract void PrintChildren(string indent);
    }
}

