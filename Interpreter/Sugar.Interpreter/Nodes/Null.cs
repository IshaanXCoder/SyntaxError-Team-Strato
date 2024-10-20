using Sugar.Interpreter.Caching;

using Sugar.Interpreter.Nodes.Enums;

using Sugar.Interpreter.Nodes.Interfaces;

namespace Sugar.Interpreter.Nodes
{
    internal sealed class Null : Node, IValueNode
    {
        public override NodeType Type { get => NodeType.Null; }

        public Null() 
        {
        
        }

        public int? Value(Cache cache) => null;

        public override string ToString() => $"Null Node";
        protected override void PrintChildren(string indent) { }
    }
}

