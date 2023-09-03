using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{

    private RespawnController respawnController;

    private void Awake()
    {
        respawnController = FindObjectOfType<RespawnController>();
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {

        // If Player hits obstacle
        if (other.CompareTag("Player"))
        {

            respawnController.KillPlayer();
            Debug.Log("KillPlayer invoked via killzone");
        }
    }
}
