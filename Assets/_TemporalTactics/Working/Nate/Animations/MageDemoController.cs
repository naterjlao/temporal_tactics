using System;
using UnityEngine;
using DG.Tweening;

public class MageDemoController : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    private int movement_speed_hash;

    public float BASE_SPEED = 5f;
    public float AXIS_THRESHOLD = .5f;
    public float GRAVITY = -9.8f;

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
        float h_axis = Input.GetAxis("Horizontal");
        float v_axis = Input.GetAxis("Vertical");
        int command_speed = calculate_speed(h_axis, v_axis);

        update_gravity();
        update_movement(h_axis, v_axis, command_speed);
        update_rotation(h_axis, v_axis);
        update_animation(command_speed);
    }

    private int calculate_speed(float h_axis, float v_axis)
    {
        // Calculate movement speed - only run if we are walking.
        int command_speed = 0;
        if ((Math.Abs(h_axis) > AXIS_THRESHOLD) || (Math.Abs(v_axis) > AXIS_THRESHOLD))
        {
            command_speed += 1;
            if (Input.GetKey("space"))
                command_speed += 1;
        }
        return command_speed;
    }

    private void update_gravity()
    {
        if (!controller.isGrounded)
        {
            Vector3 fall = new Vector3(0,1,0) * GRAVITY;
            controller.Move(fall * Time.deltaTime);
        }
    }

    private void update_movement(float h_axis, float v_axis, int command_speed)
    {
        Vector3 move = new Vector3(h_axis, 0, v_axis);
        move.Normalize(); // Normalize the vector so that diagonal movement is consistent (RIP speedruns)
        controller.Move(move * Time.deltaTime * BASE_SPEED * command_speed);
    }

    private void update_rotation(float h_axis, float v_axis)
    {
        bool v_turn = Math.Abs(v_axis) > AXIS_THRESHOLD;
        bool h_turn = Math.Abs(h_axis) > AXIS_THRESHOLD;

        if (v_turn || h_turn)
        {
            /// @todo there's prob a more clever way to do this, but I'm lazy
            int heading;
            // Diagonal Directions
            if (v_turn && h_turn)
            {
                if ((v_axis > 0) && (h_axis > 0))
                    heading = 45;
                else if ((v_axis < 0) && (h_axis > 0))
                    heading = 135;
                else if ((v_axis < 0) && (h_axis < 0))
                    heading = 225;
                else
                    heading = 315;
            }
            // X-Axis Cardinal Directions
            else if (v_turn)
            {
                if (v_axis > 0)
                    heading = 0;
                else
                    heading = 180;
            }
            // Z-Axis Cardinal Directions
            else
            {
                if (h_axis > 0)
                    heading = 90;
                else
                    heading = 270;
            }

            // Turn towards the direction we want to go
            transform.DORotateQuaternion(Quaternion.Euler(0,heading,0), 0.5f);
        }
    }

    private void update_animation(int command_speed)
    {
        // Optimize: only use setter if there is a change of state
        // REF: https://www.youtube.com/watch?v=-FhvQDqmgmU
        int current_speed = animator.GetInteger(movement_speed_hash);
        if (current_speed != command_speed)
        {
            animator.SetInteger(movement_speed_hash, command_speed);
        }
    }
}
