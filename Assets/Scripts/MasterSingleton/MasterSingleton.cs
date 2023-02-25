using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordfishGame
{
    public class MasterSingleton : MonoBehaviour
    {
        static private MasterSingleton instance;
        static public MasterSingleton Instance { get => instance; }

        [Header("SingletonObjects")]
        private PlayerStats playerStats;
        private PlayerController playerController;
        private InputManager inputManager;

        // SingletonObjects Gets/Sets
        public PlayerStats PlayerStats { get => playerStats; }
        public PlayerController PlayerController { get => playerController; }
        public InputManager InputManager { get => inputManager; }

        // Awake is called when object is first initialized
        void Awake()
        {
            if (instance != null && instance == this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        private void Start()
        {
            playerStats = transform.Find("Player").GetComponent<PlayerStats>();
            playerController = transform.Find("Player").GetComponent<PlayerController>();
            inputManager = transform.Find("InputManager").GetComponent<InputManager>();
        }
    }
}
