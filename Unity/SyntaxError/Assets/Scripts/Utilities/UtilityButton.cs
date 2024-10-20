using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Utilities
{
    public sealed class UtilityButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject prompt;

        public void OnPointerEnter(PointerEventData data)
        {
            prompt.SetActive(true);
        }
        
        public void OnPointerExit(PointerEventData data)
        {
            prompt.SetActive(false);
        }
    }
}