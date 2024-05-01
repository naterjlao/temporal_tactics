using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using NaughtyAttributes;
using UniRx;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] AudioClip destroySound, completionSound;
    [SerializeField] Enemy enemy;
    [SerializeField] SplineComputer _splineComputer;
    [SerializeField] Vector2 xOffset = new Vector2(-0.2f, 0.2f);
    [SerializeField][Expandable] LevelEnemies levelEnemies;
    [SerializeField] TowerDefenseUI towerDefenseUI;
    [SerializeField] TowerDefenseGoldManager goldManager;

    int enemyCounter = 0;
    float totalHealth = 0;
    float healthDestroyed = 0;

    public float Progress = 0;

    private void Start()
    {
        totalHealth = CalculateTotalHealth();

        StartEnemySpawn();
    }

    [Button]
    public void SpawnSingleEnemy()
    {
        var newEnemy = Instantiate(enemy, transform);
        newEnemy.SetSplineComputer(_splineComputer);

        var offsetPos = new Vector3(Random.Range(xOffset.x, xOffset.y), 0, 0);
        newEnemy.meshTransform.position = offsetPos;
    }

    [Button]
    public void StartEnemySpawn()
    {
        StartCoroutine(SpawnEnemiesCoroutine());
    }

    IEnumerator SpawnEnemiesCoroutine()
    {
        foreach (var enemy in levelEnemies.Enemies)
        {
            SpawnNewEnemy(enemy);
            enemyCounter++;

            // Debug.Log($"Enemies Left: {GetEnemyCount()}");

            yield return new WaitForSeconds(enemy.Data.SpawnCooldown);
        }
    }

    public void SpawnNewEnemy(Enemy enemy)
    {
        var newEnemy = Instantiate(enemy, transform);
        newEnemy.SetSplineComputer(_splineComputer);

        // todo change this to use spline positioner offset?
        var offsetPos = new Vector3(Random.Range(xOffset.x, xOffset.y), 0, 0);
        newEnemy.meshTransform.position = offsetPos;
    }

    public float CalculateTotalHealth()
    {
        var health = 0f;
        foreach (var enemy in levelEnemies.Enemies)
        {
            health += enemy.Data.Health;
        }

        return health;
    }

    public float GetEnemyCount()
    {
        return levelEnemies.Enemies.Count - enemyCounter;
    }

    public void EnemyDestroyed(float health, int gold)
    {
        healthDestroyed += health;
        goldManager.UpdateGold(gold);

        // todo play tower destroy sound
        // audioSource.PlayOneShot(destroySound);

        var progress = GetProgress();

        // print($"Progress: {progress:F0}%");
        towerDefenseUI.UpdateProgress(progress);

        if (progress >= 100)
        {
            Debug.Log("LEVEL COMPLETED!");

            towerDefenseUI.LevelCompleted();

            // todo play tower completed sound
            // audioSource.PlayOneShot(completedSound);
        }
    }

    public float GetProgress()
    {
        return (healthDestroyed / totalHealth) * 100f;
    }
}
