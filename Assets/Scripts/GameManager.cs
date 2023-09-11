using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager gmInstance;

    private Fader fader;

    private void Awake()
    {
        if (gmInstance == null)
        {
            gmInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void RegisterFader(Fader fD)
    {
        if(gmInstance == null)
        {
            return;
        }
        else
        {
            gmInstance.fader = fD;
        }
    }
    

    public static void GameManagerLoadLevel(int sceneIndex)
    {
        //if game manager is null - then cancel. Otherwise - call SetLevel method
        if (gmInstance == null)
            return;

        //call the set level function from the fader script
        gmInstance.fader.SetLevel(sceneIndex);
    }

}
