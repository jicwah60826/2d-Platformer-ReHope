using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour

{

    private RespawnController respawnController;

    public Transform respawnPoint; 

    private SpriteRenderer spriteRenderer;
    public Sprite passive, active;
    Collider2D coll;

    AudioManager audioManager;

    private void Awake()
    {
        respawnController = GameObject.FindGameObjectWithTag("Player").GetComponent<RespawnController>();

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        coll = GetComponent<Collider2D>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            respawnController.UpdateCheckPoint(respawnPoint.position);

            //play checkpoint SFX
            audioManager.PlaySFX(audioManager.checkPoint);

            //change the spriter to the active sprite
            spriteRenderer.sprite = active;
            //disable the collider on this object so that we cannot pass back through it again
            coll.enabled = false;
        }
    }
}
