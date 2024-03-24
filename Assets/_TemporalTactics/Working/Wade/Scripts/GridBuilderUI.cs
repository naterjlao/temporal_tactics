using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridBuilderUI : MonoBehaviour
{
    [SerializeField] GridBuilder gridBuilder;
    [SerializeField] PathCreator pathCreator;

    [SerializeField] public Toggle resetToggle, pathModeToggle;
    void Start()
    {
        resetToggle.isOn = pathModeToggle.isOn = false;
    }
}
