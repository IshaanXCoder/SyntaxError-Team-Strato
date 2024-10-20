using System;

using Sugar.Interpreter.Caching;

using Sugar.Interpreter.Nodes.Enums;

using Sugar.Interpreter.Nodes.Interfaces;

namespace Sugar.Interpreter.Nodes.Functions
{
    internal sealed class FunctionNode : Node, INameable, IBody, IValueNode, IExecutableNode
    {
        public override NodeType Type { get => NodeType.FunctionNode; }
        
        private readonly string name;
        public string Name { get => name; }
    
        private readonly IReadOnlyList<string> parameters;
        public int Count { get => parameters.Count; }
        public string this[int index] { get => parameters[index]; }

        private readonly INode body;
        public INode Body { get => body; }

        public FunctionNode(string _name, IReadOnlyList<string> _parameters, INode _body)
        {
            name = _name;
            body = _body;

            parameters = _parameters;
        }

        public void Execute(Cache cache)
        {
            cache.SetFunction(this);
        }
        
        public int? Value(Cache cache)
        {
            var collection = (NodeCollection)body;
            for (int i = 0; i < collection.Count - 1; i++)
                Execute(collection[i]);

            if(collection.Count > 0)
                return Execute(collection[collection.Count - 1]);

            return null;

            int? Execute(INode node)
            {
                if((node.Type & NodeType.ValueNode) == node.Type)
                    return ((IValueNode)node).Value(cache);
                
                if((node.Type & NodeType.ExecutableNode) == node.Type)
                    ((IExecutableNode)node).Execute(cache);

                return null;
            }
        }
        
        public override string ToString() => $"Function Node: { name }";
        protected override void PrintChildren(string indent)
        {
            body.Print(indent, true);
        }
    }
}

