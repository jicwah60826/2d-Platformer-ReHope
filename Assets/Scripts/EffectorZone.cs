using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectorZone : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //play effector zone SFX
            audioManager.PlaySFX(audioManager.effectorZone);
        }
    }
}
