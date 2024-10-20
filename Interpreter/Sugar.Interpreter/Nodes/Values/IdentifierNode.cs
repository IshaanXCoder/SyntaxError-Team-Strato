using System;

using Sugar.Interpreter.Tokens;

using Sugar.Interpreter.Caching;

using Sugar.Interpreter.Nodes.Enums;

using Sugar.Interpreter.Nodes.Interfaces;

namespace Sugar.Interpreter.Nodes.Values
{
    internal sealed class IdentifierNode : Node, INameable, IValueNode
    {
        public override NodeType Type { get => NodeType.IdentifierNode; }
    
        private readonly Identifier name;
        public string Name { get => name.Value; }

        public IdentifierNode(Identifier _identifier)
        {
            name = _identifier;
        }

        public int? Value(Cache cache) => cache.GetVariable(name.Value);
        
        public override string ToString() => $"Identifier Node: { name.Value }";
        protected override void PrintChildren(string indent) { }
    }
}

