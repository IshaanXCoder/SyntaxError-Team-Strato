using Scripts.Enums;
using Scripts.Branches;
using UnityEngine;

namespace Scripts.Nodes
{
    public abstract class InNode : Node
    {
        public override NodeType Type { get => NodeType.In; }

        protected Branch branch;
        public override bool BranchConnected { get => branch != null; }

        public override void SetBranch(Branch toSet)
        {
            branch = toSet;
        }
        
        public override void RemoveBranch(Branch toSet)
        {
            branch = null;
        }

        public override void Connect()
        {
            if (branch == null)
                isConnected = false;
            else
                isConnected = branch.IsConnected;
        }

        public override void Connect(bool value)
        {  }

        public override void OnStartDrag()
        {
            if(branch != null)
                branch.OnStartDrag();
        }

        public override void OnStopDrag()
        {
            if(branch != null)
                branch.OnStopDrag();
        }

        public override void ManageDeleteBranches()
        {
            if(branch != null)
                BranchManager.Instance.DeleteBranch(branch);
        }
    }
}