using System;

namespace Scripts.Enums
{
    [Flags]
    public enum NodeState
    {
        On = 1,
        Off = 2,
        Disable = 4,
        OnDisable = On | Disable,
        OffDisable = Off | Disable
    }
}