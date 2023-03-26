using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

namespace SwordfishGame
{
    public class PlayerController : MonoBehaviour
    {
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

        }

        private void FixedUpdate()
        {
            MovePlayer();
        }   

        public void SpawnOnSpawnPoint()
        {
            Transform spawnPointTranform = GameObject.Find("PlayerSpawnPoint").transform;
            gameObject.transform.position = spawnPointTranform.position;
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

        void FindComponents()
        {
            inputManager = MasterSingleton.Instance.InputManager;
            playerStats = MasterSingleton.Instance.PlayerStats;


            rb = gameObject.GetComponent<Rigidbody>();
        }
    }
}
