using System;
using UnityEngine;

using Scripts.Nodes;
using Scripts.Enums;
using Scripts.Interfaces;

namespace Scripts.Branches
{
    public sealed class Branch : MonoBehaviour, IConnectable, IDraggable
    {
        [SerializeField] private Gizmos gizmos;
        [SerializeField] private UILineRendererTool renderer;
    
        [SerializeField] private OutNode nodeIn;
        [SerializeField] private InNode nodeOut;
        
        private bool isConnected = false;
        public bool IsConnected { get => isConnected; }
        
        private void Start()
        {
            if (gizmos == null)
                gizmos = GetComponent<Gizmos>();
            if (renderer == null)
                renderer = GetComponent<UILineRendererTool>();
        }
        
        public void SetNodeIn(OutNode node)
        {
            nodeIn = node;
            Connect();
        }
        
        public void SetNodeOut(InNode node)
        {
            nodeOut = node;
            nodeOut.Connect();

            Connect();
            SetState();
            OnStopDrag();
        }

        public void SetState()
        {
            if (nodeIn != null && nodeOut != null)
            {
                nodeOut.SetState(nodeIn.State);
                
                NodeState nodeState;
                if (nodeIn.State == 0)
                    nodeState = NodeState.Off;
                else
                    nodeState = NodeState.On;

                if (!nodeIn.IsClickable)
                    nodeState |= NodeState.Disable;
                
                gizmos.Display(nodeState);
            }
        }

        public void Connect()
        {
            if (nodeIn == null)
                isConnected = false;
            else
            {
                isConnected = nodeIn.IsConnected;
            
                if(nodeOut != null)
                    nodeOut.Connect();
            }
        }

        public void Connect(bool value)
        {
            
        }

        public void ClearConnections()
        {
            if(nodeIn != null)
                nodeIn.RemoveBranch(this);

            if (nodeOut != null)
            {
                nodeOut.RemoveBranch(this);
                
                nodeOut.Connect();
                nodeOut.SetState(0);
            }
        }

        public void OnStartDrag()
        {
            if (nodeIn == null || nodeOut == null)
                return;

            renderer.PushVector(Vector2.zero);
            renderer.SetAllDirty();
        }

        public void OnStopDrag()
        {
            if (nodeIn == null || nodeOut == null)
                return;
            
            transform.position = nodeIn.transform.position;
            
            renderer.PushVector(nodeOut.Position - nodeIn.Position);
            renderer.SetAllDirty();
        }
    }
}