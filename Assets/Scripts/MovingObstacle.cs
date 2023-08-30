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
}
