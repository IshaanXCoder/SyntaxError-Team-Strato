using System;

using Sugar.Interpreter.Caching;

using Sugar.Interpreter.Nodes.Enums;

using Sugar.Interpreter.Nodes.Interfaces;

namespace Sugar.Interpreter.Nodes.Operations
{
    internal sealed class CacheAssignmentNode : Node, IValueNode
    {
        public override NodeType Type { get => NodeType.OperatorNode; }
        
        private readonly OutputNode identifier;
        private readonly IValueNode value;

        public CacheAssignmentNode(OutputNode _identifier, IValueNode _value)
        {
            identifier = _identifier;
            value = _value;
        }
        
        public int? Value(Cache cache)
        {
            var result = value.Value(cache);
            var index = identifier.Index.Value(cache);
            if (index == null)
                return null;
            
            if(result == null)
                cache.SetOutputCache((int)index, 0);
            else
                cache.SetOutputCache((int)index, (int)result);

            return cache.GetOutputCache((int)index);
        }
        
        public override string ToString() => $"Cache Assignment Node";
        protected override void PrintChildren(string indent)
        {
            identifier.Print(indent, false);
            value.Print(indent, true);
        }
    }
}