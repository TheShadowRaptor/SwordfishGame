using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SwordfishGame
{
    public class Joybutton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerExitHandler
    {
        [SerializeField] protected bool pressed;

        // Get/Set
        public bool Pressed { get => pressed; }

        public void OnPointerUp(PointerEventData eventData)
        {
            pressed = false;
        }

        public void OnPointerDown(PointerEventData eventData) 
        {
            pressed = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            pressed = false;
        }
    }
}

