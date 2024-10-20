using System;

using Sugar.Interpreter.Caching;

using Sugar.Interpreter.Nodes.Enums;

using Sugar.Interpreter.Nodes.Interfaces;

namespace Sugar.Interpreter.Nodes.Values
{
    internal sealed class ConstantNode : Node, IValueNode
    {
        public override NodeType Type { get => NodeType.ConstantNode; }
        
        private readonly int value;

        public ConstantNode(int _value)
        {
            value = _value;
        }

        public int? Value(Cache cache) => value;
        
        public override string ToString() => $"Constant Node: { value }";
        protected override void PrintChildren(string indent) { }
    }
}

