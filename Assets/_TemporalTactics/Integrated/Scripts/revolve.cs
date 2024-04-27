using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines.Editor;
using UnityEngine;
using NaughtyAttributes;

public class revolve : MonoBehaviour
{
    public float revolve_speed = 10;
    public float transition_speed = 3;

    private float on_screen = -440;
    private float off_screen = -600;
    private float dest = -440;
    void Update()
    {
        transform.Rotate(0,revolve_speed * Time.deltaTime,0);

        float error = dest - transform.localPosition.y;
        if (Math.Abs(error) > 1.0)
        {
            transform.Translate(Vector3.up * Time.deltaTime * error * transition_speed);
        }
    }

    [Button]
    public void Hide()
    {
        dest = off_screen;
    }

    [Button]
    public void Show()
    {
        dest = on_screen;
    }
}
