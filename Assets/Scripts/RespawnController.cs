using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class RespawnController : MonoBehaviour
{

    private Vector2 checkPoint;

    private SpriteRenderer spriteRenderer;

    private Rigidbody2D playerRB;

    public ParticleController particleController;

    private MovementController movementController;

    private ShadowCaster2D shadowCaster;

    private BoxCollider2D boxCollider;

    [SerializeField] private float waitToRespawn;

    private void Awake()
    {
        //find the Sprite Rendered on this gameobject
        spriteRenderer = GetComponent<SpriteRenderer>();

        //Find the RigidBody2D on this game object
        playerRB = GetComponent<Rigidbody2D>();

        movementController = GetComponent<MovementController>();

        shadowCaster = GetComponent<ShadowCaster2D>();

        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //store the players starting position in the startPos Vector2;
        checkPoint = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If Player hits obstacle

        if (other.gameObject.tag == "Obstacle")
        {
            //Debug.Log("Invoking Kill Player method");
            KillPlayer();
        }
    }

    public void KillPlayer()
    {
        StartCoroutine(Respawn(waitToRespawn));
        Debug.Log("Respawn function invoked via KillPlayer method");
    }

    public void UpdateCheckPoint(Vector2 pos)
    {
        checkPoint = pos;
    }

    void UpdateSaveSystem()
    {
        PlayerStats stats = PlayerStats.instance;

        SaveSystem.instance.activeSave.deathCount = stats.deathCount;
    }

IEnumerator Respawn(float duration)
    {


        //De-activate RigidBody 2D
        playerRB.simulated = false;

        Debug.Log("UpdateSaveSystem invoked via Respawn CoRoutine");

        // Play Death Particles
        particleController.PlayDeathParticle(transform.position);

        //disable the sprite renderer
        spriteRenderer.enabled = false;

        //disable shadow caster
        shadowCaster.enabled = false;

        //disable box collider
        boxCollider.enabled = false;


        //immediately freeze the position so player stops moving
        playerRB.constraints = RigidbodyConstraints2D.FreezePositionX;
        
        //stop all velocity for the player
        playerRB.velocity = new Vector2(0,0);

        //Store PlayerStats as reference so we can access the UpdateDeathCount method
        PlayerStats stats = PlayerStats.instance;
        stats.UpdateDeathCount();

        //Update the Save System
        UpdateSaveSystem();

        // Save Data to Disk
        SaveSystem.instance.Save();

        yield return new WaitForSeconds(duration);
        
        //after wait time has passed, reset the player position to the startPos
        transform.position = checkPoint;

        //re-enable the sprite renderer
        spriteRenderer.enabled = true;

        //re-enable shadow caster
        shadowCaster.enabled = true;

        //re-enable box collider
        boxCollider.enabled = true;

        //un freeze all contraints on the rigidbody2D
        playerRB.constraints = RigidbodyConstraints2D.None;
        
        // freeze the Z rotation again
        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;

        //check if player has been rotated. If so, reset to 0;
        if(movementController.gameObject.transform.rotation.y < 0)
        {
            //Debug.Log("Player is rotated -180!!!");
            movementController.transform.Rotate(0f, 180f, 0f); //THIS WORKS
            movementController.UpdateRelativeTransform();
        }

        //Re-enable RigidBody 2D
        playerRB.simulated = true;
    }

}
