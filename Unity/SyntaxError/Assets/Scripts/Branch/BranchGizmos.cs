using UnityEngine;

using Scripts.Enums;

namespace Scripts.Branches
{
    public sealed class BranchGizmos : Gizmos
    {
        [SerializeField] private UILineRendererTool renderer;

        void Start()
        {
            if (renderer == null)
                renderer = GetComponent<UILineRendererTool>();
        }
        
        public override void Display(NodeState state)
        {
            switch (state)
            {
                case NodeState.On:
                    renderer.SetColor(onColor);
                    break;
                case NodeState.Off:
                    renderer.SetColor(offColor);
                    break;
                case NodeState.OnDisable:
                    renderer.SetColor(onDisableColor);
                    break;
                case NodeState.OffDisable:
                    renderer.SetColor(offDisableColor);
                    break;
            }
            
            renderer.SetAllDirty();
        }
    }
}