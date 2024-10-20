using Scripts.Enums;

namespace Scripts.Gates.Binary
{
    public sealed class OrGate : Gate
    {
        public override GateType Type { get => GateType.Or; }
        
        public override void Evaluate()
        {
            if(nodesIn[0].State == 1 || nodesIn[1].State == 1)
                nodesOut[0].SetState(1);
            else
                nodesOut[0].SetState(0);
        }
    }
}