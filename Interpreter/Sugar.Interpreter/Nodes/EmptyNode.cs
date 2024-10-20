using Sugar.Interpreter.Nodes.Enums;

namespace Sugar.Interpreter.Nodes;

internal sealed class EmptyNode : Node
{
    public override NodeType Type { get => NodeType.Empty; }

    public EmptyNode()
    {
        
    }

    public override string ToString() => $"Empty Node";
    protected override void PrintChildren(string indent) { }
}