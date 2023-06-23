using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SwordfishGame
{
    public class InputManager : MonoBehaviour
    {
        // Controls
        public InputAction playerMoveAction;
        public InputAction attackAction;
        public InputAction leanAction;

        // Switches
        public bool inputEnabled;

        // Structs
        private Vector3 playerMovementInput;
        private bool attackInput;
        private bool leanInput;

        private bool canPressKey = true;

        // Singletons
        PlayerStats playerStats;

        // Varibles
        float mouseX;
        float mouseY;

        //Gets/Sets
        public Vector3 PlayerMovementInput { get => playerMovementInput; }
        public bool AttackInput { get => attackInput; }
        public bool LeanInput { get => leanInput; }
        public float MouseX { get => mouseX; }
        public float MouseY { get => mouseY; }


        void Start()
        {
            FindComponents();
        }

        void Update()
        {
            if (MasterSingleton.Instance.GameManager.gameState == GameManager.GameState.gameplay) EnableInput(true);
            else EnableInput(false);
            ManageInput();
        }

        public void EnableInput(bool enabled)
        {
            if (enabled) inputEnabled = true;
            else inputEnabled = false;
        }

        void OnMovePreformed(InputAction.CallbackContext context)
        {
            // Reads input values
            playerMovementInput = context.ReadValue<Vector3>();
        }

        void OnAttackPreformed(InputAction.CallbackContext context)
        {
            attackInput = context.ReadValueAsButton();
        }

        void OnLeanPreformed(InputAction.CallbackContext context)
        {
            leanInput = context.ReadValueAsButton();
        }

        void ManageInput()
        {
            if (playerMoveAction == null) return;
            if (inputEnabled)
            {
                playerMoveAction.Enable();
                attackAction.Enable();
                leanAction.Enable();
                playerMoveAction.performed += OnMovePreformed;
                attackAction.performed += OnAttackPreformed;
                leanAction.performed += OnLeanPreformed;

                // Mouse Input (Using old input system)
                mouseX = Input.GetAxis("Mouse X") * playerStats.MouseSensitivity * Time.deltaTime;
                mouseY = Input.GetAxis("Mouse Y") * playerStats.MouseSensitivity * Time.deltaTime;
            }
            else
            {
                playerMoveAction.Disable();
                attackAction.Disable();
                leanAction.Disable();
                playerMoveAction.performed -= OnMovePreformed;
                attackAction.performed -= OnAttackPreformed;
                leanAction.performed -= OnLeanPreformed;

            }
        }

        void FindComponents()
        {
            playerStats = MasterSingleton.Instance.PlayerStats;
        }
    }
}
