using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordfishGame
{
    public class PlayerStats : CharacterStats
    {
        [SerializeField] int damage = 1;

        [Header("CamerSettings")]
        [SerializeField] float mouseSensitivity = 100f;
        [SerializeField] float minViewDistance = 45f;

        // Gets/Sets
        public float MouseSensitivity { get => mouseSensitivity; }
        public float MinViewDistance { get => minViewDistance; }
        public int Damage { get => damage; }

        // Start is called before the first frame update
        void Start()
        {
            isAlive = true;
        }

        // Update is called once per frame
        void Update()
        {
            CheckIfAlive();
        }
    }
}
