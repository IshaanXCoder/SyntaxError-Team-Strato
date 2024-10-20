using System;

using UnityEngine;

using TMPro;

namespace Scripts.Prompts
{
    [Serializable]
    public sealed class TextPrompt : Prompt
    {
        [SerializeField] private TMP_Text text;

        public void SetText(string message)
        {
            text.text = message;
        }
    }
}