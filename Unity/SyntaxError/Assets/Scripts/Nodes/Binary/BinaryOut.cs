using Scripts.Enums;

namespace Scripts.Nodes.Binary
{
    public sealed class BinaryOut : BinaryOutNode
    {
        public override NodeType Type { get => NodeType.Out; }

        protected override void OnSetState()
        {
            foreach (var branch in branches)
                branch.SetState();
        }

        public override void Connect(bool value)
        {
            isConnected = value;
            
            foreach (var branch in branches)
                branch.Connect();
        }
    }
}