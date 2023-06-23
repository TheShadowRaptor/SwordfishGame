using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace SwordfishGame
{
    public class WeaponController : MonoBehaviour
    {
        // Singletons
        InputManager inputManager;

        [Header("Components")]
        [SerializeField] protected Animator weaponAnimator;
        [SerializeField] protected GameObject harpoonTip;

        public bool tipLoaded = true;

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
            DeactivateHarpoonTip();
        }

        private bool attackButtonPressed = false;
        void Attack()
        {
            if (tipLoaded)
            {
                AnimatorStateInfo stateInfo = weaponAnimator.GetCurrentAnimatorStateInfo(0);
                if (stateInfo.IsName("AttackAnimation") && stateInfo.normalizedTime >= 1.0f)
                {
                    weaponAnimator.SetBool("isAttacking", false);
                }
                else
                {
                    if (inputManager.AttackInput && !attackButtonPressed)
                    {
                        weaponAnimator.SetBool("isAttacking", true);
                        attackButtonPressed = true;
                    }
                    else if (!inputManager.AttackInput)
                    {
                        attackButtonPressed = false;
                    }
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
            weaponAnimator.SetBool("isAttacking", false);
            reloadTime -= Time.deltaTime;
            if (reloadTime <= 0) 
            {
                reloadTime = reloadTimeReset;
                tipLoaded = true;
            }
        }

        void FindComponents()
        {
            inputManager = MasterSingleton.Instance.InputManager;
        }

        void DeactivateHarpoonTip()
        {
            harpoonTip.gameObject.SetActive(tipLoaded);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                tipLoaded = false;
            }
        }
    }
}
