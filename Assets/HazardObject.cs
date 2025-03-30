using SwordfishGame;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

namespace SwordfishGame
{
    public class HazardObject : MonoBehaviour
    {
        [SerializeField] protected float activeTime = 5;
        [SerializeField] protected int damage = 5;
        
        protected float speed;
        protected float initActiveTime;

        // Get/Sets
        public int Damage { get => damage; }

        private void Awake()
        {
            initActiveTime = activeTime;
        }

        private void OnEnable()
        {
            activeTime = initActiveTime;
        }

        void FixedUpdate()
        {
            HazardSpeed();
            transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);

            if (speed <= 0) return;

            activeTime -= Time.deltaTime;
            if (activeTime <= 0) gameObject.SetActive(false);
        }

        protected virtual void HazardSpeed()
        {
            speed = MasterSingleton.Instance.PlayerController.ShipSpeed;
        }
    }
}
