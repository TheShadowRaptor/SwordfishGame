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

        // Mobile
        public FixedJoystick movementJoystick; 
        public FixedJoystick lookJoystick; 
        public Joybutton joyAttack; 
        public Joybutton joyLean; 

        private float mobileMoveHorizontal;
        private float mobileMoveVertical;

        private float mobileLookHorizontal;
        private float mobileLookVertical;

        private bool screenPressed = false;
        private float tapDurationThreshold = 0.2f; // Adjust this value as needed
        private float touchStartTime;

        //Gets/Sets
        public Vector3 PlayerMovementInput { get => playerMovementInput; }
        public bool AttackInput { get => attackInput; }
        public bool LeanInput { get => leanInput; }
        public float MouseX { get => mouseX; }
        public float MouseY { get => mouseY; }

        //Gets/Sets Mobile
        public float MobileMoveHorizontal { get => mobileMoveHorizontal; }
        public float MobileMoveVertical { get => mobileMoveVertical; }
        public float MobileLookHorizontal { get => mobileLookHorizontal; }
        public float MobileLookVertical { get => mobileLookVertical; }
        public bool ScreenPressed { get => screenPressed; }


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

                mobileMoveHorizontal = movementJoystick.Horizontal;
                mobileMoveVertical = movementJoystick.Vertical;

                // Mobile Input (Using old input system)
                if (JoystickIsMoved(lookJoystick))
                {
                    mobileLookHorizontal = lookJoystick.Horizontal * playerStats.MobileLookSensitivity * Time.deltaTime;
                    mobileLookVertical = lookJoystick.Vertical * playerStats.MobileLookSensitivity * Time.deltaTime;
                }
                else
                {
                    mobileLookHorizontal = 0;
                    mobileLookVertical = 0;
                }

                // Screen press detect
                if (Input.touchCount > 0)
                {
                    for (int i = 0; i < Input.touchCount; i++)
                    {
                        Touch touch = Input.GetTouch(i);

                        if (touch.phase == UnityEngine.TouchPhase.Began)
                        {
                            // Record the start time of the touch
                            touchStartTime = Time.time;
                        }
                        else if (touch.phase == UnityEngine.TouchPhase.Ended)
                        {
                            // Calculate the duration of the touch
                            float touchDuration = Time.time - touchStartTime;

                            if (touchDuration < tapDurationThreshold)
                            {
                                // The touch duration is below the threshold, consider it a quick tap
                                screenPressed = true;
                                Debug.Log("Quick tap detected");
                            }
                            else
                            {
                                // Reset the boolean variable and touch start time
                                screenPressed = false;
                                touchStartTime = 0f;
                            }
                        }
                        else
                        {
                            screenPressed = false;
                        }
                    }

                }
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

        bool JoystickIsMoved(Joystick joystick)
        {
            if (joystick.Horizontal > 0.25 || joystick.Horizontal < -0.25 || joystick.Vertical > 0.25 || joystick.Vertical < -0.25)
            {
                return true;
            }
            return false;
        }

        void FindComponents()
        {
            playerStats = MasterSingleton.Instance.PlayerStats;
        }
    }
}
