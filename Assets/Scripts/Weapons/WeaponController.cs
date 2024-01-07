using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordfishGame
{
    public class WeaponController : MonoBehaviour
    {
        // Singletons
        InputManager inputManager;

        [Header("Weapon Settings")]
        // Shown
        [SerializeField] int chamberCapacity = 1;
        [SerializeField] float reloadTimer = 1;
        [SerializeField] Transform bulletHolder;
        [SerializeField] Animator animator;

        // Hidden
        int bulletsInChamber = 1;
        bool reloaded = true;
        bool isAiming = false;

        [Header("AmmoSettings")]
        // Shown
        [SerializeField] int maxBulletsPooled;
        [SerializeField] GameObject bulletType;
        [SerializeField] List<GameObject> bulletPool = new List<GameObject>();

        // Hidden
        GameObject bulletInChamber;

        // Resets
        float reloadTimerReset;

        // Start is called before the first frame update
        void Start()
        {
            inputManager = MasterSingleton.Instance.InputManager;
            InstantiateBullets();
            reloadTimerReset = reloadTimer;
        }

        // Update is called once per frame
        void Update()
        {
            CountReloadingTimer();
            ToggleAiming();
            ShootOrReload();
        }

        bool isJoyAttackPressed;
        void ShootOrReload()
        {
            if (!isJoyAttackPressed) 
            {
                if (inputManager.joyAttack.Pressed && isAiming)
                {
                    if (bulletsInChamber < chamberCapacity) ReloadWeapon();
                    else if (bulletsInChamber > 0) ShootWeapon();

                    animator.SetTrigger("fired");
                    isJoyAttackPressed = true;
                    isAiming = false;
                }
            }
            else if (!inputManager.joyAttack.Pressed) 
            {
                isJoyAttackPressed = false;
                ReloadWeapon();
            }
        }

        bool isJoyAimPressed;
        void ToggleAiming() 
        {
            if (!isJoyAimPressed)
            {
                if (inputManager.joyAim.Pressed)
                {
                    if (!isAiming) isAiming = true;
                    else isAiming = false;

                    isJoyAimPressed = true;
                } 
            }
            else if (!inputManager.joyAim.Pressed) 
            {
                isJoyAimPressed = false;
            }

            animator.SetBool("isAiming", isAiming);
        }

        void ReloadWeapon()
        {     
            if (reloaded)
            {
                reloaded = false;
                   
            }
        }

        void CountReloadingTimer()
        {
            if (reloaded == false && bulletsInChamber == 0)
            {
                reloadTimer -= Time.deltaTime;

                if(reloadTimer <= 0)
                {
                    reloadTimer = reloadTimerReset;
                    bulletsInChamber++;
                    reloaded = true;
                    PullBullet();
                }
            }
        }

        void ShootWeapon()
        {
            bulletInChamber.GetComponent<Bullet>().isFired = true;
            bulletInChamber.transform.parent = null;
            bulletsInChamber--;
        }

        // BulletPool
        void InstantiateBullets() 
        {
            for (int i = 0; i < maxBulletsPooled; i++) 
            {
                GameObject newBullet = Instantiate(bulletType);
                newBullet.GetComponent<Bullet>().ResetBulletToWeapon(bulletHolder);
                newBullet.transform.localScale = new Vector3(100, 100, 100);
                bulletPool.Add(newBullet);
            }

            bulletPool[0].SetActive(true);
            bulletInChamber = bulletPool[0];
        }

        void PullBullet() 
        {
            GameObject bullet = bulletPool[0];
            bullet.GetComponent<Bullet>().ResetBulletToWeapon(bulletHolder);
            bullet.SetActive(true);
            bulletInChamber = bullet;
            bulletPool.RemoveAt(0);
            bulletPool.Add(bullet);
        }

        public void ResetBullets()
        {
            foreach (GameObject bullet in bulletPool) 
            {
                bullet.GetComponent<Bullet>().ResetBulletToWeapon(bulletHolder);
            }
        }
    }
}
