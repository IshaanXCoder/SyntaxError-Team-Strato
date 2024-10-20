using System;

using Sugar.Interpreter.Caching;

using Sugar.Interpreter.Nodes.Enums;

using Sugar.Interpreter.Nodes.Interfaces;

namespace Sugar.Interpreter.Nodes.Conditionals
{
    internal sealed class WhileLoopNode : BaseConditionalNode
    {
        public override NodeType Type { get => NodeType.WhileNode; }
        
        public WhileLoopNode(IValueNode _expression, INode _body) : base(_expression, _body)
        {
            
        }

        public override void Execute(Cache cache)
        {
            if((body.Type & NodeType.ValueNode) == body.Type)
                RunValueNode((IValueNode)body, cache);
            
            if((body.Type & NodeType.ExecutableNode) == body.Type)
                RunExecutableNode((IExecutableNode)body, cache);
        }

        private void RunValueNode(IValueNode node, Cache cache)
        {
            while (expression.Value(cache) == 1)
                node.Value(cache);
        }
        
        private void RunExecutableNode(IExecutableNode node, Cache cache)
        {
            while (expression.Value(cache) == 1)
                node.Execute(cache);
        }

        public override string ToString() => $"While Node";
    }
}

