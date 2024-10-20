using System;

using Sugar.Interpreter.Caching;

using Sugar.Interpreter.Nodes.Interfaces;

namespace Sugar.Interpreter.Nodes.Operations.Relational
{
    internal sealed class GreaterThanEqualsNode : OperatorNode
    {
        public GreaterThanEqualsNode(IValueNode _a, IValueNode _b) : base(_a, _b)
        {
 
        }

        public override int? Value(Cache cache) => a.Value(cache) >= b.Value(cache) ? 1 : 0;
        
        public override string ToString() => "Greater Than Equals Node";
    }
}