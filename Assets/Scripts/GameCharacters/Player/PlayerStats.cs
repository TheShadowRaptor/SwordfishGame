using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordfishGame
{

    public class PlayerStats : MonoBehaviour
    {
        [Header("Player Movement Settings")]
        [SerializeField] float movementSpeed = 5;

        [Header("Camera Settings")]
        [SerializeField] float mouseSensitivity = 100f;
        [SerializeField] float minViewDistance = 25f;

        // Gets/Sets
        public float MovementSpeed { get => movementSpeed; }
        public float MouseSensitivity { get => mouseSensitivity; }
        public float MinViewDistance { get => minViewDistance; }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
