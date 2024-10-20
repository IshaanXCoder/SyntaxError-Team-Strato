using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Scripts.Enums;
using TMPro;

namespace Scripts.Gates.Prefabs
{
    public sealed class PrefabGate : Gate
    {
        public override GateType Type { get => GateType.Prefab; }

        [SerializeField] private PrefabData data;
        [SerializeField] private TMP_Text nameText;
        
        public void Initialise(PrefabData toSet)
        {
            data = toSet;
            nameText.text = data.Name;
        }
        
        public override void Evaluate()
        {
            int state1 = nodesIn[0].State;
            int state2 = nodesIn[0].State;

            if (state1 == 1)
            {
                if(state2 == 1)
                    nodesOut[0].SetState(data.TrueTrue);
                else
                    nodesOut[0].SetState(data.TrueFalse);
            }
            else if(state2 == 1)
                nodesOut[0].SetState(data.FalseTrue);
            else
                nodesOut[0].SetState(data.FalseFalse);
        }
    }
}

