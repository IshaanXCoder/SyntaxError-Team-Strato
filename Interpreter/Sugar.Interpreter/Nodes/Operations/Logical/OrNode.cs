using System;

using Sugar.Interpreter.Caching;

using Sugar.Interpreter.Nodes.Interfaces;

namespace Sugar.Interpreter.Nodes.Operations.Logical
{
    internal sealed class OrNode : OperatorNode
    {
        public OrNode(IValueNode _a, IValueNode _b) : base(_a, _b)
        {
 
        }

        public override int? Value(Cache cache) => a.Value(cache) == 1 || b.Value(cache) == 1 ? 1 : 0;
        
        public override string ToString() => "Or Node";
    }
}