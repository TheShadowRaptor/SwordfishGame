using System.Collections.Generic;
using UnityEngine;

namespace SwordfishGame
{
    public class EnemyController : MonoBehaviour
    {
        Rigidbody rb;
        [SerializeField] protected EnemyStats stats;

        [Header("Patrol Settings")]
        public GameObject target;
        protected int nextTarget = 0;
        bool willJump = false;

        // Targets selected for the next enemy to use
        List<GameObject> selectedTargets = new List<GameObject>();

        public enum EnemyStates
        {
            swimming,
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
                case EnemyStates.attacking:
                    
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

            // Move towards target
            transform.position += transform.forward * stats.MovementSpeed * Time.deltaTime;

            // Check if we've reached the target
            if (Vector3.Distance(transform.position, target.transform.position) < 1f)
            {
                
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!stats.isAlive) return;
            if (other.gameObject.CompareTag("Harpoon"))
            {
                int damage = MasterSingleton.Instance.PlayerStats.Damage;
                stats.TakeDamage(damage);
                other.gameObject.GetComponent<WeaponController>().tipLoaded = false;
                // HitBox script also has an onTrigger checking for enemy
                //Debug.Log("damage");
            }
        }
    }
}
