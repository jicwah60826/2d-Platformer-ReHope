using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [Range(0f, 10f)]
    [SerializeField] private float speed;

    [Range(0f, 10f)]
    [SerializeField] private float waitDuration;

    [SerializeField] private int startingPoint;

    public Transform[] points;
    private int i;
    private int speedMultiplier = 1;

    private void Start()
    {
        transform.position = points[startingPoint].position;
    }


    private void Update()
    {

        var step = speedMultiplier * speed * Time.deltaTime;

        

        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        
        {
            i++;
            StartCoroutine(WaitNextPoint());

            if (i == points.Length)
            {
                i = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, points[i].position, step);
    }

    IEnumerator WaitNextPoint()
    
    {
        //freeze the speed
        speedMultiplier = 0;

        //wait until wait duration is over
        yield return new WaitForSeconds(waitDuration);

        // un-freeze the speed
        speedMultiplier = 1;
    }

    //[SerializeField] private float speed;
    //private Vector3 targetPos;

    //[SerializeField] private GameObject ways;
    //[SerializeField] private int startingPoint;
    //[SerializeField] private Transform[] wayPoints;
    //private int pointIndex;
    //private int pointCount;
    //private int direction = 1;



    //private void Awake()
    //{
    //    get the number of children the parent object "ways" has
    //    wayPoints = new Transform[ways.transform.childCount];

    //    for (int i = 0; i < ways.gameObject.transform.childCount; i++)
    //    {
    //        wayPoints[i] = ways.transform.GetChild(i).gameObject.transform;
    //    }
    //}

    //private void Start()
    //{
    //    get the count of items in waypoints
    //    pointCount = wayPoints.Length;

    //    set the point index to 1 so that this is the first item target will move towards
    //   pointIndex = startingPoint;


    //    targetPos = wayPoints[pointIndex].transform.position;
    //}

    //private void Update()
    //{
    //    var step = speed * Time.deltaTime;
    //    transform.position = Vector3.MoveTowards(transform.position, targetPos, step);


    //    when the object has reached the target position -go to next point
    //    if (transform.position == targetPos)
    //    {
    //        NextPoint();
    //    }
    //}

    //void NextPoint()
    //{

    //    code below moves the object in a loop. Back and forth between all point in waypoints

    //    if (pointIndex == pointCount - 1) //Arrived at last point
    //    {
    //        direction = -1;
    //    }

    //    if (pointIndex == 0) //Arrived at first point
    //    {
    //        direction = 1;
    //    }

    //    pointIndex += direction;
    //    targetPos = wayPoints[pointIndex].transform.position;
    //}
}
