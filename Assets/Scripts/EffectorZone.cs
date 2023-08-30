using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectorZone : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //play effector zone SFX
            AudioManager.instance.PlaySFX(3);
        }
    }
}
