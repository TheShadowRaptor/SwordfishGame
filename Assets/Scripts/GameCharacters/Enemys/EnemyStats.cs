using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SwordfishGame
{
    public class EnemyStats : CharacterStats
    {
        public float TurnSpeed { get => turnSpeed; }

        public GameObject _buoy;
        [SerializeField] protected float turnSpeed;
        [SerializeField] protected EnemyController _enemyController;

        private float deathTimer = 1.5f;
        private float deathTimerReset = 1.5f;

        // Start is called before the first frame update

        // Update is called once per frame
        void Update()
        {
            CheckIfAlive();
            if (isAlive == false)
            {
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                deathTimer -= Time.deltaTime;
                if (deathTimer < 0)
                {
                    GameObject buoy = Instantiate(_buoy,transform.position,Quaternion.identity);
                    Buoys.buoys.Add(buoy);
                    gameObject.GetComponent<Rigidbody>().useGravity = false;
                    if (gameObject.transform.GetChild(1) != null)
                    {
                        GameObject spear = gameObject.transform.GetChild(1).gameObject;
                        MasterSingleton.Instance.SpearPool.ParentSingleSpearToWeapon(MasterSingleton.Instance.WeaponController.gameObject, spear);
                    }

                    // Reset Timer
                    deathTimer = deathTimerReset;
                    
                    this.gameObject.SetActive(false);
                }
            }
        }

        public void Spawn()
        {
            health = initHeath;
            isAlive = true;
            _enemyController.enemyState = EnemyController.EnemyStates.swimming;
        }

        public void InitStats(int health)
        {
            initHeath = health;
        }
    }
}
