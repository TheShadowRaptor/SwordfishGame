using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SwordfishGame
{
    public class EnemyStats : CharacterStats
    {
        [SerializeField] protected float turnSpeed;

        public float TurnSpeed { get => turnSpeed; }

        // Start is called before the first frame update
        void Start()
        {
            isAlive = true;
        }

        // Update is called once per frame
        void Update()
        {
            CheckIfAlive();
            if (Deactivate()) gameObject.GetComponent<Rigidbody>().useGravity = true;
        }

        public bool Deactivate()
        {
            if (isAlive) return false;
            else return true;
        }
    }
}
