using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SwordfishGame
{
    public class InputManager : MonoBehaviour
    {
        public InputAction playerMoveAction;

        // Switches
        bool inputEnabled;

        // Structs
        private Vector3 playerMovementInput;

        // Singletons
        PlayerStats playerStats;

        // Varibles
        float mouseX;
        float mouseY;

        //Gets/Sets
        public Vector3 PlayerMovementInput { get => playerMovementInput; }
        public float MouseX { get => mouseX; }
        public float MouseY { get => mouseY; }

        void Start()
        {
            FindComponents();
        }

        void Update()
        {
            ManageInput();
        }

        void OnMovePreformed(InputAction.CallbackContext context)
        {
            // Reads input values
            playerMovementInput = context.ReadValue<Vector3>();
        }

        void ManageInput()
        {
            if (playerMoveAction == null) return;
            if (inputEnabled)
            {
                playerMoveAction.Enable();
                playerMoveAction.performed += OnMovePreformed;

                // Mouse Input (Using old input system)
                mouseX = Input.GetAxis("Mouse X") * playerStats.MouseSensitivity * Time.deltaTime;
                mouseY = Input.GetAxis("Mouse Y") * playerStats.MouseSensitivity * Time.deltaTime;
            }
            else
            {
                playerMoveAction.Disable();
                playerMoveAction.performed -= OnMovePreformed;
            }
        }

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

        void FindComponents()
        {
            playerStats = MasterSingleton.Instance.PlayerStats;
        }
    }
}
