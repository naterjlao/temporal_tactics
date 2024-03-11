using System;
using UnityEngine;

public class MageDemoController : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    private int movement_speed_hash;

    public float BASE_SPEED = 5f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        // Optimize: reduce lookup overhead
        movement_speed_hash = Animator.StringToHash("movement_speed");
    }

    // Update is called once per frame
    void Update()
    {
        // Technically, this is a keyboard only game - so this might not be necessary
        float AXIS_THRESHOLD = .5f;
        float h_axis = Input.GetAxis("Horizontal");
        float v_axis = Input.GetAxis("Vertical");

        Debug.Log(h_axis + " " + v_axis);

        // Calculate movement speed - only run if we are walking.
        // REF: https://www.youtube.com/watch?v=-FhvQDqmgmU
        int command_speed = 0;
        if ((Math.Abs(h_axis) > AXIS_THRESHOLD) || (Math.Abs(v_axis) > AXIS_THRESHOLD))
        {
            command_speed += 1;
            if (Input.GetKey("space"))
                command_speed += 1;
        }

        int current_speed = animator.GetInteger(movement_speed_hash);
        // Optimize: only use setter if there is a change of state
        if (current_speed != command_speed)
        {
            animator.SetInteger(movement_speed_hash, command_speed);
        }

        Vector3 move = new Vector3(h_axis, 0, v_axis);
        controller.Move(move * Time.deltaTime * BASE_SPEED * command_speed);
    }
}
