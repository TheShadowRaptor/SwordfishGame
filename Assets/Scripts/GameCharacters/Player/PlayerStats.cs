using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordfishGame
{
    public class PlayerStats : CharacterStats
    {
        [SerializeField] int bulletSpeed = 50;
        [SerializeField] int bulletDamage = 1;

        [Header("CamerSettings")]
        [SerializeField] float mouseSensitivity = 100f;
        [SerializeField] float minViewDistance = 25f;

        // Gets/Sets
        public float MouseSensitivity { get => mouseSensitivity; }
        public float MinViewDistance { get => minViewDistance; }
        public int BulletSpeed { get => bulletSpeed; }
        public int BulletDamage { get => bulletDamage; }

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
