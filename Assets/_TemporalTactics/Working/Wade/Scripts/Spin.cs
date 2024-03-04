using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float rotationsPerSecond = 1f; // Number of full rotations per second

    void Update()
    {
        // Calculate the amount of rotation for this frame
        float rotationAmount = 360f * rotationsPerSecond * Time.deltaTime;

        // Get the current local rotation
        Vector3 currentRotation = transform.localEulerAngles;

        // Add the rotation amount to the current y-axis rotation
        currentRotation.y += rotationAmount;

        // Set the new local rotation
        transform.localRotation = Quaternion.Euler(currentRotation);
    }
}
