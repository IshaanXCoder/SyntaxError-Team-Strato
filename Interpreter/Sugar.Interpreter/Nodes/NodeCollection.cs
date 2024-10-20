using System;

using System.Collections;

using Sugar.Interpreter.Caching;

using Sugar.Interpreter.Nodes.Enums;

using Sugar.Interpreter.Nodes.Interfaces;

namespace Sugar.Interpreter.Nodes
{
    internal sealed class NodeCollection : Node, ICollection<INode>, IExecutableNode
    {
        public override NodeType Type { get => NodeType.NodeCollection; }

        private readonly List<INode> collection;
        public int Count { get => collection.Count; }
        public bool IsReadOnly { get => false; }
    
        public INode this[int index] { get => collection[index]; }

        public NodeCollection()
        {
            collection = new List<INode>();
        }
    
        public void Add(INode item) => collection.Add(item);

        public void Clear() =>  collection.Clear();

        public bool Contains(INode item) => collection.Contains(item);

        public void CopyTo(INode[] array, int arrayIndex) => throw new NotImplementedException();

        public bool Remove(INode item) => collection.Remove(item);

        public void Execute(Cache cache)
        {
            foreach (var node in collection)
            {
                if ((node.Type & NodeType.ValueNode) == node.Type)
                    ((IValueNode)node).Value(GlobalCache.Instance);
                else if ((node.Type & NodeType.ExecutableNode) == node.Type)
                    ((IExecutableNode)node).Execute(GlobalCache.Instance);
            }
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<INode> GetEnumerator()
        {
            return new NodeEnumerator(this);
        }
        
        public override string ToString() => $"Node Collection";
        protected override void PrintChildren(string indent)
        {
            for(int i = 0; i < collection.Count; i++)
                collection[i].Print(indent, i == collection.Count - 1);
        }
    }
}

