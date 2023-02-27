using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordfishGame
{
    public class Bullet : MonoBehaviour
    {
        int bulletSpeed = 50;
        Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();   
        }

        void Update()
        {
            Move();
        }

        public void Reset()
        {
            //Activated(false);
        }

        public void Activated(bool activated) 
        {
            if (activated)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        public void Fired()
        {

        }

        void Move()
        {
            // Moves player
            rb.velocity = gameObject.transform.forward * bulletSpeed;
        }
    }
}
