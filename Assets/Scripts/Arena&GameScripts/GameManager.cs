using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Player Stats")]
    public int playerCurrentLifeCount;
    public int playerMaxLifeCount;
    public TextMeshProUGUI playerLifeDisplay;
    public TextMeshProUGUI shieldDisplay;
    public TextMeshProUGUI missileAmmoDisplay;
    [Header("Score")]
    public int score;
    public TextMeshProUGUI scoreDisplay;
    [Header("Game")]
    public bool isGameActive;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    [Header("Connections")]
    public PlayerControl playerScript;
    public SpawnManager spawnManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerControl>();
        spawnManagerScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        StartGameInGameManager();
        spawnManagerScript.startGameSpawnManager();
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        playerLifeDisplay.text = "lives: " + playerCurrentLifeCount;
        shieldDisplay.text = "shield charge: " + playerScript.shieldCharge;
        missileAmmoDisplay.text = "missiles: " + playerScript.missileAmmo;
        if(playerCurrentLifeCount < 0)
        {
            playerCurrentLifeCount = 0;
        }
    }
    public void StartGameInGameManager()
    {
        playerCurrentLifeCount = playerMaxLifeCount;
        score = 0;
        UpdateScore();
    }
    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
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
