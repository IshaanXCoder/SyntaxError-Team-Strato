using System;

using Sugar.Interpreter.Caching;

using Sugar.Interpreter.Nodes.Enums;

using Sugar.Interpreter.Nodes.Interfaces;

namespace Sugar.Interpreter.Nodes.Operations
{
    internal abstract class OperatorNode : Node, IValueNode
    {
        public override NodeType Type { get => NodeType.OperatorNode; }
        
        protected readonly IValueNode a;
        protected readonly IValueNode b;

        protected OperatorNode(IValueNode _a, IValueNode _b)
        {
            a = _a;
            b = _b;
        }
    
        public abstract int? Value(Cache cache);
        
        protected override void PrintChildren(string indent)
        {
            a.Print(indent, false);
            b.Print(indent, true);
        }
    }
}

