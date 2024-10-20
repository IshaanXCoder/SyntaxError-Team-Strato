using System;

using Sugar.Interpreter.Caching;

using Sugar.Interpreter.Nodes.Interfaces;

namespace Sugar.Interpreter.Nodes.Operations.Relational
{
    internal sealed class GreaterThanNode : OperatorNode
    {
        public GreaterThanNode(IValueNode _a, IValueNode _b) : base(_a, _b)
        {
 
        }

        public override int? Value(Cache cache) => a.Value(cache) > b.Value(cache) ? 1 : 0;
        
        public override string ToString() => "Greater Than Node";
    }
}