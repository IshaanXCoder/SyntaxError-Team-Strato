using Scripts.Enums;

namespace Scripts.Gates.Binary
{
    public sealed class XnorGate : Gate
    {
        public override GateType Type { get => GateType.Xnor; }
        
        public override void Evaluate()
        {
            if(nodesIn[0].State != nodesIn[1].State)
                nodesOut[0].SetState(0);
            else
                nodesOut[0].SetState(1);
        }
    }
}