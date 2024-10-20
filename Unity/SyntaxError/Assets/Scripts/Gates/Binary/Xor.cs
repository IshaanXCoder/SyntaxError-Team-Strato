using Scripts.Enums;

namespace Scripts.Gates.Binary
{
    public sealed class Xor : Gate
    {
        public override GateType Type { get => GateType.Xor; }
        
        public override void Evaluate()
        {
            if(nodesIn[0].State != nodesIn[1].State)
                nodesOut[0].SetState(1);
            else
                nodesOut[0].SetState(0);
        }
    }
}