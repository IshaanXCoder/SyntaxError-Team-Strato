using System;

using Sugar.Interpreter.Caching;

namespace Sugar.Interpreter.Nodes.Interfaces
{
    internal interface IValueNode : INode
    {
        public int? Value(Cache cache);
    }
}

