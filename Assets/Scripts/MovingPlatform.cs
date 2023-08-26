using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    Vector3 targetPos;


    MovementController movementController;
    Rigidbody2D theRB;
    Vector3 moveDirection;

    Rigidbody2D playerRB;

    public GameObject ways;
    public Transform[] movePoints;
    private int pointIndex;
    private int pointCount;
    private int direction = 1;

    public float waitDuration;

    private void Awake()
    {
        movementController = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>();
        theRB = GetComponent<Rigidbody2D>();
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        movePoints = new Transform[ways.transform.childCount];

        for( int i  = 0; i < ways.gameObject.transform.childCount; i++)
        {
            movePoints[i] = ways.transform.GetChild(i).gameObject.transform;
        }
    }

    private void Start()
    {
        pointIndex = 1;
        pointCount = movePoints.Length;
        targetPos = movePoints[1].transform.position;
        DirectionCalculate();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, targetPos) < 0.05f)
        {
            NextPoint();
        }
    }

    private void FixedUpdate()
    {
        theRB.velocity = moveDirection * speed;
    }


    void NextPoint()
    {
        transform.position = targetPos;
        moveDirection = Vector3.zero;

        if(pointIndex == pointCount - 1) // arrived at last point
        {
            direction = -1;
        }

        if (pointIndex == 0) // arrived at first point
        {
            direction = 1;
        }

        pointIndex += direction;
        targetPos = movePoints[pointIndex].transform.position;

        StartCoroutine(WaitAtNextPoint());
    }

    IEnumerator WaitAtNextPoint()
    
    {
        yield return new WaitForSeconds(waitDuration);
        DirectionCalculate();
    }

    private void DirectionCalculate()
    {
        moveDirection = (targetPos - transform.position).normalized;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            movementController.isOnPlatform = true;
            movementController.platformRB = theRB;
            playerRB.gravityScale = playerRB.gravityScale * 50f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            movementController.isOnPlatform = false;
            playerRB.gravityScale = playerRB.gravityScale  / 50f;
        }
    }
}
