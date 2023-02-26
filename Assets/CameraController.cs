using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordfishGame
{
    public class CameraController : MonoBehaviour
    {
        float xRotation;

        // Singletons
        InputManager inputManager;
        PlayerStats playerStats;
        PlayerController playerController;

        // Start is called before the first frame update
        void Start()
        {
            FindComponents();
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        void Update()
        {
             xRotation -= inputManager.MouseY;
             xRotation = Mathf.Clamp(xRotation, -90f, playerStats.MinViewDistance);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerController.transform.Rotate(Vector3.up * inputManager.MouseX);
        }

        void FindComponents()
        {
            inputManager = MasterSingleton.Instance.InputManager;
            playerStats = MasterSingleton.Instance.PlayerStats;
            playerController = MasterSingleton.Instance.PlayerController;

        }
    }
}
