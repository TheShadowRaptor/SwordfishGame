using System.Collections;
using System.Collections.Generic;
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
            inputManager.InputEnabled(true);
        }

        // Update is called once per frame
        void Update()
        {
            inputManager.ManageInput();
        }

        private void FixedUpdate()
        {
            rb.velocity = inputManager.MovementInput * playerStats.MovementSpeed * Time.deltaTime;            
        }      

        void FindComponents()
        {
            inputManager = MasterSingleton.Instance.InputManager;
            playerStats = MasterSingleton.Instance.PlayerStats;

            rb = gameObject.GetComponent<Rigidbody>();
        }
    }
}
