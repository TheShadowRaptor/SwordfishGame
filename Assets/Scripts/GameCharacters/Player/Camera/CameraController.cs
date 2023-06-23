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
            basePosition = new Vector3(0, 0.9f, 0);
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        private void LateUpdate()
        {
            if (MasterSingleton.Instance.GameManager.gameState != GameManager.GameState.gameplay) return;
            ChangeCameraView();
        }

        bool enteredLean = false;
        void ChangeCameraView()
        {
            if (playerController.isLeaning)
            {
                if (!enteredLean)
                {
                    // Calculate the desired position and rotation for the camera when leaning
                    Vector3 leanPosition = playerController.transform.position + playerController.transform.forward * leanForwardDistance;
                    Quaternion leanRotation = Quaternion.LookRotation(playerController.transform.forward, Vector3.up);

                    // Apply the lean position and rotation to the container object
                    transform.position = leanPosition;
                    transform.rotation = leanRotation;
                    enteredLean = true;
                }

                // Rotate the container object based on input
                float horizontalInput = inputManager.MouseX;
                float verticalInput = inputManager.MouseY;

                // Get the current rotation of the container object
                Vector3 currentEulerAngles = transform.rotation.eulerAngles;

                // Adjust the rotation values based on input
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

                // Apply the adjusted rotation to the container object
                transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
            }
            else
            {
                xRotation -= inputManager.MouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, minViewAngle);

                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                playerController.transform.Rotate(Vector3.up * inputManager.MouseX);

                // Reset the camera position
                transform.localPosition = basePosition;

                if (enteredLean)
                {
                    enteredLean = false;
                }
            }    
        }

        // Helper function to normalize an angle to be within 0-360 degrees
        float NormalizeAngle(float angle)
        {
            while (angle < 0f)
            {
                angle += 360f;
            }

            while (angle > 360f)
            {
                angle -= 360f;
            }

            return angle;
        }

        void FindComponents()
        {
            inputManager = MasterSingleton.Instance.InputManager;
            playerStats = MasterSingleton.Instance.PlayerStats;
            playerController = MasterSingleton.Instance.PlayerController;

        }
    }
}
