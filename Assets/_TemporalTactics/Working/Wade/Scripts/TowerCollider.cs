using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCollider : MonoBehaviour
{
    [SerializeField] Tower tower;

    void Start()
    {
        if (!tower)
        {
            tower = GetComponentInParent<Tower>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponentInParent<Enemy>();

        if (enemy != null) tower.SeenEnemies.Add(enemy);
    }
    private void OnTriggerExit(Collider other)
    {
        var enemy = other.GetComponentInParent<Enemy>();

        if (enemy != null) tower.SeenEnemies.Remove(enemy);
    }
}
