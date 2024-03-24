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

    System.IDisposable spawnDisposable;

    [Button]
    public void SpawnEnemy()
    {
        var newEnemy = Instantiate(enemy, transform);
        newEnemy.SetSplineComputer(_splineComputer);

        var offsetPos = new Vector3(Random.Range(xOffset.x, xOffset.y), 0, 0);
        newEnemy.meshTransform.position = offsetPos;
    }

    public void StartSpawning()
    {
        SpawnEnemy();

        spawnDisposable?.Dispose();
        spawnDisposable = Observable.Interval(System.TimeSpan.FromSeconds(2f)).Subscribe(_ =>
        {
            SpawnEnemy();
        });
    }

    public void StopSpawning()
    {
        spawnDisposable?.Dispose();
        spawnDisposable = null;
    }
}
