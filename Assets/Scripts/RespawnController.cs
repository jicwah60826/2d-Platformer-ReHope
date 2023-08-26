using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class RespawnController : MonoBehaviour
{

    private Vector2 startPos;

    private Vector3 startRot;


    private SpriteRenderer spriteRenderer;

    private Rigidbody2D playerRB;

    public ParticleController particleController;

    public MovementController movementController;

    [SerializeField] private float waitToRespawn;

    private void Awake()
    {
        //find the Sprite Rendered on this gameobject
        spriteRenderer = GetComponent<SpriteRenderer>();

        //Find the RigidBody2D on this game object
        playerRB = GetComponent<Rigidbody2D>();

        movementController = GetComponent<MovementController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //store the players starting position in the startPos Vector2;
        startPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If Player hits obstacle
        if (other.CompareTag("Obstacle"))
        {

            KillPlayer();
        }
    }

    public void KillPlayer()
    {
        // Play Death Particles
        particleController.PlayDeathParticle(transform.position);

        StartCoroutine(Respawn(waitToRespawn));
    }

IEnumerator Respawn(float duration)
    {
        //disable the sprite renderer
        spriteRenderer.enabled = false;

        //immediately freeze the position so player stops moving
        playerRB.constraints = RigidbodyConstraints2D.FreezePositionX;
        
        //stop all velocity for the player
        playerRB.velocity = new Vector2(0,0);

        yield return new WaitForSeconds(duration);
        
        //after wait time has passed, reset the player position to the startPos
        transform.position = startPos;

        ////reset player rotation
        //Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
        //transform.rotation = Quaternion.Euler(rotator);

        //re-enable the sprite renderer
        spriteRenderer.enabled = true;
        
        //un freeze all contraints on the rigidbody2D
        playerRB.constraints = RigidbodyConstraints2D.None;
        
        // freeze the Z rotation again
        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

}
