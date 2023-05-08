using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SwordfishGame
{
    public class EnemyStats : CharacterStats
    {
        [SerializeField] protected float turnSpeed;
        private float deathTimer = 1.5f;
        public float TurnSpeed { get => turnSpeed; }

        public GameObject _buoy;

        // Start is called before the first frame update
        void Start()
        {
            isAlive = true;
        }

        // Update is called once per frame
        void Update()
        {
            CheckIfAlive();
            if (Deactivate())
            {
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                deathTimer -= Time.deltaTime;
                if (deathTimer < 0)
                {
                    GameObject buoy = Instantiate(_buoy,transform.position,Quaternion.identity);
                    Buoys.buoys.Add(buoy);
                    this.gameObject.SetActive(false);
                }
            }
        }

        public bool Deactivate()
        {
            if (isAlive) return false;
            else return true;
        }
    }
}
