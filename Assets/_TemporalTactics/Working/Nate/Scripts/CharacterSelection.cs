using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[System.Serializable]
public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characters;
    public int selection = 0;

    [Button]
    public void Next()
    {
        characters[selection].SetActive(false);
        selection = (selection + 1) % characters.Length;
        characters[selection].SetActive(true);
    }

    [Button]
    public void Prev()
    {
        characters[selection].SetActive(false);
        selection = selection - 1;
        if (selection < 0)
        {
            selection = characters.Length - 1;
        }
        characters[selection].SetActive(true);
    }
}
