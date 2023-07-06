using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SwordfishGame
{
    public class EnemyController : MonoBehaviour
    {
        Rigidbody rb;
        [SerializeField] protected EnemyStats stats;
        [SerializeField] protected GameObject ship;

        [Header("Patrol Settings")]
        public GameObject target;
        protected int nextTarget = 0;
        bool willJump = false;

        // Targets selected for the next enemy to use
        List<GameObject> selectedTargets = new List<GameObject>();

        public enum EnemyStates
        {
            swimming,
            ramming,
            attacking,
            dying
        }
        public EnemyStates enemyState;

        private void OnEnable()
        {
            EnemyManager.enemyAliveCounter += 1;
        }

        private void OnDisable()
        {
            EnemyManager.enemyAliveCounter -= 1;
        }

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            enemyState = EnemyStates.swimming;
            ship = ShipStats.Instance.gameObject;
            stats.timeTillNextAttackReset = stats.timeTillNextAttack;
        }

        // Update is called once per frame
        void Update()
        {          
            if (stats.isAlive == false) enemyState = EnemyStates.dying;
            switch (enemyState)
            {
                case EnemyStates.swimming:
                    Swim();
                    break;

                case EnemyStates.ramming:
                    Swim();
                    break;

                case EnemyStates.attacking:
                    CheckIfCanDamageShip();
                    break;
                    
                case EnemyStates.dying:
                    break;
            }
        }

        private void Swim()
        {
            if (target == null)
            {
                return;
            }

            // Rotate towards target
            Vector3 targetDirection = target.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, stats.TurnSpeed * Time.deltaTime);

            Vector3 enemyDistancePos = transform.position;
            Vector3 targetDistancePos = target.transform.position;

            enemyDistancePos.y = 0;
            targetDistancePos.y = 0;

            float distance = Vector3.Distance(enemyDistancePos, targetDistancePos);

            if (enemyState == EnemyStates.swimming)
            {
                // Move towards target
                transform.position += transform.forward * stats.MovementSpeed * Time.deltaTime;

                // Check if half way from the target
                if (distance <= 20f)
                {
                    enemyState = EnemyStates.ramming;
                }
            }
            else if (enemyState == EnemyStates.ramming) 
            {
                // Ram towards target
                transform.position += transform.forward * stats.rammingSpeed * Time.deltaTime;
                // Check if reached the target
                if (distance < 1f)
                {
                    enemyState = EnemyStates.attacking;
                }
            }
        }

        private bool CanAttack()
        {
            stats.timeTillNextAttack -= Time.deltaTime;

            if (stats.timeTillNextAttack <= 0)
            {
                return true;
            }
            return false;
        }

        private void CheckIfCanDamageShip()
        {
            if (CanAttack())
            {
                ship.GetComponent<ShipStats>().TakeDamage(stats.damage);
                stats.timeTillNextAttack = stats.timeTillNextAttackReset;
            }
        }
    }
}
