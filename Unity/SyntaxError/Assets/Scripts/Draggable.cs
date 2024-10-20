using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts
{
    public class Draggable : MonoBehaviour, IDragHandler
    {
        [SerializeField] protected RectTransform rect;

        public void OnDrag(PointerEventData data)
        {
            rect.anchoredPosition += data.delta;
        }
    }
}
