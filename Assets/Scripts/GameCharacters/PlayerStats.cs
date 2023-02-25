using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordfishGame
{

    public class PlayerStats : MonoBehaviour
    {
        [Header("Player Movement Settings")]
        [SerializeField] float movementSpeed = 5;

        // Gets/Sets
        public float MovementSpeed { get => movementSpeed; }

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
