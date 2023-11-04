using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SwordfishGame
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private GameObject swordFishEnemy;
        [SerializeField] private List<GameObject> swordFishEnemyList = new List<GameObject>();

        [Header("SpawnSettings")]
        [SerializeField] private Vector3 leftSpawnAreaPosition;
        [SerializeField] private Vector3 rightSpawnAreaPosition;
        [SerializeField] private Vector3 spawnAreaSize;
        [SerializeField] private float timeUntillSpawn;
        [SerializeField] private List<int> enemiesPerWave = new List<int>();

        private int waveNum = 0;

        public void StartWave()
        {
            StartCoroutine(SpawnEnemies());       
        }

        public IEnumerator SpawnEnemies() 
        {
            int dir = Random.Range(0,1);
            Vector3 finalSpawnPos = Vector3.zero;
            for (int i = 0; i < enemiesPerWave[waveNum]; i++)
            {
                if (dir == 0) 
                {
                    // Left
                    float randomXSpawnPos = Random.Range(0, spawnAreaSize.x);
                    float randomYSpawnPos = Random.Range(0, spawnAreaSize.y);
                    float randomZSpawnPos = Random.Range(0, spawnAreaSize.z);

                    finalSpawnPos = new Vector3(leftSpawnAreaPosition.x + randomXSpawnPos,
                        leftSpawnAreaPosition.y + randomYSpawnPos,
                        leftSpawnAreaPosition.z + randomZSpawnPos);

                }
                else 
                {
                    // Right
                    float randomXSpawnPos = Random.Range(0, spawnAreaSize.x);
                    float randomYSpawnPos = Random.Range(0, spawnAreaSize.y);
                    float randomZSpawnPos = Random.Range(0, spawnAreaSize.z);

                    finalSpawnPos = new Vector3(rightSpawnAreaPosition.x + randomXSpawnPos,
                        rightSpawnAreaPosition.y + randomYSpawnPos,
                        rightSpawnAreaPosition.z + randomZSpawnPos);
                }

                GameObject enemy = Instantiate(swordFishEnemy, finalSpawnPos, Quaternion.identity);

                yield return new WaitForSeconds(timeUntillSpawn);
            }
            yield return null;
        }
    }
}