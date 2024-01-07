using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SwordfishGame;
using UnityEngine.UI;

public class GameplayHud : MonoBehaviour
{
    public TextMeshProUGUI waveDir;
    public TextMeshProUGUI waveNum;
    public TextMeshProUGUI enemiesRemaining;
    public TextMeshProUGUI shipHealth;

    public Image canLean;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // UpdateTextValues();
        // UpdateImages();
    }

    void UpdateTextValues()
    {
        if (MasterSingleton.Instance.GameManager.gameState != GameManager.GameState.gameplay) return;
        if (MasterSingleton.Instance.EnemyManager.spawnWaveOnRight)
        {
            waveDir.text = $"right";
        }

        else waveDir.text = $"Left";

        waveNum.text = $"Wave: {MasterSingleton.Instance.EnemyManager.waveCounter}";
        enemiesRemaining.text = $"Swordfish: {EnemyManager.enemyAliveCounter}";
        shipHealth.text = $"Ship Health: {ShipStats.Instance.Health}";
    }

    //void UpdateImages()
    //{
    //    if (MasterSingleton.Instance.GameManager.gameState != GameManager.GameState.gameplay) return;
    //}
}
