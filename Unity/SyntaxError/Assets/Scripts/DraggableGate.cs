using System;

using UnityEngine;
using UnityEngine.EventSystems;

using Scripts.Gates;

namespace Scripts
{
    public sealed class DraggableGate : Draggable, IEndDragHandler, IBeginDragHandler, IPointerDownHandler
    {
        [SerializeField] private string trigger;

        [SerializeField] private Gate gate;
        
        private bool isInTrigger;
        
        void Start()
        {
            isInTrigger = true;

            if (gate == null)
                gate = GetComponent<Gate>();
        }
        
        
        public void OnEndDrag(PointerEventData data)
        {
            if (isInTrigger)
            {
                gate.OnStopDrag();
                return;
            }
            
            Destroy(gameObject);
            CircuitManager.Instance.ToggleDeletePrompt(false);
        }
        
        public void OnBeginDrag(PointerEventData data)
        {
            gate.OnStartDrag();
        }
        
        public void OnTriggerStay2D(Collider2D other)
        {
            if(other.CompareTag(trigger))
                isInTrigger = true;
        }
        
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(trigger))
            {
                isInTrigger = true;
                CircuitManager.Instance.ToggleDeletePrompt(false);
            }
        }
        
        public void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(trigger))
            {
                isInTrigger = false;
                CircuitManager.Instance.ToggleDeletePrompt(true);
            }
        }
        
        public void OnPointerDown(PointerEventData data)
        {
            if(data.pointerId == -2)
                Destroy(gameObject);
        }
    }
}