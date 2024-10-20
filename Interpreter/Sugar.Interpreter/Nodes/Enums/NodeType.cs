namespace Sugar.Interpreter.Nodes.Enums
{
    
    [Flags]
    internal enum NodeType
    {   
        ConstantNode = 1,
        IdentifierNode = 2,
    
        OperatorNode = 4,
    
        IfNode = 8,
        WhileNode = 16,
    
        FunctionNode = 32,
        FunctionCallNode = 64,
    
        NodeCollection = 128,
    
        Input = 256,
        Output = 512,
    
        Null = 1024,
        Empty = 2048,
    
        ExecutableNode = FunctionNode | IfNode | WhileNode | NodeCollection,
        ValueNode = ConstantNode | IdentifierNode | OperatorNode | FunctionCallNode | Input | Output | Null 
    }
}
