using Scripts.Enums;

namespace Scripts.Nodes.Binary
{
    public abstract class BinaryInNode : InNode
    {
        protected override void OnSetState()
        {
            NodeState nodeState;
            if (state == 0)
                nodeState = NodeState.Off;
            else
                nodeState = NodeState.On;

            if (!isClickable)
                nodeState |= NodeState.Disable;
            
            gizmos.Display(nodeState);
        }
    }
}