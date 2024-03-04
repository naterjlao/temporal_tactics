using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using NaughtyAttributes;
using UniRx;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] SplineComputer _splineComputer;
    [SerializeField] Vector2 xOffset = new Vector2(-0.2f, 0.2f);

    [Button]
    void SpawnEnemy()
    {
        var newEnemy = Instantiate(enemy, transform);
        newEnemy.SetSplineComputer(_splineComputer);

        var offsetPos = new Vector3(Random.Range(xOffset.x, xOffset.y), 0, 0);
        newEnemy.meshTransform.position = offsetPos;
    }

    private void Start()
    {
        SpawnEnemy();

        Observable.Interval(System.TimeSpan.FromSeconds(2f)).Subscribe(_ =>
        {
            SpawnEnemy();
        });
    }
}
