using System;

using Sugar.Interpreter.Caching;

using Sugar.Interpreter.Exceptions;

using Sugar.Interpreter.Nodes.Enums;

using Sugar.Interpreter.Nodes.Interfaces;
namespace Sugar.Interpreter.Nodes
{
    internal sealed class OutputNode : Node, IValueNode
    {
        public override NodeType Type { get => NodeType.Output; }

        private readonly IValueNode index;
        public IValueNode Index { get => index; }

        public OutputNode(IValueNode _index)
        {
            index = _index;
        }

        public int? Value(Cache cache)
        {
            var result = index.Value(cache);
            if (result == null)
                return null;
            
            return cache.GetOutputCache((int)result);
        }
        
        public override string ToString() => $"Output Node";
        protected override void PrintChildren(string indent)
        {
            index.Print(indent, true);
        }
    }
}