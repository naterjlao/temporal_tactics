using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject player;
    public NPCTextPrompt textPrompt;

    private Quaternion initial_looking_direction;
    private Quaternion from_direction;
    private Quaternion next_direction;
    private float lerpDerp;

    // Start is called before the first frame update
    void Start()
    {
        initial_looking_direction = transform.rotation;
        from_direction = initial_looking_direction;
        next_direction = initial_looking_direction;
        lerpDerp = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (lerpDerp < 1.0f)
        {
            transform.rotation = Quaternion.Lerp(from_direction,next_direction,lerpDerp);
            lerpDerp += Time.deltaTime * 3.5f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            from_direction = initial_looking_direction;
            next_direction = Quaternion.LookRotation(other.gameObject.transform.position - transform.position);
            lerpDerp = 0.0f;
            textPrompt.Show();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            from_direction = next_direction;
            next_direction = initial_looking_direction;
            lerpDerp = 0.0f;
            textPrompt.Hide();
        }
    }
}
