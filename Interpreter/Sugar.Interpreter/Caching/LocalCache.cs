using Sugar.Interpreter.Exceptions;
using Sugar.Interpreter.Nodes.Functions;

namespace Sugar.Interpreter.Caching
{
    internal sealed class LocalCache : Cache
    {
        private readonly GlobalCache globalCache;
        private readonly Dictionary<string, int?> variableCache;    
    
        public LocalCache(GlobalCache _global)
        {
            globalCache = _global;
            variableCache = new Dictionary<string, int?>();
        }

        public override int GetInputCache(int index) => globalCache.GetInputCache(index);
        public override int GetOutputCache(int index) => globalCache.GetOutputCache(index);
        public override void SetOutputCache(int index, int value) => globalCache.SetOutputCache(index, value);
    
        public override int? GetVariable(string name) 
        {
            if(variableCache.ContainsKey(name))
                return variableCache[name];
            else
                return globalCache.GetVariable(name);
        }
    
        public override void SetVariable(string name, int? value)
        {
            if(variableCache.ContainsKey(name))
                variableCache[name] = value;
            else
                variableCache.Add(name, value);  
        }
    
        public override FunctionNode GetFunction(string name)  => globalCache.GetFunction(name);
        public override void SetFunction(FunctionNode function) => throw new CompileException("local functions are not permitted");
    }
}

