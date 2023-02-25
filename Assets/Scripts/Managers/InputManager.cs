using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SwordfishGame
{
    public class InputManager : MonoBehaviour
    {
        public InputAction moveAction;

        // Switches
        bool inputEnabled;

        // Structs
        private Vector3 movementInput;

        //Gets/Sets
        public Vector3 MovementInput { get => movementInput; }

        public void InputEnabled(bool on)
        {
            // Freedom to toggle controls when needed
            if (on)
            {
                inputEnabled = true;
                return;
            }
            inputEnabled = false;
        }

        public void OnMovePreformed(InputAction.CallbackContext context)
        {
            // Reads input values
            movementInput = context.ReadValue<Vector3>();
        }

        public void ManageInput()
        {
            if (moveAction == null) return;
            if (inputEnabled)
            {
                moveAction.Enable();
                moveAction.performed += OnMovePreformed;
            }
            else
            {
                moveAction.Disable();
                moveAction.performed -= OnMovePreformed;
            }
        }
    }
}
