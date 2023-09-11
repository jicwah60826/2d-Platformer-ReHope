using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FinishPoint : MonoBehaviour
{

    public int levelToLoad;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //disable the colider on the finish point
            GetComponent<BoxCollider2D>().enabled = false;

            //disable player input
            other.GetComponent<PlayerInput>().enabled = false;

            //call the method from the game manager script that does the level loading
            GameManager.GameManagerLoadLevel(levelToLoad);
        }
    }
}