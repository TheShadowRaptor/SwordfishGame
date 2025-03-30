using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SwordfishGame;
using UnityEngine.UI;

public class GameplayHud : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI shipHealth;
    public TextMeshProUGUI scoreTextMesh;
    public TextMeshProUGUI finalScore;

    private void Start()
    {
        UpdateShipHealth(3);
        AddScore(0);
    }

    public void UpdateShipHealth(int health)
    {
        shipHealth.text = "Health: " + health.ToString();
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreTextMesh.text = "Score: " + score.ToString();
        finalScore.text = "Final Score " + score.ToString();
    }

    public void ResetScore() 
    {
        score = 0;
    }
}
