using System;

using UnityEngine;

using Scripts.Enums;

namespace Scripts
{
    public abstract class Gizmos : MonoBehaviour
    {
        [SerializeField] protected Color onColor;
        [SerializeField] protected Color offColor;
        [SerializeField] protected Color onDisableColor;
        [SerializeField] protected Color offDisableColor;

        public abstract void Display(NodeState state);
    }
}