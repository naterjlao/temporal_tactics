using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataPreserve : MonoBehaviour
{
    // Start and Update methods have been deleted

    public static dataPreserve Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
