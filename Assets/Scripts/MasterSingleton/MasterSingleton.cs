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
        private EnemyManager enemyManager;
        private UIManager uIManager;
        private InputManager inputManager;
        private PlayerStats playerStats;
        private PlayerController playerController;
        private WeaponController weaponController;
        private OceanManager oceanManager;

        // SingletonObjects Gets/Sets
        public GameManager GameManager { get => gameManager; }
        public LevelManager LevelManager { get => levelManager; }
        public EnemyManager EnemyManager { get => enemyManager; }
        public UIManager UIManager { get => uIManager; }
        public InputManager InputManager { get => inputManager; }
        public PlayerStats PlayerStats { get => playerStats; }
        public PlayerController PlayerController { get => playerController; }
        public WeaponController WeaponController { get => weaponController; }
        public OceanManager OceanManager { get => oceanManager; }

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
            playerStats = GetComponentInChildren<PlayerStats>();
            playerController = GetComponentInChildren<PlayerController>();
            weaponController = GetComponentInChildren<WeaponController>();
            oceanManager = GetComponentInChildren<OceanManager>();
        }
    }
}
