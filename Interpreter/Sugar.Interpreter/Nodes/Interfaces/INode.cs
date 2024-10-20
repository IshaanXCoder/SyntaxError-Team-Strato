using Sugar.Interpreter.Nodes.Enums;

namespace Sugar.Interpreter.Nodes.Interfaces
{
    internal interface INode
    {
        public NodeType Type { get; }

        public void Print(string indent, bool last);
    }
}

