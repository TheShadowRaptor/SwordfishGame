using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SwordfishGame
{
    public class MasterSingleton : MonoBehaviour
    {
        static private MasterSingleton instance;
        static public MasterSingleton Instance { get => instance; }

        [Header("SingletonObjects")]
        private GameManager gameManager;
        private LevelManager levelManager;
        private EnemyManager enemyManager;
        private UIManager uIManager;
        private InputManager inputManager;
        private PlayerController playerController;
        private CameraController cameraController;
        private OceanManager oceanManager;

        // SingletonObjects Gets/Sets
        public GameManager GameManager { get => gameManager; }
        public LevelManager LevelManager { get => levelManager; }
        public EnemyManager EnemyManager { get => enemyManager; }
        public UIManager UIManager { get => uIManager; }
        public InputManager InputManager { get => inputManager; }
        public PlayerController PlayerController { get => playerController; }
        public CameraController CameraController { get => cameraController; }
        public OceanManager OceanManager { get => oceanManager; }
        public EventSystem eventSystem;

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
            gameManager = GetComponentInChildren<GameManager>();
            levelManager = GetComponentInChildren<LevelManager>();
            enemyManager = GetComponentInChildren<EnemyManager>();
            uIManager = GetComponentInChildren<UIManager>();
            inputManager = GetComponentInChildren<InputManager>();
            playerController = GetComponentInChildren<PlayerController>();
            cameraController = GetComponentInChildren<CameraController>();
            oceanManager = GetComponentInChildren<OceanManager>();
        }
    }
}
