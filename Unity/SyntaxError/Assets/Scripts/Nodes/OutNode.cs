using System.Collections.Generic;

using Scripts.Enums;
using Scripts.Branches;

namespace Scripts.Nodes
{
    public abstract class OutNode : Node
    {
        public override NodeType Type { get => NodeType.Out; }

        protected List<Branch> branches = new List<Branch>();
        public override bool BranchConnected { get => branches.Count > 0; }
        public override void SetBranch(Branch toSet)
        {
            if(!branches.Contains(toSet))
                branches.Add(toSet);
        }
        
        public override void RemoveBranch(Branch toSet)
        {
            if(branches.Contains(toSet))
                branches.Remove(toSet);
        }
        
        public override void Connect()
        { }

        public override void Connect(bool value)
        {
            isConnected = value;
        }
        
        public override void OnStartDrag()
        {
            foreach (var branch in branches)
                branch.OnStartDrag();
        }

        public override void OnStopDrag()
        {
            foreach (var branch in branches)
                branch.OnStopDrag();
        }

        public override void ManageDeleteBranches()
        {
            while(branches.Count > 0)
                BranchManager.Instance.DeleteBranch(branches[0]);
        }
        
        protected override void PointerDown()
        {
            BranchManager.Instance.RegisterNewBranch(this);
            branches[branches.Count - 1].Connect();
        }
    }
}