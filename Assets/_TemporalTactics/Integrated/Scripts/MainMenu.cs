//-----------------------------------------------------------------------------
// Project: TemporalTactics
// File: MainMenu.cs
// Author: Nate Lao (lao.nathan@yahoo.com)
// Description: Unity Script for MainMenu object in the StartMenu scene.
//-----------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        /// @note Scenes must be added to (File -> Build Settings) in Unity
        SceneManager.LoadScene("Overworld");
    }

    private Vector3 dest;
    void Start()
    {
        transform.localPosition = new Vector3(0,0,0);
        dest = new Vector3(0,0,0);
    }

    void Update()
    {
        Vector3 error = dest - transform.localPosition;
        if (error.magnitude > 0.1)
        {
            transform.localPosition = transform.localPosition + (error * Time.deltaTime * 5);
        }
    }

    [Button]
    public void Mainmenu()
    {
        dest = new Vector3(0,0,0);
    }

    [Button]
    public void Submenu()
    {
        dest = new Vector3(0,200,0);
    }
}
