using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordfishGame
{
    public class WeaponController : MonoBehaviour
    {
        // Singletons
        InputManager inputManager;
        BulletManager bulletManager;

        [Header("Weapon Settings")]
        [SerializeField] int chamberCapacity = 1;
        [SerializeField] float reloadTimer = 1;

        // Resets
        float reloadTimerReset;

        int bulletsInChamber = 0;
        bool reloaded = true;
        GameObject bullet;


        // Start is called before the first frame update
        void Start()
        {
            reloadTimerReset = reloadTimer;
            FindComponents();
        }

        // Update is called once per frame
        void Update()
        {
            bulletManager.bulletPool.FindWeapon(gameObject);
            ManageReloadingTimer();
            HandleWeapon();
        }

        void HandleWeapon()
        {
            if (inputManager.AttackInput)
            {
                if (bulletsInChamber < chamberCapacity) ReloadWeapon();
                else if (bulletsInChamber > 0) ShootWeapon();
            }
        }

        void ReloadWeapon()
        {     
            if (reloaded)
            {
                bulletManager.bulletPool.GetBullet(gameObject);
                reloaded = false;
                bulletsInChamber++;   
            }
        }

        void ManageReloadingTimer()
        {
            if (reloaded == false && bulletsInChamber == 0)
            {
                reloadTimer -= Time.deltaTime;

                if(reloadTimer <= 0)
                {
                    reloadTimer = reloadTimerReset;
                    reloaded = true;
                }
            }
            else if (reloaded)
            {
                bulletManager.bulletPool.SetBulletActives();
            }
        }

        void ShootWeapon()
        {
            bulletsInChamber--;
        }

        void FindComponents()
        {
            bulletManager = MasterSingleton.Instance.BulletManager;
            inputManager = MasterSingleton.Instance.InputManager;
        }
    }
}
