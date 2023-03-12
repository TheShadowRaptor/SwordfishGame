using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace SwordfishGame
{
    public class Bullet : MonoBehaviour
    {
        public int bulletSpeed;
        public int bulletDamage;

        bool canMove = false;
        Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();

            bulletSpeed = MasterSingleton.Instance.PlayerStats.BulletSpeed;
            bulletDamage = MasterSingleton.Instance.PlayerStats.BulletDamage;
        }

        void Update()
        {
            if (canMove)
            {               
                Move();
            }
        }

        public void Reset()
        {
            rb.isKinematic = false;
        }

        public void Activated(bool activated) 
        {
            if (activated) gameObject.SetActive(true);
            else gameObject.SetActive(false);
        }

        public void Fired()
        {
            canMove = true;
        }

        void Move()
        {
            // Moves bullet
            rb.velocity = gameObject.transform.forward * bulletSpeed;
        }

        private void OnTriggerEnter(Collider other)
        {
            GameObject enemy = null;
            GameObject terrain = null;
            GameObject collidedObject = other.gameObject;

            //Debug.Log(collidedObject);

            if (other.gameObject.CompareTag("Enemy") && canMove)
            {
                canMove = false;
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
                // Hit the enemy/ Deal damage/ Stick in enemy
                enemy = collidedObject;
                enemy.GetComponent<EnemyStats>().TakeDamage(bulletDamage);
                this.gameObject.transform.parent = enemy.transform;
            }

            if (other.gameObject.CompareTag("Terrain") && canMove || other.gameObject.CompareTag("Ship") && canMove)
            {
                canMove = false;
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
                terrain = collidedObject;
                this.gameObject.transform.parent = terrain.gameObject.transform;
            }          
        }
    }
}
