using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Temporal Tactics/New Tower Data", order = 100)]

public class TowerData : ScriptableObject
{
    public float RateOfFire;
    public float FireForce;
    public float Radius;
    public float Cost;
}
