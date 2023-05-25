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
                    gameObject.GetComponent<Rigidbody>().useGravity = false;
                    GameObject spear = gameObject.transform.GetChild(1).gameObject;
                    spear.GetComponent<Spear>();
                    Destroy(gameObject.transform.GetChild(1));
                    
                    this.gameObject.SetActive(false);
                }
            }
        }

        public void Spawn()
        {
            health = initHeath;
            isAlive = true;
        }

        public bool Deactivate()
        {
            if (isAlive) return false;
            else return true;
        }

        public void InitStats(int health)
        {
            initHeath = health;
        }
    }
}
