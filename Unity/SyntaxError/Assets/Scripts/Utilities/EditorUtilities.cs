using System;
using System.Collections.Generic;

using UnityEngine;
using TMPro;

using Scripts;
using Scripts.Enums;
using Scripts.Gates;
using Scripts.Prompts;
using Scripts.Gates.Prefabs;

using Sugar.Interpreter;
using UnityEngine.UI;

namespace Scripts.Utilities
{
    public sealed class EditorUtilities : MonoBehaviour
    {
        [SerializeField] private Button createButton;
        
        [SerializeField] private Prompt prefabPrompt;
        [SerializeField] private Prompt deletePrompt;
        [SerializeField] private Prompt editorPrompt;
        [SerializeField] private Prompt customPrompt;
    
        [SerializeField] private TextPrompt errorPrompt;
        
        [SerializeField] private TMP_InputField inCount;
        [SerializeField] private TMP_InputField outCount;
        [SerializeField] private TMP_InputField nameText;
        [SerializeField] private TMP_InputField sourceCode;
        [SerializeField] private TMP_InputField prefabName;
        
        void Start()
        {
            prefabPrompt.Display(false);
            deletePrompt.Display(false);
        
            editorPrompt.Display(false);
            customPrompt.Display(false);

            createButton.interactable = false;
        }

        void LateUpdate()
        {
            if(!int.TryParse(inCount.text, out int count))
                return;

            if (count < 0 || count > 5)
                return;
            
            if(!int.TryParse(outCount.text, out count))
                return;

            if (count < 0 || count > 5)
                return;

            createButton.interactable = true;
        }

        public void CreateProgrammable()
        {
            CircuitManager.Instance.CreateProgrammable(
                int.Parse(inCount.text), 
                int.Parse(outCount.text),  
                sourceCode.text.Replace(Environment.NewLine, "\n").Replace("\t", ""), 
                nameText.text);

            inCount.text = "";
            outCount.text = "";
            nameText.text = "";
            sourceCode.text = "";
            
            ToggleProgrammablePrompt();
        }
        
        public void TogglePrefabPrompt() => prefabPrompt.Toggle();
        public void ToggleProgrammablePrompt() => editorPrompt.Toggle();
    
        public void ToggleDeletePrompt(bool toggle) => deletePrompt.Display(toggle);
    
        public void ToggleErrorPrompt()
        {
            errorPrompt.Display(false);
        } 
        public void ToggleErrorPrompt(string message)
        {
            errorPrompt.Display(true);
            errorPrompt.SetText(message);
        }

        public void InitialiseProgrammablePrompt(int vectorIn, int vectorOut, string source)
        {
            inCount.text = vectorIn.ToString();
            outCount.text = vectorOut.ToString();

            sourceCode.text = source;
        }

        public void ToggleCustomPrompt()
        {
            prefabName.text = "";
            customPrompt.Toggle();
        }
        
        public void PushCustomPrompt()
        {
            customPrompt.Toggle();
            BranchManager.Instance.CreatePrefab(prefabName.text);
        }
    }
}