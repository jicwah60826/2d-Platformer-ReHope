using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{

    [SerializeField] private Transform destination; //other portal
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            if(Vector2.Distance(player.transform.position, transform.position) > 0.3)
            {
                // Only teleport player once within the target distance from the portal
                player.transform.position = destination.transform.position;
            }
            
        }
    }
}
