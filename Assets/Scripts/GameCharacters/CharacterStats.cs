using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordfishGame
{
    public abstract class CharacterStats : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] protected int health;
        [SerializeField] protected float movementSpeed;

        public bool isAlive;

        protected int initHeath;

        // Gets/Sets
        public float MovementSpeed { get => movementSpeed; }
        public int Health { get => health; }

        public void TakeDamage(int damage)
        {
            health -= damage;
        }

        protected void CheckIfAlive()
        {
            if (health <= 0)
            {
                health = 0;
                isAlive = false;
            }
        }
    }
}
