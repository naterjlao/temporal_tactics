using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class TowerAmmo : MonoBehaviour
{
    public Rigidbody Rigidbody;
    [Expandable] public TowerAmmoData Data;

    private void Awake()
    {
        Rigidbody.mass = Data.Mass;
    }

    private void Start()
    {
        StartCoroutine(DestroyTimer());
    }
    private void OnCollisionEnter(Collision other)
    {
        var enemy = other.collider.GetComponentInParent<Enemy>();

        if (enemy != null)
        {
            // Subtract Health from enemy
            enemy.Damage(Data.Damage);
            Destroy(gameObject);
        }

        //Hit particles/effect
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(2);
    }
}
