using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordfishGame
{
    public class PlayerStats : CharacterStats
    {
        [SerializeField] int damage = 1;
        [SerializeField] float stamina = 100;

        [Header("CamerSettings")]
        [SerializeField] float mouseSensitivity = 100f;
        [SerializeField] float mobileLookSensitivity = 50f;

        // Gets/Sets
        public float MouseSensitivity { get => mouseSensitivity; }
        public float MobileLookSensitivity { get => mobileLookSensitivity; }
        public int Damage { get => damage; }
        public float Stamina { get => stamina; }

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
