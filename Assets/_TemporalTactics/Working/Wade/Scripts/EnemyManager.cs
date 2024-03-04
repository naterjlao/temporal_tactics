using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using NaughtyAttributes;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] SplineComputer _splineComputer;

    [Button]
    void SpawnEnemy()
    {
        var newEnemy = Instantiate(enemy, transform);
        newEnemy.SetSplineComputer(_splineComputer);
    }
}
