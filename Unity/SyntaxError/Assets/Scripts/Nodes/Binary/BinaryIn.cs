using UnityEngine;
using UnityEngine.Events;

using Scripts.Enums;

namespace Scripts.Nodes.Binary
{
    public sealed class BinaryIn : BinaryInNode
    {
        [SerializeField] private UnityEvent onClick;
        [SerializeField] private UnityEvent onConnect;
        
        public override NodeType Type { get => NodeType.In; }

        protected override void OnSetState()
        {
            base.OnSetState();
            
            if(isConnected)
                onClick.Invoke();
        }

        public override void Connect()
        {
            if (branch == null)
                isConnected = false;
            else
            {
                isConnected = branch.IsConnected;
                if(isConnected)
                    onConnect.Invoke();
            }
        }

        protected override void PointerDown()
        {
            BranchManager.Instance.CompleteBranch(this);
        }
    }
}