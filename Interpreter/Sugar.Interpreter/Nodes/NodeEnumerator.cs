using System;

using System.Collections;

using Sugar.Interpreter.Nodes.Interfaces;

namespace Sugar.Interpreter.Nodes
{
    internal sealed class NodeEnumerator : IEnumerator<INode>
    {
        int position = -1;
        private readonly NodeCollection collection;
    
        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }
    
        public INode Current
        {
            get
            {
                try
                {
                    return collection[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    
        public NodeEnumerator(NodeCollection _collection)
        {
            collection = _collection;
        }
    
        public bool MoveNext()
        {
            position++;
            return position < collection.Count;
        }
    
        public void Reset()
        {
            position = -1;
        }
    
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}

