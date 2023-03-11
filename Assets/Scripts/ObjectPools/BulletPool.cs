using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Mathematics;
using UnityEngine;

namespace SwordfishGame
{
    public class BulletPool : MonoBehaviour
    {
        public int initialCapacity = 3;
        public GameObject bulletType;
        public List<GameObject> bullets = new List<GameObject>();

        public GameObject weapon;

        private void Start()
        {
            BulletPoolInstantiation();
        }

        public void BulletPoolInstantiation()
        {
            for (int i = 0; i < initialCapacity; i++)
            {
                GameObject bullet = Instantiate(bulletType, weapon.transform.parent);
                bullets.Add(bullet);
                bullets[i].SetActive(false);
            }
        }

        public void LoadInWeapon()
        {
            // Line bullet up to gun muzzle
            Vector3 weaponPos = weapon.transform.position;
            quaternion weaponRot = weapon.transform.rotation;

            bullets[0].transform.parent = weapon.transform.parent;
            bullets[0].transform.position = weaponPos;
            bullets[0].transform.rotation = weaponRot;
        }

        public void SetBulletActives()
        {
            bullets[0].SetActive(true);
            bullets[1].SetActive(false);
        }

        public void GetBullet(GameObject weapon)
        {
            if (bullets.Count > 0)
            {
                // Shoot bullet from gun muzzle
                Vector3 weaponPos = weapon.transform.position;
                quaternion weaponRot = weapon.transform.rotation;

                bullets[0].transform.parent = null;
                bullets[0].transform.position = weaponPos;
                bullets[0].transform.rotation = weaponRot;

                bullets[0].GetComponent<Bullet>().Fired();

                ReturnBullet(bullets[0]);
                bullets.RemoveAt(0);
            }
            else
            {
                // If the pool is empty, create a new bullet
                bulletType = new GameObject();
            }
        }

        public void ReturnBullet(GameObject bullet)
        {
            // Sets bullet.SetActivate to false
            bullet.GetComponent<Bullet>().Reset();
            bullets.Add(bullet);
        }
    }
}
