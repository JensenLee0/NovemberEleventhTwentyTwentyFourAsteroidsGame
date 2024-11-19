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
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI titleScreenDisplay;
    // Start is called before the first frame update
    void Start()
    {
        playerCurrentLifeCount = playerMaxLifeCount;
    }

    // Update is called once per frame
    void Update()
    {
        playerLifeDisplay.text = "lives: " + playerCurrentLifeCount;
        if(playerCurrentLifeCount <= 0)
        {
            GameOver();
        }
    }
    public void StartGame()
    {
        titleScreenDisplay.enabled = false;
    }
    public void GameOver()
    {
        isGameActive = false;
        gameOverText.enabled = true;
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
