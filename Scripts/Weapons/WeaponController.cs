using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static NewEnemyController;

namespace SwordfishGame
{
    public class WeaponController : MonoBehaviour
    {
        // Singletons
        InputManager inputManager;

        [Header("Components")]
        [SerializeField] protected Animator weaponAnimator;
        [SerializeField] protected GameObject harpoonBullets;

        public bool loaded = true;
        public float reloadTime = 2;

        float reloadTimeReset;

        // Start is called before the first frame update
        void Start()
        {
            reloadTimeReset = reloadTime;                  
            FindComponents();
        }

        // Update is called once per frame
        void Update()
        {
            Attack();
        }

        private bool attackButtonPressed = false;
        void Attack()
        {
            if (loaded)
            {
                if (inputManager.ScreenPressed && !attackButtonPressed)
                {
                    //weaponAnimator.SetBool("isAttacking", true);
                    attackButtonPressed = true;
                    loaded = false;
                }
                else if (!inputManager.ScreenPressed)
                {
                    attackButtonPressed = false;
                }
            }
            else
            {
                Reload();
            }
        }

        void Reload()
        {
            //Debug.Log("Reloading");
            //weaponAnimator.SetBool("isAttacking", false);
            reloadTime -= Time.deltaTime;
            if (reloadTime <= 0) 
            {
                reloadTime = reloadTimeReset;
                loaded = true;
            }
        }

        void FindComponents()
        {
            inputManager = MasterSingleton.Instance.InputManager;
        }
    }
}
