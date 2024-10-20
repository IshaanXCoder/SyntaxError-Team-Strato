using Scripts.Enums;

namespace Scripts.Gates.Binary
{
    public sealed class NotGate : Gate
    {
        public override GateType Type { get => GateType.Not; }
        
        public override void Evaluate()
        {
            if(nodesIn[0].State == 1)
                nodesOut[0].SetState(0);
            else
                nodesOut[0].SetState(1);
        }
    }
}