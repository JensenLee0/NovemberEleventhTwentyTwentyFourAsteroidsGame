using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Player Life")]
    public int playerCurrentLifeCount;
    public int playerMaxLifeCount;
    public TextMeshProUGUI playerLifeDisplay;
    [Header("Score")]
    public int score;
    public TextMeshProUGUI scoreDisplay;
    [Header("Game")]
    public bool isGameActive;
    // Start is called before the first frame update
    void Start()
    {
        playerCurrentLifeCount = playerMaxLifeCount;
    }

    // Update is called once per frame
    void Update()
    {
        playerLifeDisplay.text = "lives: " + playerCurrentLifeCount;
    }
    public void GameOver()
    {
        isGameActive = false;
    }
    public void UpdateScore()
    {
        scoreDisplay.text = "score: " + score;
    }
    public void addScore(int scoreToAdd)
    {
        score = score + scoreToAdd;
        UpdateScore();
    }
}
