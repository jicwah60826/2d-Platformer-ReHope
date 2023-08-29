using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FinishPoint : MonoBehaviour
{

    public Fader fader;
    public int levelToLoad;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //disable the colider on the finish point
            GetComponent<BoxCollider2D>().enabled = false;

            //disable player input
            other.GetComponent<PlayerInput>().enabled = false;

            fader.SetLevel(levelToLoad);
        }
    }
}