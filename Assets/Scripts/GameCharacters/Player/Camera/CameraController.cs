using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordfishGame
{
    public class CameraController : MonoBehaviour
    {
        [Header("CameraSettings")]
        [SerializeField] float minViewAngle = 45f;

        [Header("LeanSettings")]
        [SerializeField] float leanViewAngle = 45;
        [SerializeField] float leanForwardDistance = 2;

        [SerializeField] float maxVerticalLeanViewAngle = 45;
        [SerializeField] float minVerticalLeanViewAngle = 0;

        [SerializeField] float maxHorizontalLeanViewAngle = 45;
        [SerializeField] float minHorizontalLeanViewAngle = -45;

        private float xRotation;

        private Vector3 basePosition;

        // Singletons
        InputManager inputManager;
        PlayerStats playerStats;
        PlayerController playerController;

        // Start is called before the first frame update
        void Start()
        {
            FindComponents();
            basePosition = new Vector3(0, 1f, 0);
        }

        bool enteredLean = false;
        // Update is called once per frame
        void Update()
        {
            if (MasterSingleton.Instance.GameManager.gameState != GameManager.GameState.gameplay) return;

            // Get the joystick input values
            float horizontalInput = inputManager.MobileLookHorizontal;
            float verticalInput = inputManager.MobileLookVertical;

            if (playerController.isLeaning)
            {
                if (!enteredLean)
                {
                    // Calculate the desired position and rotation for the camera when leaning
                    Vector3 playerPos = playerController.transform.position;
                    playerPos.y = playerPos.y + 1f;
                    Vector3 leanPosition = playerPos + playerController.transform.forward * leanForwardDistance;
                    Quaternion leanRotation = Quaternion.LookRotation(playerController.transform.forward, Vector3.up);

                    // Apply the lean position and rotation to the container object
                    transform.position = leanPosition;
                    transform.rotation = leanRotation;
                    enteredLean = true;
                }

                // Rotate the container object based on input
                Vector3 currentEulerAngles = transform.rotation.eulerAngles;
                float rotationX = currentEulerAngles.x - verticalInput;
                float rotationY = currentEulerAngles.y + horizontalInput;

                // Get the player's current Euler angles
                Vector3 playerEulerAngles = playerController.transform.eulerAngles;

                // Calculate the clamped angle range based on the player's facing direction
                float minAngle = playerEulerAngles.y - 0.5f * maxHorizontalLeanViewAngle;
                float maxAngle = playerEulerAngles.y + 0.5f * maxHorizontalLeanViewAngle;

                // Clamp the rotation values within the specified range
                rotationX = Mathf.Clamp(rotationX, minVerticalLeanViewAngle, maxVerticalLeanViewAngle);
                rotationY = Mathf.Clamp(rotationY, minAngle, maxAngle);

                // Prevent wrapping around the other side
                if (rotationY < 0f)
                {
                    rotationY = 0f;
                }
                else if (rotationY > 359f)
                {
                    rotationY = 359f;
                }

                if (rotationX < 0f)
                {
                    rotationX = 0f;
                }
                else if (rotationX > 359f)
                {
                    rotationX = 359f;
                }

                // Apply the adjusted rotation to the container object
                transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
            }
            else
            {
                xRotation -= verticalInput;
                xRotation = Mathf.Clamp(xRotation, -90f, minViewAngle);

                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                playerController.transform.Rotate(Vector3.up * horizontalInput);

                // Reset the camera position
                transform.localPosition = basePosition;

                if (enteredLean)
                {
                    enteredLean = false;
                }
            }
        }

        void FindComponents()
        {
            inputManager = MasterSingleton.Instance.InputManager;
            playerStats = MasterSingleton.Instance.PlayerStats;
            playerController = MasterSingleton.Instance.PlayerController;
        }
    }
}
