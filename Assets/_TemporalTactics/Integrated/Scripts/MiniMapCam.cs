using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCam : MonoBehaviour
{
    public GameObject player;
    private Vector3 plane = new Vector3(1,0,1);

    void Update()
    {
        // just follow the player around laterally
        transform.position = new Vector3(
            player.transform.position.x,
            transform.position.y,
            player.transform.position.z);
    }
}
