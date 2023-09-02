using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Microsoft.Unity.VisualStudio.Editor;

public class UIController : MonoBehaviour
{

    public GameObject pauseScreen;

    public string mainMenuScene;

    private void Start()
    {
        //default hide pause screen
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }


    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
        Time.timeScale = 1f;
    }

    public void PauseUnpause()
    {
        if (pauseScreen.activeSelf == false)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;

        }
        else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
