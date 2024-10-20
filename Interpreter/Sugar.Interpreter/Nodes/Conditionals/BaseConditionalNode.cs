using Sugar.Interpreter.Caching;
using Sugar.Interpreter.Nodes.Interfaces;

namespace Sugar.Interpreter.Nodes.Conditionals
{
    internal abstract class BaseConditionalNode : Node, IBody, IExecutableNode
    {
        protected readonly IValueNode expression;

        protected readonly INode body;
        public INode Body { get => body; }

        protected BaseConditionalNode(IValueNode _expression, INode _body)
        {
            expression = _expression;

            body = _body;
        }

        public abstract void Execute(Cache cache);
        
        protected override void PrintChildren(string indent)
        {
            expression.Print(indent, false);
            body.Print(indent, true);
        }
    }
}

