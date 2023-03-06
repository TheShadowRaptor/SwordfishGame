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
        private GameManager gameManager;
        private LevelManager levelManager;
        private UIManager uIManager;
        private InputManager inputManager;
        private BulletManager bulletManager;
        private PlayerStats playerStats;
        private PlayerController playerController;

        // SingletonObjects Gets/Sets
        public GameManager GameManager { get => gameManager; }
        public LevelManager LevelManager { get => levelManager; }
        public UIManager UIManager { get => uIManager; }
        public InputManager InputManager { get => inputManager; }
        public BulletManager BulletManager { get => bulletManager; }
        public PlayerStats PlayerStats { get => playerStats; }
        public PlayerController PlayerController { get => playerController; }

        // Awake is called when object is first initialized
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            else
            {
                Destroy(gameObject);
                
            }
            DontDestroyOnLoad(gameObject);

            // Find child gameobjects
            gameManager = transform.Find("GameManager").GetComponent<GameManager>();
            levelManager = transform.Find("LevelManager").GetComponent<LevelManager>();
            uIManager = transform.Find("UIManager").GetComponent<UIManager>();
            inputManager = transform.Find("InputManager").GetComponent<InputManager>();
            bulletManager = transform.Find("BulletManager").GetComponent<BulletManager>();
            playerStats = transform.Find("Player").GetComponent<PlayerStats>();
            playerController = transform.Find("Player").GetComponent<PlayerController>();
        }
    }
}
