using System;
using Sugar.Interpreter.Nodes.Functions;

namespace Sugar.Interpreter.Caching
{
    internal abstract class Cache
    {
        public abstract int GetInputCache(int index);
        public abstract int GetOutputCache(int index);
        public abstract void SetOutputCache(int index, int value);
        
        public abstract void SetVariable(string name, int? value);
        public abstract int? GetVariable(string name);
    
        public abstract void SetFunction(FunctionNode function);
        public abstract FunctionNode GetFunction(string name);
    }
}
