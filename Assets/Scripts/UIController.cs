using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{

    public static UIController Instance;

    public GameObject pauseScreen;
    public string mainMenuScene;
    public GameObject quitOverlay;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //default hide pause screen
        pauseScreen.SetActive(false);
        quitOverlay.SetActive(false);
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

    public void QuitConfirm()
    {
        quitOverlay.SetActive(true);
    }

    public void QuitGame()
    {
        // Save game
        SaveSystem.instance.Save();

        //quit game
        Application.Quit();
    }

    public void QuitCancel()
    {
        quitOverlay.SetActive(false);
    }

    public void UpdateUI()
    {
        //NOT USED YET
    }

    public void PointerOver()
    {
        AudioManager.instance.PlayPointerOver();
    }

    public void PointerClick()
    {
        AudioManager.instance.PlayPointerClick();
    }

}
