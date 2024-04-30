using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Tower")]
    [SerializeField] float _rotationSmoothingAmountXZ = 0.1f;
    [SerializeField] float _rotationSmoothingAmountY = 0.25f;
    [SerializeField] float fireAngleThreshold = 5f;
    [SerializeField] Vector2 yAngleBounds;
    [SerializeField] Transform yPivot;
    [SerializeField] SphereCollider sphereCollider;
    [SerializeField][Expandable] public TowerData Data;

    [Header("Ammo")]
    [SerializeField] public TowerAmmo AmmoPrefab;
    [SerializeField] Transform FirePoint;

    bool canFire = true;


    [Header("Enemies")]
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
                //* Look At XZ Direction
                var targetXZ = _focusedEnemy.transform.position;
                targetXZ.y = transform.position.y;

                Vector3 directionXZ = targetXZ - transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directionXZ), _rotationSmoothingAmountXZ);

                //* Look at Y Direction
                // var targetY = _focusedEnemy.transform.position;
                // targetY.x = transform.position.x; // Ignore x and z coordinates
                // targetY.z = transform.position.z;

                // Vector3 directionY = targetY - yPivot.position;
                // Quaternion targetRotationY = Quaternion.LookRotation(directionY);
                // // yPivot.rotation = Quaternion.Slerp(yPivot.rotation, targetRotationY, _rotationSmoothingAmountY);

                // // Calculate the angle between current rotation and target rotation
                // float angleY = Quaternion.Angle(transform.rotation, targetRotationY);

                // // Clamp the angle within the specified bounds
                // float clampedAngleY = Mathf.Clamp(angleY, yAngleBounds.x, yAngleBounds.y);

                // // Get the axis of rotation from the target rotation
                // Vector3 axisY;
                // targetRotationY.ToAngleAxis(out angleY, out axisY);

                // // Create a clamped rotation based on the axis and clamped angle
                // Quaternion clampedRotationY = Quaternion.AngleAxis(clampedAngleY, axisY);

                // yPivot.transform.rotation = Quaternion.Slerp(yPivot.transform.rotation, clampedRotationY, _rotationSmoothingAmountY);

                //* Shoot
                var angle = Vector3.Angle(transform.forward, directionXZ);
                if (angle < fireAngleThreshold)
                {
                    Fire();
                }
            }
        }
    }
    void Fire()
    {
        if (!canFire) return;

        // Instantiate a new projectile at the launch point
        TowerAmmo newAmmo = Instantiate(AmmoPrefab, FirePoint.position, FirePoint.rotation);
        newAmmo.transform.parent = transform;

        // Apply force to the projectile in the forward direction
        newAmmo.Rigidbody.AddForce(FirePoint.forward * Data.FireForce, ForceMode.Impulse);

        StartCoroutine(FireCooldown());
    }
    IEnumerator FireCooldown()
    {
        canFire = false;

        yield return new WaitForSeconds(Data.RateOfFire);

        canFire = true;
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
