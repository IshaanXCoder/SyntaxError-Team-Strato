using Sugar.Interpreter.Caching;

namespace Sugar.Interpreter.Nodes.Interfaces
{
    internal interface IExecutableNode : INode
    {
        public void Execute(Cache cache);
    }
}

