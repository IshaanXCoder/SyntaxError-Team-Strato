using Sugar.Interpreter.Exceptions;

using Sugar.Interpreter.Nodes.Functions;

namespace Sugar.Interpreter.Caching
{
    internal sealed class GlobalCache : Cache
{
    private static GlobalCache instance;
    public static GlobalCache Instance { get => instance ; }

    public static void Initalise()
    {
        instance = new GlobalCache();
    }
    
    private readonly Dictionary<string, int?> variableCache;    
    private readonly Dictionary<string, FunctionNode> functionCache;

    private readonly List<int> inputCache; 
    private readonly List<int> outputCache; 
    public GlobalCache()
    {
        variableCache = new Dictionary<string, int?>();
        functionCache = new Dictionary<string, FunctionNode>();

        inputCache = new List<int>();
        outputCache = new List<int>();
    }

    public void InitialiseCache(IReadOnlyList<int> input, int outputLength)
    {
        inputCache.Clear();
        for(int i = 0; i < input.Count; i++)
            inputCache.Add(input[i]);

        outputCache.Clear();
        for(int i = 0; i < outputLength; i++)
            outputCache.Add(0);
    }

    public override int GetInputCache(int index)
    {
        if (index < 0 || index > inputCache.Count)
            throw new CompileException($"invalid index for input. length: {inputCache.Count}");

        return inputCache[index];
    }
    
    public override int GetOutputCache(int index)
    {
        
        if (index < 0 || index > outputCache.Count)
            throw new CompileException($"invalid index for output. length: {inputCache.Count}");

        return outputCache[index];
    }
    
    public override void SetOutputCache(int index, int value)
    {
        if (index < 0 || index > outputCache.Count)
            throw new CompileException($"invalid index for output. length: {inputCache.Count}");

        outputCache[index] = value;
    }
    
    public override int? GetVariable(string name) 
    {
        if(variableCache.ContainsKey(name))
            return variableCache[name];
        
        return null;
    }
    
    public override void SetVariable(string name, int? value)
    {
        if(variableCache.ContainsKey(name))
            variableCache[name] = value;
        else if(!functionCache.ContainsKey(name))
            variableCache.Add(name, value);        
    }

    public override FunctionNode GetFunction(string name) 
    {
        if(functionCache.ContainsKey(name))
            return functionCache[name];
        
        return null;
    }

    public override void SetFunction(FunctionNode function)
    {
        if(functionCache.ContainsKey(function.Name))
            functionCache[function.Name] = function;
        else if(!variableCache.ContainsKey(function.Name))
            functionCache.Add(function.Name, function);
    }
}
}

