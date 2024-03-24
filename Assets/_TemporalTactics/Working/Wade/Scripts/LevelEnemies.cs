using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Level Enemies", menuName = "Temporal Tactics/New Level Enemies", order = 100)]

public class LevelEnemies : ScriptableObject
{
    public List<Enemy> Enemies;
}
