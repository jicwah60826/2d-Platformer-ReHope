using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [Header("Movement Particle")]
    [SerializeField] ParticleSystem movementParticle;

    [Range(0, 10)]
    //speed the player must reach for the dust to begin emitting
    [SerializeField] int occurAfterVelocity;

    [Range(0, 0.2f)]
    [SerializeField] float dustFormationPeriod;

    [SerializeField] private Rigidbody2D thePlayerRB;

    private float counter;

    private bool isOnGround;

    [Header("")]
    [SerializeField] ParticleSystem fallParticle;
    [SerializeField] ParticleSystem touchParticle;
    [SerializeField] ParticleSystem playerDeathParticle;

    AudioManager audioManager;

    private void Awake()
    {
        //audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        touchParticle.transform.parent = null;
        fallParticle.transform.parent = null;
        playerDeathParticle.transform.parent = null;
    }

    private void Update()
    {
        counter += Time.deltaTime;

        if(isOnGround && Mathf.Abs(thePlayerRB.velocity.x) > occurAfterVelocity)
        {
            if(counter > dustFormationPeriod)

            {
                movementParticle.Play();
                counter = 0;
            }
        }
    }

    public void PlayTouchParticle(Vector2 pos)
    {
        touchParticle.transform.position = pos;
        AudioManager.instance.PlaySFX(audioManager.fall);
        audioManager.PlaySFX(audioManager.wallTouch);
        touchParticle.Play();
        //Debug.Log("PlayTouchParticle");
    }

    public void PlayFallParticle(Vector2 pos)
    {
        
        fallParticle.transform.position = pos;
        audioManager.PlaySFX(audioManager.fall);
        fallParticle.Play();
        //Debug.Log("PlayFallParticle");
    }

    public void PlayDeathParticle(Vector2 pos)
    {
        playerDeathParticle.transform.position = pos;
        audioManager.PlaySFX(audioManager.death);
        playerDeathParticle.Play();
        //Debug.Log("PlayDeathParticle");
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground")){
            isOnGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }
}
