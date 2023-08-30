using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class PortalController : MonoBehaviour
{

    [SerializeField] private Transform destination; //other portal

    private GameObject player;

    private Animation anim;

    private Rigidbody2D playerRB;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animation>();
        playerRB = player.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            if(Vector2.Distance(player.transform.position, transform.position) > 0.3)
            {
                StartCoroutine(PortalIn());
            }
            
        }
    }

    IEnumerator PortalIn()
    {

        //play portal in SFX here
        AudioManager.instance.PlaySFX(8);

        // disable RB as soon as player touched portal
        playerRB.simulated = false;

        //play the portal in animation
        anim.Play("Portal_IN");

        //trigger move into portal
        StartCoroutine(MoveIntoPortal());

        //wait
        yield return new WaitForSeconds(0.5f);

        // Only teleport player once within the target distance from the portal
        player.transform.position = destination.transform.position;

        //reset the velocity of the player so that we don't come out of portals with same velocity as we went in
        playerRB.velocity = Vector2.zero;

        //play portal out SFX here

        // play the portal out animation
        anim.Play("Portal_OUT");

        //wait
        yield return new WaitForSeconds(0.5f);

        //re-activate RB component
        playerRB.simulated = true;
    }

    IEnumerator MoveIntoPortal()
    {
        float timer = 0;

        while (timer < 0.5f)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, transform.position, 3 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
    }
}
