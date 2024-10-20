using System;

using Sugar.Interpreter.Caching;

using Sugar.Interpreter.Nodes.Enums;

using Sugar.Interpreter.Nodes.Interfaces;

namespace Sugar.Interpreter.Nodes.Conditionals
{
    internal sealed class IfNode : BaseConditionalNode
    {
        public override NodeType Type { get => NodeType.IfNode; }
        
        public IfNode(IValueNode _expression, INode _body) : base(_expression, _body)
        {
            
        }

        public override void Execute(Cache cache)
        {
            if (expression.Value(cache) == 1)
            {
                if((body.Type & NodeType.ValueNode) == body.Type)
                    ((IValueNode)body).Value(cache);
            
                if((body.Type & NodeType.ExecutableNode) == body.Type)
                    ((IExecutableNode)body).Execute(cache);
            }
        }
        
        public override string ToString() => $"If Node";
    }
}

