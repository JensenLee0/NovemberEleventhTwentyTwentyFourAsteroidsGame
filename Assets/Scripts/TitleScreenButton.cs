using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenButton : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(StartGameFromTitleScreenButton);
    }
    void StartGameFromTitleScreenButton()
    {
        SceneManager.LoadScene("THEGAME");
    }
    void Update()
    {
        if(Input.GetButtonDown("Shoot"))
        {
            StartGameFromTitleScreenButton();
        }
    }
}
