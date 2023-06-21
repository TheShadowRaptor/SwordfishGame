using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SwordfishGame
{
    public class EnemyManager : MonoBehaviour
    {
        public static int enemyAliveCounter;

        public int wave;
        public int waveCounter;
        public int waveEnemyCount;

        public int enemyPoolSize = 10;
        public GameObject enemyPrefab;

        private List<GameObject> enemyPool;

        public float spawnInterval = 1.0f;
        public int enemiesPerWave = 10;

        public int enemiesSpawned = 0;

        public bool spawnWaveOnRight;

        void Start()
        {
            // Create enemy object pool
            enemyPool = new List<GameObject>();
            for (int i = 0; i < enemyPoolSize; i++)
            {
                GameObject enemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
                enemy.transform.parent = this.transform;
                enemy.SetActive(false);
                enemyPool.Add(enemy);
            } 
        }

        private bool sideChosen = false;

        [ContextMenu("SpawnWave")]
        public void SpawnEnemyWave()
        {
            if (MasterSingleton.Instance.GameManager.gameState != GameManager.GameState.gameplay) return;

            waveCounter++;
            // Start enemy spawning coroutine
            StartCoroutine(SpawnEnemies());
        }

        void EndEnemyWave()
        {
            StopCoroutine(SpawnEnemies());
        }

        IEnumerator SpawnEnemies()
        {
            while (true)
            {
                if (!sideChosen)
                {
                    spawnWaveOnRight = Random.value > 0.5f;
                    sideChosen = true;
                }

                // Spawn next enemy from object pool
                GameObject enemy = GetNextEnemyFromPool();
                enemy.transform.position = GetSpawnPosition();
                enemy.GetComponent<EnemyController>().target = SetTargets();
                enemy.GetComponent<EnemyStats>().InitStats(1);
                enemy.GetComponent<EnemyStats>().Spawn();
                enemy.SetActive(true);
                enemiesSpawned++;

                if (enemiesSpawned >= enemiesPerWave)
                {
                    enemiesSpawned = 0;
                    sideChosen = false;
                    EndEnemyWave();
                    break;
                }
                Debug.Log("EnemySpawned");
                // Wait for next spawn interval
                yield return new WaitForSeconds(Random.Range(spawnInterval, spawnInterval * 2));
            }
        }

        private GameObject GetNextEnemyFromPool()
        {
            for (int i = 0; i < enemyPool.Count; i++)
            {
                if (!enemyPool[i].activeInHierarchy)
                {
                    return enemyPool[i];
                }
            }

            // If no inactive enemy found in pool, create a new one
            GameObject newEnemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
            enemyPool.Add(newEnemy);
            return newEnemy;
        }

        private Vector3 spawnPosition = Vector3.zero;

        private Vector3 GetSpawnPosition()
        {
            int rightLocation;
            int leftLocation;
            if (spawnWaveOnRight)
            {
                rightLocation = Random.Range(100, 200);
                spawnPosition = new Vector3(transform.position.x + rightLocation, transform.position.y, transform.position.z + 100);
                Debug.Log($"rightLocation | {rightLocation}");
            }
            else
            {
                leftLocation = Random.Range(100, 200);
                spawnPosition = new Vector3(transform.position.x - leftLocation, transform.position.y, transform.position.z + 100);
                Debug.Log($"LeftLocation | {leftLocation}");
            }

            return spawnPosition;
        }

        private GameObject SetTargets()
        {
            int num = 0;
            GameObject selectedEnemyTarget = null;
            if (spawnPosition.x >= 0) // right
            {
                num = Random.Range(0, EnemyTarget.rightTargets.Count);
                Debug.Log(num);
                selectedEnemyTarget = EnemyTarget.rightTargets[num];
            }

            else if (spawnPosition.x < 0) // left
            {
                num = Random.Range(0, EnemyTarget.leftTargets.Count);
                Debug.Log(num);
                selectedEnemyTarget = EnemyTarget.leftTargets[num];
            }

            return selectedEnemyTarget;
        }
    }
}