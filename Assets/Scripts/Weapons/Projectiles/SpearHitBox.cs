using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordfishGame
{
    public class SpearHitBox : MonoBehaviour
    {
        [SerializeField] Spear spear;
        bool hit;

        public bool Hit { get => hit; }

        // Start is called before the first frame update
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                spear.ParentSpear(other.gameObject);
                MasterSingleton.Instance.WeaponController.LoseChamberAmmo();
                MasterSingleton.Instance.SpearPool.RemoveItem();
            }
        }
    }
}
