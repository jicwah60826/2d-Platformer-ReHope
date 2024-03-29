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

    [SerializeField] private FlashImage _flashImage = null;
    [SerializeField] private Color _flashColor = Color.white;
    [SerializeField] private float _flashTime, _flashMinAlpha, _flashMmaxAlpha;

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
            KillPlayer();
        }
    }

    public void KillPlayer()
    {
        _flashImage.Flash(_flashTime, _flashMinAlpha, _flashMmaxAlpha, _flashColor);

        StartCoroutine(Respawn(waitToRespawn));
    }

    public void UpdateCheckPoint(Vector2 pos)
    {
        checkPoint = pos;
    }

    void UpdateSaveSystem()
    {
        PlayerStats stats = PlayerStats.instance;
    }

IEnumerator Respawn(float duration)
    {


        //De-activate RigidBody 2D
        playerRB.simulated = false;

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
            movementController.transform.Rotate(0f, 180f, 0f); //THIS WORKS
            movementController.UpdateRelativeTransform();
        }

        //Re-enable RigidBody 2D
        playerRB.simulated = true;
    }

}
