using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;

[System.Serializable]
public class Title : MonoBehaviour
{
    public TMP_Text[] titles;
    public Color color;
    public float transition_speed = 5;

    private Color color_set;
    private bool color_change;
    void Start()
    {
        foreach (var text in titles)
        {
            text.color = color;
        }
        color_set = color;
        color_change = false;
    }

    void Update()
    {
        if (color_change)
        {
            foreach (var text in titles)
            {
                Color delta = color_set - text.color;
                text.color = text.color + (transition_speed * delta * Time.deltaTime);
            }
        }
    }

    [Button]
    public void FadeOut()
    {
        color_set = new Color(0,0,0,0);
        color_change = true;
    }

    [Button]
    public void FadeIn()
    {
        color_set = color;
        color_change = true;
    }
}
