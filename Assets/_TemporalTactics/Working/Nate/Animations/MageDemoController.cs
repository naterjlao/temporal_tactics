using UnityEngine;

public class MageDemoController : MonoBehaviour
{
    Animator animator;
    int movement_speed_hash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        // Optimize: reduce lookup overhead
        movement_speed_hash = Animator.StringToHash("movement_speed");
    }

    // Update is called once per frame
    void Update()
    {
        int current_speed = animator.GetInteger(movement_speed_hash);
        int command_speed = 0;

        // Calculate movement speed - only run if we are walking.
        if (Input.GetKey("w"))
        {
            command_speed += 1;
            if (Input.GetKey("space"))
            {
                command_speed += 1;
            }
        }

        // Optimize: only use setter if there is a change of state
        if (current_speed != command_speed)
        {
            animator.SetInteger(movement_speed_hash, command_speed);
        }
    }
}
