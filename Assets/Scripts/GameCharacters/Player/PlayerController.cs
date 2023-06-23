using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

namespace SwordfishGame
{
    public class PlayerController : MonoBehaviour
    {
        // Public bools
        public bool isLeaning = false;

        // Singletons
        InputManager inputManager;
        PlayerStats playerStats;

        // Components
        Rigidbody rb;
        

        // Start is called before the first frame update
        void Start()
        {
            FindComponents();
        }

        // Update is called once per frame
        void Update()
        {
            if (MasterSingleton.Instance.GameManager.gameState != GameManager.GameState.gameplay) return;
            LeanOverSide();
        }

        private void FixedUpdate()
        {
            if (MasterSingleton.Instance.GameManager.gameState != GameManager.GameState.gameplay) return;
            if (isLeaning) return;
            MovePlayer();
        }   

        public void SpawnOnSpawnPoint()
        {
            Transform spawnPointTranform = GameObject.Find("PlayerSpawnPoint").transform;
            gameObject.transform.position = spawnPointTranform.position;
        }
        public bool CanLean()
        {
            if (Physics.Raycast(Camera.main.gameObject.transform.position, Camera.main.transform.forward, out RaycastHit leanableHit, 2f, leanOverable)) return true;           
            else return false;
        }
        
        void MovePlayer()
        {
            // For easier Reading
            Vector3 input = inputManager.PlayerMovementInput;
            float speed = playerStats.MovementSpeed;

            Vector3 playerVelocity = new Vector3(input.x * speed, rb.velocity.y, input.z * speed);

            // Moves player
            rb.velocity = transform.TransformDirection(playerVelocity);
        }

        [SerializeField] private LayerMask leanOverable;

        private bool leanButtonPressed = false;
        void LeanOverSide()
        {
            if (isLeaning && !inputManager.LeanInput)
            {
                isLeaning = false;
                Debug.Log("isNotLeaning");
            }
            else if (CanLean())
            {
                if (inputManager.LeanInput && !leanButtonPressed)
                {
                    // Go into lean view
                    isLeaning = true;
                    leanButtonPressed = true;
                    Debug.Log("isLeaning");

                    // Debug visualization
                    Debug.DrawRay(Camera.main.gameObject.transform.position, Camera.main.transform.forward * 1.9f, Color.green, 2f);
                }
                else if (!inputManager.LeanInput)
                {
                    leanButtonPressed = false;
                }
            }
            else
            {
                // Debug visualization
                Debug.DrawRay(gameObject.transform.position, transform.forward * 1.9f, Color.red, 1.0f);
            }
        }


        void FindComponents()
        {
            inputManager = MasterSingleton.Instance.InputManager;
            playerStats = MasterSingleton.Instance.PlayerStats;


            rb = gameObject.GetComponent<Rigidbody>();
        }
    }
}
