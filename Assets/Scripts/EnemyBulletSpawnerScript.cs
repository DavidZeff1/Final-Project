using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletSpawnerScript : MonoBehaviour
{
    [SerializeField] GameObject m_BulletPrefab; 
    [SerializeField] Bullet m_BulletType;       
    [SerializeField] Transform m_EnemyObject;   
    private Transform m_PlayerObject;           

    void Start()
    {
        InvokeRepeating(nameof(SpawnBullets), 1f, m_BulletType.spawnRate);

        GameObject player = GameObject.FindGameObjectWithTag(m_BulletType.target);
        if (player != null)
        {
            m_PlayerObject = player.transform;
        }
        GameEventManager.instance.OnBulletSpawnRateIncrease += IncreaseBulletSpawnRate;
    }

    private void SpawnBullets()
    {
        if (m_PlayerObject != null)
        {
            GameObject bullet = Instantiate(m_BulletPrefab, m_EnemyObject.position, Quaternion.identity);
            if (bullet.TryGetComponent<BulletMovementScript>(out var bulletMovement))
            {
                bulletMovement.SetTarget(m_BulletType.target);
            }
        }
    }
    private void IncreaseBulletSpawnRate()
    {
        CancelInvoke(nameof(SpawnBullets)); // Cancel the old repeating Invoke
        m_BulletType.spawnRate /= 2f; // Increase the spawn rate (half the time)
        InvokeRepeating(nameof(SpawnBullets), 0f, m_BulletType.spawnRate);
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event
        GameEventManager.instance.OnBulletSpawnRateIncrease -= IncreaseBulletSpawnRate;
    }
}


