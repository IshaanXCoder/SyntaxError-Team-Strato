using UnityEngine;
using UnityEngine.EventSystems;

using Scripts.Enums;
using Scripts.Branches;
using Scripts.Interfaces;

namespace Scripts.Nodes
{
    public abstract class Node : MonoBehaviour, IState, IConnectable, IDraggable, IPointerDownHandler
    {
        public abstract NodeType Type { get; }
        
        [SerializeField] protected NodeGizmos gizmos;

        protected int state;
        public int State { get => state; }

        protected bool isConnected;
        public bool IsConnected { get => isConnected; }

        protected bool isClickable;
        public bool IsClickable { get => isClickable; }
        
        public Vector2 Position { get => transform.position; }

        public abstract bool BranchConnected { get; }
        
        void Start()
        {
            state = 0;
            isConnected = false;
            
            if (gizmos == null)
                gizmos = GetComponent<NodeGizmos>();
            
            gizmos.Display(NodeState.Off);

            if (Type == NodeType.Out)
                isClickable = true;
            else
                isClickable = false;
            
            OnStart();
        }
        
        protected virtual void OnStart() { }

        public abstract void SetBranch(Branch toSet);
        public abstract void RemoveBranch(Branch toSet);

        public void ToggleClickable()
        {
            isClickable = !isClickable;
        }

        public abstract void Connect();
        public abstract void Connect(bool value);

        public void SetState(int toSet)
        {
            if (state == toSet)
                return;
            
            state = toSet;
            OnSetState();
        }
        
        protected virtual void OnSetState() { }

        public abstract void OnStartDrag();
        public abstract void OnStopDrag();
        
        public void OnPointerDown(PointerEventData data)
        {
            if (data.pointerId == -2)
                ManageDeleteBranches();
            else if (isClickable)
                PointerDown();
        }

        protected virtual void PointerDown() { }
        public abstract void ManageDeleteBranches();
    }
}
