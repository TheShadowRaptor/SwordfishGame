using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordfishGame
{
    public class BulletPool : MonoBehaviour
    {
        public int initialCapacity = 3;
        public GameObject bulletType;
        public List<GameObject> bullets = new List<GameObject>();

        private void Start()
        {
            BulletPoolInstantiation();
        }

        public void BulletPoolInstantiation()
        {
            for (int i = 0; i < initialCapacity; i++)
            {
                GameObject bullet = Instantiate(bulletType);
                bullets.Add(bullet);
                bullets[i].SetActive(false);
            }
        }

        public void FindWeapon(GameObject weapon)
        {
            bullets[0].transform.parent = weapon.transform.parent;
            bullets[0].transform.position = weapon.transform.position;
            bullets[0].transform.rotation = weapon.transform.rotation;
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
                // Get the first bullet from the list             
                bullets[0].transform.parent = null;
                bullets[0].transform.position = weapon.transform.position;
                bullets[0].transform.rotation = weapon.transform.rotation;
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
