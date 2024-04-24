using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watermill : MonoBehaviour
{
    public float speed = 0.2f;
    void Update()
    {
        // just spin forever
        transform.Rotate(speed,0,0,Space.Self);  
    }
}
