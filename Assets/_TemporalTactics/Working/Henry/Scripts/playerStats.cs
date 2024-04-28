using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : MonoBehaviour
{
    // Creates a variable counter for amount of gold
    public static int goldCount;

    // Player's Health from 0.0-100.0%
    public static float health;

    // Holds the player's character selection
    public static int characterSelection;

    // Sets the starting gold value
    public const int startingCount = 500;

    void Awake()
    {
        // Keep ourselves alive even if we transition scenes.
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Sets starting gold equal to the starting gold value
        playerStats.goldCount = startingCount;
        playerStats.characterSelection = 0;
        playerStats.health = 100.0f;

        // Converts gold count to a string for debugging purposes
        string goldCountString = goldCount.ToString();

        // Debugs the starting/current goldCount amount
        Debug.Log(goldCountString);
    }

#if false
    void Update()
    {
        Debug.Log($"goldCount: {goldCount}, characterSelection: {characterSelection}");
    }
#endif
}
