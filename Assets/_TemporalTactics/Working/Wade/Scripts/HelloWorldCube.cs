using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloWorldCube : MonoBehaviour
{
    // Speed of rotation
    public float rotationSpeed = 50f;

    // Update is called once per frame
    void Update()
    {
        // Rotate the cube around its local Y and X axes every frame
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }
}
