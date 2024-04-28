using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldCamera : MonoBehaviour
{
    public Player target;
    public float TrackingSpeed = 10.0f;

    private Vector3 initial_position;
    private float initial_distance;
    private Vector3 TRACK_MASK = new Vector3(1,0,1);

    void Start()
    {
        initial_position = this.transform.position - target.transform.position;
    }

    void Update()
    {
        update_position();
        update_rotation();
    }

    private void update_position()
    {
        Vector3 panning = initial_position.z * 
            new Vector3(
                Mathf.Sin(target.Heading / 180f * Mathf.PI),
                0f,
                Mathf.Cos(target.Heading / 180f * Mathf.PI));

        Vector3 target_position = Vector3.Scale(target.transform.position, TRACK_MASK) // Track x,z
            + new Vector3(0, target.transform.position.y + initial_position.y, 0)
            + panning;

        Vector3 error = transform.position - target_position;
        transform.position = target_position;
    }

    private void update_rotation()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, target.Heading, transform.eulerAngles.z);
    }
}
