using System;

using UnityEngine;
using TMPro;

using Scripts.Enums;

using Sugar.Interpreter;

namespace Scripts.Gates
{
    public sealed class ProgrammableGate : Gate
    {
        public override GateType Type { get => GateType.Programmable; }

          
        [SerializeField] protected TMP_Text text;
        
        [SerializeField] private Transform inputParent;
        [SerializeField] private Transform outputParent;
        
        private int inCount = 0;
        private int outCount = 0;
        private string source = null;
        
        public void Initialise(int vectorIn, int vectorOut, string code, string name)
        {
            inCount = vectorIn;
            for(int i = 0; i < inCount; i++)
                inputParent.GetChild(i).gameObject.SetActive(true);
            
            outCount = vectorOut;
            for(int i = 0; i < outCount; i++)
                outputParent.GetChild(i).gameObject.SetActive(true);

            text.text = name;
            source = code.Replace(Environment.NewLine, "\n");
        }
        
        public override void Evaluate()
        {
            int[] input = new int[inCount];
            for (int i = 0; i < inCount; i++)
                input[i] = nodesIn[i].State;

            try
            {
                var result = Interpreter.Instance.Interpret(input, outCount, source);
                
                for(int i = 0; i < outCount; i++)
                    nodesOut[i].SetState(result[i]);
            }
            catch (Exception e)
            {
                CircuitManager.Instance.ToggleErrorPrompt($"ERROR: {e.Message}", inCount, outCount, source);
                
                for(int i = 0; i < outCount; i++)
                    nodesOut[i].SetState(0);
            }
        }
    }
}