using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SwordfishGame
{
    public class EnemyManager : MonoBehaviour
    {
        public List<int> wave = new List<int>();
        public List<int> waveEnemyCount = new List<int>();

        private List<GameObject> selectedEnemyTargets = new List<GameObject>();

        public int enemyPoolSize = 10;
        public GameObject enemyPrefab;

        private List<GameObject> enemyPool;
        private Queue<int> spawnQueue;

        public float spawnInterval = 1.0f;
        public int enemiesPerWave = 10;

        private int waveIndex = 0;
        private int enemiesSpawned = 0;

        public List<GameObject> SelectedEnemyTargets { get => selectedEnemyTargets; }

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

            // Initialize spawn queue
            spawnQueue = new Queue<int>();
            for (int i = 0; i < wave.Count; i++)
            {
                for (int j = 0; j < waveEnemyCount[i]; j++)
                {
                    spawnQueue.Enqueue(wave[i]);
                }
            }          
        }

        private void Update()
        {
            if (MasterSingleton.Instance.GameManager.gameState != GameManager.GameState.gameplay) return;

            if (enemiesSpawned == 0)
            {
                // Start enemy spawning coroutine
                StartCoroutine(SpawnEnemies());
            }
        }

        IEnumerator SpawnEnemies()
        {
            while (true)
            {
                // Check if wave is complete
                if (enemiesSpawned >= enemiesPerWave)
                {
                    // Wait for next wave
                    yield return new WaitForSeconds(spawnInterval * 5);
                    enemiesSpawned = 0;
                    spawnQueue.Clear();

                    // Reinitialize spawn queue for new wave
                    for (int i = 0; i < waveEnemyCount[waveIndex]; i++)
                    {
                        spawnQueue.Enqueue(wave[waveIndex]);
                    }
                }

                // Check if there are enemies left to spawn
                if (spawnQueue.Count == 0)
                {
                    yield return new WaitForSeconds(spawnInterval);
                    continue;
                }

                // Spawn next enemy from object pool
                GameObject enemy = GetNextEnemyFromPool();
                enemy.transform.position = GetSpawnPosition();
                enemy.GetComponent<EnemyController>().targets = SetTargets();
                enemy.GetComponent<EnemyStats>().isAlive = true;
                enemy.SetActive(true);
                enemiesSpawned++;

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
            if (enemiesSpawned == 0)
            {
                bool spawnOnRight = Random.value > 0.5f;
                if (spawnOnRight)
                {
                    spawnPosition = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z + 80);
                }
                else
                {
                    spawnPosition = new Vector3(transform.position.x - 10, transform.position.y, transform.position.z + 80);
                }
            }

            return spawnPosition;
        }

        private List<GameObject> SetTargets()
        {
            if (spawnPosition.x > 0) selectedEnemyTargets = EnemyTarget.rightTargets;
            else if (spawnPosition.x < 0) selectedEnemyTargets = EnemyTarget.leftTargets;
            return selectedEnemyTargets;
        }
    }
}