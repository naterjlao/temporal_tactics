using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy", menuName = "New Enemy Data", order = 100)]
public class EnemyData : ScriptableObject
{
    public float Speed;
    public float Health;
    public float Damage;
}
