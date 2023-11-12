using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotateAxis
{
    YAxis,
    ZAxis
}

public class SelfRotation : MonoBehaviour
{
    public float speed;
    public RotateAxis rotateAxis;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rotateAxis == RotateAxis.YAxis)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * speed);
        }
        else
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * speed);
        }
    }
}
