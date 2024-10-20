using System;

using Sugar.Interpreter.Tokens;

using Sugar.Interpreter.Caching;

using Sugar.Interpreter.Nodes.Enums;

using Sugar.Interpreter.Nodes.Interfaces;
using Sugar.Interpreter.Nodes.Values;

namespace Sugar.Interpreter.Nodes.Operations
{
    internal sealed class AssignmentNode : Node, IValueNode
    {
        public override NodeType Type { get => NodeType.OperatorNode; }
        
        private readonly IdentifierNode identifier;
        private readonly IValueNode value;

        public AssignmentNode(IdentifierNode _identifier, IValueNode _value)
        {
            identifier = _identifier;
            value = _value;
        }
        
        public int? Value(Cache cache)
        {
            cache.SetVariable(identifier.Name, value.Value(cache));
            return cache.GetVariable(identifier.Name);
        }
        
        public override string ToString() => $"Assignment Node";
        protected override void PrintChildren(string indent)
        {
            identifier.Print(indent, false);
            value.Print(indent, true);
        }
    }
}

