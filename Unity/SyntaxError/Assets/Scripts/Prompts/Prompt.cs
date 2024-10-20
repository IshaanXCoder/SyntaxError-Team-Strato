using System;
using UnityEngine;

namespace Scripts.Prompts
{
    [Serializable]
    public class Prompt
    {
        [SerializeField] private GameObject prompt;
        [SerializeField] private GameObject button;
        
        private bool isShowing = false;
        public bool IsShowing { get => isShowing; }

        public void Display(bool show)
        {
            isShowing = show;

            prompt.SetActive(isShowing);
            
            if(button != null)
                button.SetActive(!isShowing);
        }

        public void Toggle()
        {
            prompt.SetActive(!prompt.activeSelf);
            
            if(button != null)
                button.SetActive(!button.activeSelf);

            isShowing = !isShowing;
        }
    }
}