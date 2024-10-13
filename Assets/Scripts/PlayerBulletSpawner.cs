using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletSpawner : MonoBehaviour
{
    [SerializeField] GameObject m_BulletPrefab; // Bullet prefab
    [SerializeField] Bullet m_BulletType;       // Bullet ScriptableObject reference
    [SerializeField] Transform m_PlayerObject;  // Player's Transform
    private Transform m_EnemyObject;            // Enemy's Transform

    void Start()
    {
        // Use the spawn rate from the Bullet ScriptableObject
        InvokeRepeating(nameof(SpawnBullets), 1f, m_BulletType.spawnRate);
    }

    void Update()
    {
        // Use the target from the Bullet ScriptableObject
        GameObject enemy = GameObject.FindGameObjectWithTag(m_BulletType.target);
        if (enemy != null)
        {
            m_EnemyObject = enemy.transform;
        }
        else
        {
            m_EnemyObject = null;
        }
    }

    private void SpawnBullets()
    {
        if (m_EnemyObject != null)
        {
            GameObject bullet = Instantiate(m_BulletPrefab, m_PlayerObject.position, Quaternion.identity);
            if (bullet.TryGetComponent<BulletMovementScript>(out var bulletMovement))
            {
                bulletMovement.SetTarget(m_BulletType.target);
            }
        }
    }
}



