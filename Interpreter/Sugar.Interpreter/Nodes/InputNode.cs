using System;

using Sugar.Interpreter.Caching;

using Sugar.Interpreter.Exceptions;

using Sugar.Interpreter.Nodes.Enums;

using Sugar.Interpreter.Nodes.Interfaces;

namespace Sugar.Interpreter.Nodes
{
    internal sealed class InputNode : Node, IValueNode
    {
        public override NodeType Type { get => NodeType.Input; }

        private readonly IValueNode index;

        public InputNode(IValueNode _index)
        {
            index = _index;
        }

        public int? Value(Cache cache)
        {
            var result = index.Value(cache);
            if (result == null)
                return null;
            
            return cache.GetInputCache((int)result);
        }
        
        public override string ToString() => $"Input Node";
        protected override void PrintChildren(string indent)
        {
            index.Print(indent, true);
        }
    }
}

