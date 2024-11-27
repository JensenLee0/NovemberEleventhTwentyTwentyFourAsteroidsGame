using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(RestartGame);
    }
    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            RestartGame();
        }
    }
}
