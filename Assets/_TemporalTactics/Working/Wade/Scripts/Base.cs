using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] AudioClip failSound;
    [SerializeField] TowerDefenseUI towerDefenseUI;
    public float Health;
    public AudioSource audioSource;

    private void Start()
    {
        towerDefenseUI.UpdateBaseHealth((int)Health);
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponentInParent<Enemy>();

        if (enemy != null)
        {
            LoseHealth(enemy.Data.Damage);
        }
    }

    public void LoseHealth(float amount)
    {
        Health = Mathf.Max(0, Health - amount);

        // print($"Base Health: {Health}");

        towerDefenseUI.UpdateBaseHealth((int)Health);

        if (Health <= 0)
        {
            Debug.Log("BASE HAS BEEN DESTROYED");

            towerDefenseUI.LevelFailed();

            // play tower fail sound
            audioSource.PlayOneShot(failSound);

            // Time.timeScale = 0;
        }
    }
}
