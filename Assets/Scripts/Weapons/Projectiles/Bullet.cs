using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordfishGame
{
    public class Bullet : MonoBehaviour
    {
        public int bulletSpeed = 10;
        public bool isFired = false;

        int bulletDamage = 1;
        bool hitTarget = false;

        void FixedUpdate()
        {
            Move();
        }

        public void ResetBulletToWeapon(Transform bulletHolder)
        {
            hitTarget = false;
            gameObject.transform.position = bulletHolder.position;
            gameObject.transform.rotation = bulletHolder.rotation;
            gameObject.transform.parent = bulletHolder.transform;
            isFired = false;
            gameObject.SetActive(false);
        }

        void Move()
        {
            if (!isFired) return;
            if (hitTarget) return;

            // Moves bullet
            Debug.Log("Bullet Moving");
            transform.Translate(Vector3.down * bulletSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy")) 
            {
                other.GetComponent<EnemyStats>().TakeDamage(bulletDamage);
                hitTarget = true;
            }
            else if (other.gameObject.CompareTag("Ship")) 
            {
                Debug.Log("HitTarget");
                hitTarget = true;
            }
        }
    }
}
