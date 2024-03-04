using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "New Tower Data", order = 100)]

public class TowerData : ScriptableObject
{
    public float Damage;
    public float Radius;
    public float Cost;
}
