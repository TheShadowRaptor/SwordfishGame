using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace SwordfishGame
{
    public class EnemyController : MonoBehaviour
    {
        Rigidbody rb;
        [SerializeField] protected EnemyStats stats;


        [Header("Patrol Settings")]
        [SerializeField] protected List<GameObject> targets = new List<GameObject>();
        protected GameObject currentTarget;
        protected int nextTarget = 0;

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
            rb = GetComponent<Rigidbody>();
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
            ChooseNextTarget();

            // Rotation Calculation
            Quaternion targetRotation = Quaternion.LookRotation(currentTarget.transform.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, stats.TurnSpeed * Time.deltaTime);

            // Position Calculation
            // Subtracting Y from vectors
            Vector3 gameObjectPos = gameObject.transform.position;
            Vector3 targetObjectPos = currentTarget.transform.position;

            gameObjectPos.y = 0;
            targetObjectPos.y = 0;

            float targetDist = Vector3.Distance(gameObjectPos, targetObjectPos);

            if (targetDist > 2) transform.Translate(Vector3.forward * stats.MovementSpeed * Time.deltaTime);
            else nextTarget++;
        }

        void ChooseNextTarget()
        {
            if (nextTarget < targets.Count) currentTarget = targets[nextTarget];
            else nextTarget = targets.Count;
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
