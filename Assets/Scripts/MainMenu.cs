using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public int levelToLoad;

    private void Start()
    {
        AudioManager.instance.PlayMainMenuMusic();
    }

    public void PointerOver()
    {
        AudioManager.instance.PlayPointerOver();
    }
    
    public void PointerClick()
    {
        AudioManager.instance.PlayPointerClick();
    }


    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
        AudioManager.instance.PlayLevelMusic();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        StartCoroutine(ContinueCo());
    }

    IEnumerator ContinueCo()
    {
        // Load from the save system on disk
        SaveSystem.instance.Load();

        yield return new ();

        //load the current level from the save data
        SceneManager.LoadScene(SaveSystem.instance.activeSave.currentLevel);

        AudioManager.instance.PlayLevelMusic();
    }
}
