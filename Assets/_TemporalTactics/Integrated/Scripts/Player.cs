using System;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public GameObject[] characters;
    private GameObject active_character;

    private CharacterController controller;
    private Animator animator;
    private int movement_speed_hash;

    public float BaseSpeed = 1f;
    public float TurnSpeed = 5f;
    public float AxisThreshold = .5f;
    public float Gravity = -9.8f;

    // Start is called before the first frame update
    void Start()
    {
        // Show only the character that was selected by the player.
        for (int idx = 0; idx < characters.Length; idx++)
        {
            characters[idx].SetActive(idx == playerStats.characterSelection);
        }

        // Get the character asset for animations.
        active_character = characters[playerStats.characterSelection];
        animator = active_character.GetComponent<Animator>();
        movement_speed_hash = Animator.StringToHash("movement_speed");

        // Get the controller.
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Technically, this is a keyboard only game - so this might not be necessary
        float h_axis = Input.GetAxis("Horizontal");
        float v_axis = Input.GetAxis("Vertical");
        int command_speed = calculate_speed(h_axis, v_axis);

        update_gravity();
        update_movement(v_axis, command_speed);
        update_rotation(h_axis);
        update_animation(command_speed);
    }

    private int calculate_speed(float h_axis, float v_axis)
    {
        // Calculate movement speed - only run if we are walking.
        int command_speed = 0;
        if ((Math.Abs(h_axis) > AxisThreshold) || (Math.Abs(v_axis) > AxisThreshold))
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
            Vector3 fall = new Vector3(0,1,0) * Gravity;
            controller.Move(fall * Time.deltaTime);
        }
    }

    private void update_movement(float v_axis, int command_speed)
    {
        Vector3 move = new Vector3(0, 0, v_axis);
        move = transform.TransformDirection(move);
        controller.Move(move * Time.deltaTime * BaseSpeed * command_speed);
    }

    private void update_rotation(float h_axis)
    {
        float heading = transform.rotation.eulerAngles.y + h_axis * TurnSpeed;
        transform.DORotateQuaternion(Quaternion.Euler(0,heading,0), 0.5f);
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
