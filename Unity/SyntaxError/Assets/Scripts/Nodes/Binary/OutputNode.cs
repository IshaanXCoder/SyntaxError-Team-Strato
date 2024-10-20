using System;

using Scripts.Enums;

namespace Scripts.Nodes.Binary
{
    public sealed class OutputNode : BinaryInNode
    {
        public override NodeType Type { get => NodeType.In; }
        
        protected override void PointerDown()
        {
            BranchManager.Instance.CompleteBranch(this);
        }
    }
}