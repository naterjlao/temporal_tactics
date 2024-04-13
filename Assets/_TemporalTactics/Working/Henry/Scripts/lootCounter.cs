using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lootCounter : MonoBehaviour
{
    // Creates a variable counter for amount of gold
    public static int goldCount;

    // Sets the starting gold value
    public const int startingCount = 500;

    // Start is called before the first frame update
    void Start()
    {
        // Sets starting gold equal to the starting gold value
        lootCounter.goldCount = startingCount;

        // Converts gold count to a string for debugging purposes
        string goldCountString = goldCount.ToString();

        // Debugs the starting/current goldCount amount
        Debug.Log(goldCountString);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
