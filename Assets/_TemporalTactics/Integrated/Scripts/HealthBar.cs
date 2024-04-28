using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class HealthBar : MonoBehaviour
{
    private float lastHealth;
    private float initialXScale;
    private float targetXScale;

    // Start is called before the first frame update
    void Start()
    {
        initialXScale = transform.localScale.x;
        targetXScale = initialXScale;
        lastHealth = playerStats.health;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastHealth != playerStats.health)
        {
            lastHealth = playerStats.health;
            targetXScale = playerStats.health / 100.0f * initialXScale;
        }
        float error = targetXScale - transform.localScale.x;
        transform.localScale = transform.localScale + error * Time.deltaTime * new Vector3(1,0,0);
    }

    // TESTING ONLY!
    [Button]
    void increase_health()
    {
        playerStats.health += 10.0f;
    }

    [Button]
    void decrease_health()
    {
        playerStats.health -= 10.0f;
    }
}
