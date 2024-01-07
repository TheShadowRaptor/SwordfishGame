using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SwordfishGame
{
    public class InputManager : MonoBehaviour
    {
        // Switches
        public bool inputEnabled;

        // Singletons
        PlayerStats playerStats;

        // Mobile
        public FixedJoystick movementJoystick; 
        public FixedJoystick lookJoystick; 
        public Joybutton joyAttack; 
        public Joybutton joyAim; 
        // public Joybutton joyLean; 

        private float mobileMoveHorizontal;
        private float mobileMoveVertical;

        private float mobileLookHorizontal;
        private float mobileLookVertical;

        //Gets/Sets Mobile
        public float MobileMoveHorizontal { get => mobileMoveHorizontal; }
        public float MobileMoveVertical { get => mobileMoveVertical; }
        public float MobileLookHorizontal { get => mobileLookHorizontal; }
        public float MobileLookVertical { get => mobileLookVertical; }


        void Start()
        {
            FindComponents();
        }

        void Update()
        {
            ReadInput();
        }

        void ReadInput()
        {
            if (MasterSingleton.Instance.GameManager.gameState == GameManager.GameState.gameplay)
            {
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
