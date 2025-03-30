using SwordfishGame;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SwordfishGame
{
    public class HazardManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> hazardObjects = new List<GameObject>();
        [SerializeField] private List<GameObject> swordfishSpawnVariations = new List<GameObject>();
        [SerializeField] private int hazardToSwordfishWeight = 2;
        [SerializeField] private int maxHazards = 10;
        [SerializeField] private int maxSwordfishVariations = 10;
        [SerializeField] private float spawnWaitTime = 5;
        [SerializeField] private Transform spawnBounds;
        private List<GameObject> spawnedHazards = new List<GameObject>();
        private List<GameObject> spawnedSwordfishVariations = new List<GameObject>();
        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < maxHazards; i++)
            {
                Vector3 randomRot = new Vector3(0, Random.Range(0, 180), 0);

                foreach (GameObject hazard in hazardObjects)
                {
                    GameObject hazardObj = Instantiate(hazard, transform.position, Quaternion.Euler(randomRot));
                    hazardObj.transform.parent = transform;
                    hazardObj.SetActive(false);
                    spawnedHazards.Add(hazardObj);
                }
            }

            for (int i = 0; i < maxSwordfishVariations; i++)
            {
                Vector3 randomRot = new Vector3(0, Random.Range(0, 180), 0);

                foreach (GameObject variation in swordfishSpawnVariations)
                {
                    GameObject swordfishVariation = Instantiate(variation, transform.position, Quaternion.identity);
                    swordfishVariation.transform.parent = transform;
                    swordfishVariation.SetActive(false);
                    spawnedSwordfishVariations.Add(swordfishVariation);
                }
            }

            Activate(true);
        }

        public void Activate(bool activated)
        {
            if (activated) StartCoroutine(SpawnHazards());
            else
            {
                spawnedHazards.Clear();
                StopAllCoroutines();
            }
        }

        IEnumerator SpawnHazards()
        {
            while (true)
            {
                float validPosX = Random.Range(-spawnBounds.localScale.x * 0.5f, spawnBounds.localScale.x * 0.5f);
                float validPosY = spawnBounds.position.y;
                float validPosZ = 1250;
                Vector3 validPos = new Vector3(validPosX, validPosY, validPosZ);

                GameObject selectedObj;
                int hazardToSwordfishNum = Random.Range(0, hazardToSwordfishWeight);

                if (hazardToSwordfishNum == 0)
                {
                    selectedObj = spawnedSwordfishVariations[Random.Range(0, spawnedSwordfishVariations.Count)];
                    validPos.y = 0;
                }
                else selectedObj = spawnedHazards[Random.Range(0, spawnedHazards.Count)];

                if (!selectedObj.activeSelf)
                {
                    selectedObj.transform.position = validPos;
                    selectedObj.SetActive(true);
                }

                if (MasterSingleton.Instance.PlayerController.IsAlive()) yield return new WaitForSeconds(spawnWaitTime);
                else Activate(false);
            }
        }
    }
}
