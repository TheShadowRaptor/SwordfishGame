using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SwordfishGame;

public class GameplayHud : MonoBehaviour
{
    public TextMeshProUGUI waveDir;
    public TextMeshProUGUI waveNum;
    public TextMeshProUGUI enemiesRemaining;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTextValues();
    }

    void UpdateTextValues()
    {
        if (MasterSingleton.Instance.EnemyManager.spawnWaveOnRight)
        {
            waveDir.text = $"right";
        }

        else waveDir.text = $"Left";

        waveNum.text = $"Wave: {MasterSingleton.Instance.EnemyManager.waveCounter}";
        enemiesRemaining.text = $"Swordfish: {EnemyManager.enemyAliveCounter}";
    }
}
