using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Utilities
{
    public sealed class PrefabButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text text;

        void Start()
        {
            if (button == null)
                button = GetComponent<Button>();
        }

        public void Initialise(int index, string name)
        {
            text.text = name;
            
            button.onClick.AddListener(() =>
            {
                CircuitManager.Instance.CreateCustom(index);
            });
        }
    }
}