using System;
using UnityEngine;

namespace Scripts.Gates.Prefabs
{
    [Serializable]
    public struct PrefabData
    {
        [SerializeField] private string name;
        public string Name { get => name; }
        
        [SerializeField] private int trueTrue; 
        public int TrueTrue { get => trueTrue; }
        
        [SerializeField] private int trueFalse;
        public int TrueFalse { get => trueFalse; }
        
        [SerializeField] private int falseTrue;
        public int FalseTrue { get => falseTrue; }
        
        [SerializeField] private int falseFalse;
        public int FalseFalse { get => falseFalse; }

        public PrefabData(string toSet, int tt, int tf, int ft, int ff)
        {
            name = toSet;
            
            trueTrue = tt;
            trueFalse = tf;
            falseTrue = ft;
            falseFalse = ff;
        }
    }
}