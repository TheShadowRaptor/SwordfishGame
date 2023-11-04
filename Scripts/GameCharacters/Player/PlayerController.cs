using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordfishGame
{
    public class PlayerController : MonoBehaviour
    {
        Vector3 moveInput;

        // Singletons
        InputManager inputManager;
        PlayerStats playerStats;

        // Components
        Rigidbody rb;

        [Header("CameraSettings")]
        [SerializeField] float minViewAngle = 45f;

        private float horizontalCameraInput;
        private float verticalCameraInput;

        private float xRotation;


        // Start is called before the first frame update
        void Start()
        {
            inputManager = MasterSingleton.Instance.InputManager;
            playerStats = MasterSingleton.Instance.PlayerStats;

            horizontalCameraInput = inputManager.MobileLookHorizontal;
            verticalCameraInput = inputManager.MobileLookVertical;

            moveInput = inputManager.PlayerMovementInput;

            rb = gameObject.GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (MasterSingleton.Instance.GameManager.gameState == GameManager.GameState.inHud) 
            {
                MovePlayer();
            }

            if (MasterSingleton.Instance.GameManager.gameState == GameManager.GameState.gameplay)
            {
                MovePlayer();
            }
        }

        private void LateUpdate()
        {
            if (MasterSingleton.Instance.GameManager.gameState == GameManager.GameState.inHud)
            {
                TurnPlayer();
            }

            if (MasterSingleton.Instance.GameManager.gameState == GameManager.GameState.gameplay)
            {
                TurnPlayer();
            }
        }

        public void SpawnOnSpawnPoint()
        {
            if (MasterSingleton.Instance.GameManager.gameState == GameManager.GameState.inHud || MasterSingleton.Instance.GameManager.gameState == GameManager.GameState.gameplay)
            {
                Transform spawnPointTranform = GameObject.Find("PlayerSpawnPoint").transform;
                gameObject.transform.position = spawnPointTranform.position;

            }
        }
        
        void MovePlayer()
        {
            Vector3 mobileInput = new Vector3(inputManager.MobileMoveHorizontal, 0, inputManager.MobileMoveVertical);
            float speed = playerStats.MovementSpeed;

            Vector3 playerVelocity = Vector3.zero;
            Vector3 playerMobileVelocity = Vector3.zero;

            // PC
            playerVelocity = new Vector3(moveInput.x * speed, rb.velocity.y, moveInput.z * speed * Time.fixedDeltaTime);
            rb.velocity = transform.TransformDirection(playerVelocity);

            // Mobile
            playerMobileVelocity = mobileInput * speed * Time.fixedDeltaTime;
            rb.velocity = transform.TransformDirection(playerMobileVelocity);
        }

        void TurnPlayer() 
        {
            horizontalCameraInput = inputManager.MobileLookHorizontal;
            verticalCameraInput = inputManager.MobileLookVertical;

            xRotation -= verticalCameraInput;
            xRotation = Mathf.Clamp(xRotation, -90f, minViewAngle);

            gameObject.GetComponentInChildren<Camera>().gameObject.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            gameObject.transform.Rotate(Vector3.up * horizontalCameraInput);
        }
    }
}
