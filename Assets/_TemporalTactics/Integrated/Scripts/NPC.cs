using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NPC : MonoBehaviour
{
    public GameObject player;
    public NPCTextPrompt textPrompt;

    private Quaternion initial_looking_direction;
    private Quaternion looking_direction;

    // Start is called before the first frame update
    void Start()
    {
        initial_looking_direction = transform.rotation;
        looking_direction = initial_looking_direction;
    }

    // Update is called once per frame
    void Update()
    {
        // transform.DORotateQuaternion(looking_direction, 0.5f);
        transform.rotation = looking_direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            looking_direction = Quaternion.LookRotation(other.gameObject.transform.position - transform.position);
            textPrompt.Show();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            looking_direction = initial_looking_direction;
            textPrompt.Hide();
        }
    }
}
