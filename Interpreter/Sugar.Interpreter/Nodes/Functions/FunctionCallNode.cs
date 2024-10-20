using System;

using Sugar.Interpreter.Caching;

using Sugar.Interpreter.Exceptions;

using Sugar.Interpreter.Nodes.Enums;

using Sugar.Interpreter.Nodes.Interfaces;

namespace Sugar.Interpreter.Nodes.Functions
{
    internal sealed class FunctionCallNode : Node, IValueNode
    {
        public override NodeType Type { get => NodeType.FunctionCallNode; }
        
        private readonly string name;
        private readonly List<IValueNode> arguments;

        public FunctionCallNode(string _name)
        {
            name = _name;
   
            arguments = new List<IValueNode>();
        }
        
        public void AddArgument(IValueNode argument)
        {
            arguments.Add(argument);
        }

        public int? Value(Cache cache)
        {
            var function = cache.GetFunction(name);
            if (function == null)
                throw new CompileException($"function {name} not found");
            
            if (arguments.Count != function.Count)
                throw new CompileException("argument lengths do not match");
        
            var localCache = new LocalCache(GlobalCache.Instance);
            for(int i = 0; i < arguments.Count; i++)
                localCache.SetVariable(function[i], arguments[i].Value(cache));
            
            return function.Value(localCache);
        }
        
        public override string ToString() => $"Function Call Node: { name }";
        protected override void PrintChildren(string indent)
        {
            for(int i = 0; i < arguments.Count; i++)
                arguments[i].Print(indent, i == arguments.Count - 1);
        }
    }
}

