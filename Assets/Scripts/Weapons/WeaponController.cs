using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordfishGame
{
    public class WeaponController : MonoBehaviour
    {
        // Singletons
        InputManager inputManager;

        [Header("Components")]
        [SerializeField] protected Animator weaponAnimator;
        [SerializeField] protected BoxCollider hitBox;

        protected int chamberAmmo = 1;

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
            CheckForSpearHitBox();
            Attack();

            if (Input.GetKey(KeyCode.R))
            {
                MasterSingleton.Instance.SpearPool.RetrieveSpears(gameObject);
            }
        }

        public void LoseChamberAmmo()
        {
            chamberAmmo -= 1;
        }

        public void ToggleHitBox(bool on)
        {
            if (on) hitBox.enabled = true;
            else hitBox.enabled = false;
        }

        void Attack()
        {
            if (chamberAmmo == 1)
            {
                //Debug.Log("CanAttack");
                // Hitboc is on weapon. Animation will play well activating hit box
                AnimatorStateInfo stateInfo = weaponAnimator.GetCurrentAnimatorStateInfo(0);
                // Chacks if animation is done playing
                if (stateInfo.IsName("AttackAnimation") && stateInfo.normalizedTime >= 1.0f)
                {
                    weaponAnimator.SetBool("isAttacking", false);
                    ToggleHitBox(false);
                }
                else
                {
                    if (inputManager.AttackInput)
                    {
                        weaponAnimator.SetBool("isAttacking", true);
                        ToggleHitBox(true);
                    }
                }
            }
            else Reload();
        }

        void Reload()
        {
            //Debug.Log("Reloading");
            weaponAnimator.SetBool("isAttacking", false);
            reloadTime -= Time.deltaTime;
            if (reloadTime <= 0) 
            {
                MasterSingleton.Instance.SpearPool.GetItem(this.gameObject);
                reloadTime = reloadTimeReset;
                chamberAmmo = 1;
            }
        }


        void CheckForSpearHitBox()
        {
            if (gameObject.transform.childCount < 0 || gameObject.transform.GetChild(0).gameObject.activeSelf == false) return;          
            if (transform.GetChild(0).gameObject.activeSelf == true) // Looks for first harpoon in children
            {
                hitBox = transform.GetChild(0).GetChild(0).GetComponent<BoxCollider>(); // Get spear's box collider 
                //Debug.Log(hitBox);
            }
        }

        void FindComponents()
        {
            inputManager = MasterSingleton.Instance.InputManager;
        }
    }
}
