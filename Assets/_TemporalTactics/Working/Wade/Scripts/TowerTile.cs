using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTile : MonoBehaviour
{
    public bool ContainsTower = false;
    void Start()
    {

    }

    void BuildTower()
    {
        ContainsTower = true;
    }
}
