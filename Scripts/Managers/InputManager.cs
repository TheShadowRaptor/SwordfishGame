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

        // Structs
        private Vector3 playerMovementInput;
        private bool attackInput;

        // Singletons
        PlayerStats playerStats;

        // Varibles
        float mouseX;
        float mouseY;

        // Mobile
        public FixedJoystick movementJoystick; 
        public FixedJoystick lookJoystick; 

        private float mobileMoveHorizontal;
        private float mobileMoveVertical;

        private float mobileLookHorizontal;
        private float mobileLookVertical;

        private bool screenPressed = false;
        private float tapDurationThreshold = 0.2f; // Adjust this value as needed
        private float touchStartTime;

        bool joystickDirUpHit;
        bool joystickDirDownHit;
        bool joystickDirRightHit;
        bool joystickDirLeftHit;

        public float timeToCompleteCircle = 5f;

        //Gets/Sets
        public Vector3 PlayerMovementInput { get => playerMovementInput; }
        public bool AttackInput { get => attackInput; }
        public float MouseX { get => mouseX; }
        public float MouseY { get => mouseY; }

        //Gets/Sets Mobile
        public float MobileMoveHorizontal { get => mobileMoveHorizontal; }
        public float MobileMoveVertical { get => mobileMoveVertical; }
        public float MobileLookHorizontal { get => mobileLookHorizontal; }
        public float MobileLookVertical { get => mobileLookVertical; }
        public bool ScreenPressed { get => screenPressed; }



        private void OnEnable()
        {
            playerMoveAction.Enable();
            attackAction.Enable();
            playerMoveAction.performed += OnMovePreformed;
            attackAction.performed += OnAttackPreformed;
        }

        private void OnDisable()
        {
            playerMoveAction.Disable();
            attackAction.Disable();
            playerMoveAction.performed -= OnMovePreformed;
            attackAction.performed -= OnAttackPreformed;
        }
        void Start()
        {
            playerStats = MasterSingleton.Instance.PlayerStats;

            // Mouse Input (Using old input system)
            mouseX = Input.GetAxis("Mouse X") * playerStats.MouseSensitivity * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * playerStats.MouseSensitivity * Time.deltaTime;
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

        void OnAttackPreformed(InputAction.CallbackContext context)
        {
            attackInput = context.ReadValueAsButton();
        }

        void ManageInput()
        {
            if (playerMoveAction == null) return;

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

        public bool JoystickIsMoved(Joystick joystick)
        {
            if (joystick.Horizontal > 0.25 || joystick.Horizontal < -0.25 || joystick.Vertical > 0.25 || joystick.Vertical < -0.25)
            {
                return true;
            }
            return false;
        }

        public bool JoystickCircled(Joystick joystick) 
        {
            if (joystick.Horizontal > 0.25) joystickDirRightHit = true;
            if (joystick.Horizontal < -0.25) joystickDirLeftHit = true;
            if (joystick.Vertical > 0.25) joystickDirUpHit = true;
            if (joystick.Vertical < -0.25) joystickDirDownHit = true;

            if (joystickDirRightHit || joystickDirLeftHit || joystickDirDownHit || joystickDirUpHit) 
            {
                timeToCompleteCircle -= Time.fixedDeltaTime;

                if (timeToCompleteCircle < 0) 
                {
                    joystickDirRightHit = false;
                    joystickDirLeftHit = false;
                    joystickDirDownHit = false;
                    joystickDirUpHit = false;
                    timeToCompleteCircle = 1f;
                }
            }
            if (joystickDirRightHit && joystickDirLeftHit && joystickDirDownHit && joystickDirUpHit) 
            {
                joystickDirRightHit = false;
                joystickDirLeftHit = false;
                joystickDirDownHit = false;
                joystickDirUpHit = false;
                timeToCompleteCircle = 1f;
                return true;
            }
            return false;
        }
    }
}
