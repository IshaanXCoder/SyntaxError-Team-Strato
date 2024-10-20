using Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Nodes
{
    public sealed class NodeGizmos : Gizmos
    {
        [SerializeField] private Image image;

        private void Start()
        {
            if (image == null)
                image = GetComponent<Image>();
        }

        public override void Display(NodeState state)
        {
            switch (state)
            {
                case NodeState.On:
                    image.color = onColor;
                    break;
                case NodeState.Off:
                    image.color = offColor;
                    break;
                case NodeState.OnDisable:
                    image.color = onDisableColor;
                    break;
                case NodeState.OffDisable:
                    image.color = offDisableColor;
                    break;
            }
        }
    }
}