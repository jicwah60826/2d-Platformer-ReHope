using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Spinner : MonoBehaviour
{
    [SerializeField] float zRotation = 1f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.rotation.x, transform.rotation.y, zRotation);
    }
}