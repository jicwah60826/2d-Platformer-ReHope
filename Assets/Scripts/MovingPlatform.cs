using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    #region ReHopeGamesPlatform

    [Range(0f, 10f)]
    [SerializeField] private float speed;

    [Range(0f, 10f)]
    public float waitDuration;

    [SerializeField] private int startingPoint;

    Vector3 targetPos;

    MovementController movementController;
    Rigidbody2D rb;
    Vector3 moveDirection;

    Rigidbody2D playerRB;

    public GameObject ways;
    public Transform[] wayPoints;
    int pointIndex;
    int pointCount;
    int direction = 1;




    private void Awake()
    {
        //get the movement controller script that is on the Player game object
        movementController = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>();

        //get the rigid body 2D that is on the same game object as this script (the platform)
        rb = GetComponent<Rigidbody2D>();

        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        //automatically add the target points
        wayPoints = new Transform[ways.transform.childCount];

        for (int i = 0; i < ways.gameObject.transform.childCount; i++)
        {
            wayPoints[i] = ways.transform.GetChild(i).gameObject.transform;
        }
    }

    private void Start()
    {
        transform.position = wayPoints[startingPoint].position;

        pointIndex = 1;
        pointCount = wayPoints.Length;
        targetPos = wayPoints[1].transform.position;
        DirectionCalculate();
    }


    private void Update()
    {
        if (Vector2.Distance(transform.position, targetPos) < 0.05f)
        {
            NextPoint();
        }
    }

    private void FixedUpdate()
    {
        // move the platform. We use fixed updated because we have a rigid body on the platform
        rb.velocity = moveDirection * speed;
    }


    private void NextPoint()
    {
        transform.position = targetPos;
        moveDirection = Vector3.zero;

        if (pointIndex == pointCount - 1) //arrived at the last point
        {
            direction = -1;
        }

        if (pointIndex == 0) //arrived at the first point
        {
            direction = 1;
        }

        pointIndex += direction;
        targetPos = wayPoints[pointIndex].transform.position;

        StartCoroutine(WaitNextPoint());
    }

    IEnumerator WaitNextPoint()
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
            movementController.platformRB = rb;
            playerRB.gravityScale = playerRB.gravityScale * 10;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            movementController.isOnPlatform = false;
            playerRB.gravityScale = playerRB.gravityScale / 10;
        }
    }

    #endregion
}
