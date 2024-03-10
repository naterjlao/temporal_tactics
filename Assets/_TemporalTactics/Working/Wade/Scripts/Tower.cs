using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] float _rotationSmoothingAmount = 0.1f;
    [SerializeField][Expandable] TowerData Data;
    [SerializeField] SphereCollider sphereCollider;

    [SerializeField] Enemy _focusedEnemy;
    [SerializeField] public List<Enemy> SeenEnemies;

    void Start()
    {
        if (!sphereCollider) GetComponentInChildren<SphereCollider>();
        sphereCollider.radius = Data.Radius;
    }

    void FixedUpdate()
    {
        if (SeenEnemies.Count > 0)
        {
            _focusedEnemy = DetermineFocusedEnemy();

            if (_focusedEnemy)
            {
                // transform.LookAt(_focusedEnemy.transform);
                // Look At XZ
                var target = _focusedEnemy.transform.position;
                target.y = 0;

                Vector3 direction = target - transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), _rotationSmoothingAmount);

                //todo Shoot
            }
        }
    }

    Enemy DetermineFocusedEnemy()
    {
        Enemy enemyOfInterest = null;

        foreach (var enemy in SeenEnemies)
        {
            if (enemy == null)
            {
                SeenEnemies.Remove(enemy);
                break;
            }

            if (enemyOfInterest == null)
            {
                enemyOfInterest = enemy;
            }
            else if (enemy.PathPosition > enemyOfInterest.PathPosition)
            {
                enemyOfInterest = enemy;
            }
        }

        return enemyOfInterest;
    }
}
