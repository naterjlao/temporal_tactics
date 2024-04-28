// Written by Henry Dana for Temporal Tactics
// Script for switching to a tower defense scene upon colliding with a time rift

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class timeRiftTransfer : MonoBehaviour
{
    /// <summary>
    /// Make sure to update Build Settings -> Scenes to Build!
    /// </summary>
    public string destination = null;
    // Start and Update methods have been deleted

    // Note to self: make sure "Is Trigger" is checked on at least one collider!
    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            // Debug to confirm collision
            Debug.Log("Collision detected");

            if (destination != null)
                // Switches scene (scene must be added to build settings
                // String must match scene title
                SceneManager.LoadScene(destination);
        }
    }
}
