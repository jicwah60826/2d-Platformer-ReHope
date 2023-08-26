using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    Rigidbody2D theRB;

    [SerializeField] int speed;

    [Range(1f, 10f)]
    [SerializeField] float acceleration;

    [SerializeField] float gravity;

    private float speedMultiplier;

    private bool btnPressed;

    private bool isTouchingWall;

    public LayerMask whatIsWall;
    public Transform wallCheckPoint;

    private Vector2 relativeTransform;

    public bool isOnPlatform;
    public Rigidbody2D platformRB;

    public ParticleController particleController;

    private void Awake()
    {
        theRB = GetComponent<Rigidbody2D>();

        theRB.gravityScale = gravity;
    }

    private void Start()
    {
        UpdateRelativeTransform();
    }

    private void FixedUpdate()
    {

        UpdateSpeedMultiplier();

        float targetSpeed = speed * speedMultiplier * relativeTransform.x;

        if(isOnPlatform)
        {
            theRB.velocity = new Vector2(targetSpeed + platformRB.velocity.x, theRB.velocity.y);
        }
        else
        {
            theRB.velocity = new Vector2(targetSpeed, theRB.velocity.y);
        }


        isTouchingWall = Physics2D.OverlapBox(wallCheckPoint.position, new Vector2(0.03f, 0.3f), 0f, whatIsWall);

        if (isTouchingWall)
        {
            Flip();
        }
    }

    public void Flip()
    {
        particleController.PlayTouchParticle(wallCheckPoint.position);
        transform.Rotate(0f, 180f, 0f);
        UpdateRelativeTransform();
    }

    void UpdateRelativeTransform()
    {
        relativeTransform = transform.InverseTransformVector(Vector2.one);
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            btnPressed = true;
        }

        else if (ctx.canceled)
        {
            btnPressed  = false;
        }

    }

    private void UpdateSpeedMultiplier()
    {
        if (btnPressed && speedMultiplier < 1f)
        {
            speedMultiplier += Time.deltaTime * acceleration;
        }

        else if(!btnPressed && speedMultiplier > 0f)

        {
            speedMultiplier -= Time.deltaTime * acceleration;

            if(speedMultiplier < 0f)
            {
                speedMultiplier = 0f;
            }
        }
    }


}
