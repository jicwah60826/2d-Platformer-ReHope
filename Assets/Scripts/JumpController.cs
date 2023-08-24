using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{

    Rigidbody2D theRB;

    [SerializeField] private int jumpPower;

    [SerializeField] private float fallMulitplier;


    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    private bool isOnGround;

    private Vector2 vecGravity;

    // Start is called before the first frame update
    void Start()
    {
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
        theRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isOnGround = Physics2D.OverlapCapsule(groundCheckPoint.position, new Vector2(0.6f, 0.0375f), CapsuleDirection2D.Horizontal, 0f, whatIsGround);

        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, jumpPower);
        }

        //if player is falling downards
        if(theRB.velocity.y < 0f)
        {
            theRB.velocity -= vecGravity * fallMulitplier * Time.deltaTime;
        }
    }
}
