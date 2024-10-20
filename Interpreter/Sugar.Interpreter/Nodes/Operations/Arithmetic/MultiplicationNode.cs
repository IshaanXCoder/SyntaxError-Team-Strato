using System;

using Sugar.Interpreter.Caching;

using Sugar.Interpreter.Nodes.Interfaces;

namespace Sugar.Interpreter.Nodes.Operations.Arithmetic
{
    internal sealed class MultiplicationNode : OperatorNode
    {
        public MultiplicationNode(IValueNode _a, IValueNode _b) : base(_a, _b)
        {
 
        }

        public override int? Value(Cache cache) => a.Value(cache) * b.Value(cache);
        
        public override string ToString() => "Multiplication Node";
    }
}