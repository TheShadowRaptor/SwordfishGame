using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordfishGame
{
    public class EnemyController : MonoBehaviour
    {
        enum EnemyStates
        {
            attacking,
            dying
        }
        EnemyStates enemyStates;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            switch (enemyStates)
            {
                case EnemyStates.attacking:
                    
                    break; 
                case EnemyStates.dying:
                    break;
            }
        }
    }
}
