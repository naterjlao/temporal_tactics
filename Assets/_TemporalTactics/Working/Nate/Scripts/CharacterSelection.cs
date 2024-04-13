using System.Collections;
using System.Collections.Generic;
using System;
using NaughtyAttributes;
using UnityEngine;

[System.Serializable]
public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characters;
    public int selection = 0;
    public void Start()
    {
        // Set only the default character visible.
        for (int idx = 0; idx < characters.Length; idx++)
        {
            characters[idx].SetActive(idx == selection);
        }
    }

    public float speed = 3;
    private float screen_right = -50;
    private float screen_middle = -330;
    private float screen_left = -580;
    private float dest = -580;
    public void Update()
    {
        float error = dest - transform.localPosition.x;
        transform.localPosition = new Vector3(
            transform.localPosition.x + (speed * Time.deltaTime * error),
            transform.localPosition.y,
            transform.localPosition.z
        );
    }

    public bool atDest()
    {
        return Math.Abs(dest - transform.localPosition.x) < 20;
    }

    [Button]
    public void flyLeft()
    {
        dest = screen_left;
    }

    [Button]
    public void flyMiddle()
    {
        dest = screen_middle;
    }

    [Button]
    public void flyRight()
    {
        dest = screen_right;
    }

    [Button]
    IEnumerator Next()
    {
        // Fly out
        flyRight();
        yield return new WaitUntil(atDest);

        // Change out character
        characters[selection].SetActive(false);
        selection = (selection + 1) % characters.Length;
        characters[selection].SetActive(true);

        // Fly in
        transform.localPosition = new Vector3(
            screen_left,
            transform.localPosition.y,
            transform.localPosition.z);
        flyMiddle();
    }

    [Button]
    IEnumerator Prev()
    {
        // Fly out
        flyLeft();
        yield return new WaitUntil(atDest);

        characters[selection].SetActive(false);
        selection = selection - 1;
        if (selection < 0)
        {
            selection = characters.Length - 1;
        }
        characters[selection].SetActive(true);

        // Fly in
        transform.localPosition = new Vector3(
            screen_right,
            transform.localPosition.y,
            transform.localPosition.z);
        flyMiddle();
    }
}
