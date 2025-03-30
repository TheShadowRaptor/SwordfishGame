using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SwordfishGame
{
    public class InputManager : MonoBehaviour
    {
        [Header("Mobile Settings")]
        [SerializeField] private Joybutton joyMoveRight;
        [SerializeField] private Joybutton joyMoveLeft;
        // public Joybutton joyAttack; 

        //Gets/Sets Mobile
        public Joybutton JoyMoveRight { get => joyMoveRight; }
        public Joybutton JoyMoveLeft { get => joyMoveLeft; }

    }
}
