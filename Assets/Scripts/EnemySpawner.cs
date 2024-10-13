using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject m_EnemyPrefab;
    [SerializeField] Transform m_RightSpawnPoint;
    [SerializeField] Transform m_LeftSpawnPoint;
    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemies), 1f, 3f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SpawnEnemies()
    {
        Transform spawnPoint = Random.Range(0, 2) == 0 ? m_LeftSpawnPoint : m_RightSpawnPoint;
        Vector3 adjustedPosition = spawnPoint.position;
        adjustedPosition.x += (spawnPoint == m_LeftSpawnPoint ? 2 : -2);
        Instantiate(m_EnemyPrefab, adjustedPosition, Quaternion.identity);
    }
}
