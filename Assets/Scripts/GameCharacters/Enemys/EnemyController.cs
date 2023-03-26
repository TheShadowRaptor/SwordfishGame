using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SwordfishGame
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] protected EnemyStats stats;

        [Header("Components")]
        [SerializeField] protected GameObject target;
        enum EnemyStates
        {
            swimming,
            attacking,
            dying
        }
        EnemyStates enemyStates;

        // Start is called before the first frame update
        void Start()
        {
            enemyStates = EnemyStates.swimming;
        }

        // Update is called once per frame
        void Update()
        {
            if (stats.Deactivate()) enemyStates = EnemyStates.dying;
            switch (enemyStates)
            {
                case EnemyStates.swimming:
                    Swim();
                    break;
                case EnemyStates.attacking:
                    
                    break; 
                case EnemyStates.dying:
                    break;
            }
        }

        void Swim()
        {
            this.gameObject.transform.position = Vector3.MoveTowards(transform.position, target.transform.position, stats.MovementSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("HitBox"))
            {
                int damage = MasterSingleton.Instance.PlayerStats.Damage;
                stats.TakeDamage(damage);
                // HitBox script also has an onTrigger checking for enemy
                //Debug.Log("damage");
            }
        }
    }
}
