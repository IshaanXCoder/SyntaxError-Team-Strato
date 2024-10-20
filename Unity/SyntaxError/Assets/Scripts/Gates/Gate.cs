using System;
using System.Collections.Generic;

using UnityEngine;

using Scripts.Nodes;
using Scripts.Enums;
using Scripts.Interfaces;

namespace Scripts.Gates
{
    public abstract class Gate : MonoBehaviour, IEvaluate, IDraggable
    {
        public abstract GateType Type { get; }
      
        [SerializeField] protected List<InNode> nodesIn;
        public IReadOnlyCollection<InNode> NodesIn {get => nodesIn.AsReadOnly(); }
        
        [SerializeField] protected List<OutNode> nodesOut;
        public IReadOnlyCollection<OutNode> NodesOut {get => nodesOut.AsReadOnly(); }

        void Start()
        {
            BranchManager.Instance.PushGate(this);
        }
        
        public abstract void Evaluate();

        public void Connect()
        {
            bool connect = false;
            foreach (var node in nodesIn)
                if (node.IsConnected)
                {
                    connect = true;
                    break;
                }
            
            foreach (var node in nodesOut)
                node.Connect(connect);
        }

        public void OnStartDrag()
        {
            foreach (var node in nodesIn)
                node.OnStartDrag();
            
            foreach (var node in nodesOut)
                node.OnStartDrag();
        }

        
        public void OnStopDrag()
        {
            foreach (var node in nodesIn)
                node.OnStopDrag();
            
            foreach (var node in nodesOut)
                node.OnStopDrag();
        }

        private void OnDestroy()
        {
            foreach (var node in nodesIn)
                node.ManageDeleteBranches();
            
            foreach (var node in nodesOut)
                node.ManageDeleteBranches();
            
            BranchManager.Instance.DeleteGate(this);
        }
    }
}