using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwordfishGame
{
    public class SpearPool : MonoBehaviour
    {
        public int initialSpearCount = 20;
        [SerializeField] GameObject spearType;

        List<GameObject> spears = new List<GameObject>();
        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < initialSpearCount; i++)
            {
                GameObject spear = Instantiate(spearType);
                spears.Add(spear);
                spears[i].SetActive(false);
            }
            ParentSpearsToWeapon(MasterSingleton.Instance.WeaponController.gameObject);
            spears[0].SetActive(true);
            spears[0].GetComponent<Spear>().ParentSpear(MasterSingleton.Instance.WeaponController.gameObject);
            spears[0].GetComponent<Spear>().OrintateSpear(MasterSingleton.Instance.WeaponController.gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void ParentSpearsToWeapon(GameObject weapon)
        {
            foreach (GameObject spear in spears)
            {
                spear.transform.parent = weapon.transform;
                spear.transform.position = weapon.transform.position;
                spear.transform.rotation = weapon.transform.rotation;
                spear.SetActive(false);
            }
        }

        public void ParentSingleSpearToWeapon(GameObject weapon, GameObject spear)
        {
            spear.transform.parent = weapon.transform;
            spear.transform.position = weapon.transform.position;
            spear.transform.rotation = weapon.transform.rotation;
            spear.SetActive(false);
        }

        public void GetItem(GameObject weapon)
        {
            spears[0].GetComponent<Spear>().ParentSpear(weapon);
            spears[0].GetComponent<Spear>().OrintateSpear(weapon);
            spears[0].SetActive(true);
        }

        public void RemoveItem()
        {
            GameObject removedSpear = spears[0];
            spears.RemoveAt(0);
            spears.Add(removedSpear);
        }

        public void RetrieveSpears(GameObject weapon)
        {
            for (int i = 0; i < spears.Count; i++)
            {
                spears[i].SetActive(false);
            }
            ParentSpearsToWeapon(weapon);
            spears[0].GetComponent<Spear>().ParentSpear(weapon);
            spears[0].GetComponent<Spear>().OrintateSpear(weapon);
            spears[0].SetActive(true);
        }
    }
}
