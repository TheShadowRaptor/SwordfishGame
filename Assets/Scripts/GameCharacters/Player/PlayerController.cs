using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordfishGame
{
    public class PlayerController : MonoBehaviour
    {
        // Public bools
        public bool isLeaning = false;
        public bool isMobile;

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

        private void FixedUpdate()
        {
            if (MasterSingleton.Instance.GameManager.gameState != GameManager.GameState.gameplay) return;
            MovePlayer();
        }   

        public void SpawnOnSpawnPoint()
        {
            if (MasterSingleton.Instance.GameManager.gameState != GameManager.GameState.gameplay) return;
            Transform spawnPointTranform = GameObject.Find("PlayerSpawnPoint").transform;
            gameObject.transform.position = spawnPointTranform.position;
        }
        
        void MovePlayer()
        {
            Vector3 mobileInput = new Vector3(inputManager.MobileMoveHorizontal, 0, inputManager.MobileMoveVertical);
            float speed = playerStats.MovementSpeed;

            Vector3 playerMobileVelocity = Vector3.zero;

            // Mobile
            playerMobileVelocity = mobileInput * speed;
            rb.velocity = transform.TransformDirection(playerMobileVelocity);
            
        }
        
        void FindComponents()
        {
            inputManager = MasterSingleton.Instance.InputManager;
            playerStats = MasterSingleton.Instance.PlayerStats;


            rb = gameObject.GetComponent<Rigidbody>();
        }
    }
}
