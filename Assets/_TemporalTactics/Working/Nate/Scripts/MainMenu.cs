//-----------------------------------------------------------------------------
// Project: TemporalTactics
// File: MainMenu.cs
// Author: Nate Lao (lao.nathan@yahoo.com)
// Description: Unity Script for MainMenu object in the StartMenu scene.
//-----------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        /// @note Scenes must be added to (File -> Build Settings) in Unity

        /// @todo this is of course, temporary - should be changed to level01 or something when ready.
        SceneManager.LoadScene("Hello World Cube");

        // This is an alternate way to do this dynamically based on the Build Settings list
        // REF: https://www.youtube.com/watch?v=zc8ac_qUXQY
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
