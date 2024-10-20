using Scripts.Enums;
using UnityEngine;

namespace Scripts.Nodes.Binary
{
    public sealed class InputNode : BinaryOutNode
    {
        public override NodeType Type { get => NodeType.Out; }

        protected override void OnStart()
        {
            isConnected = true;
        }

        public override void Connect()
        {  }
        
        public override void Connect(bool value)
        {  }
        
        public void Toggle()
        {
            SetState(state == 0 ? 1 : 0);

            foreach (var branch in branches)
                branch.SetState();
        }
    }
}