using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject m_EnemyPrefab; 
    [SerializeField] private Transform m_PlayerTransform; 
    [SerializeField] private float m_SpawnRadius = 5f; 
    [SerializeField] private float m_SpawnInterval = 5f; 

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, m_SpawnInterval);
    }

    private void SpawnWithinCircleRadiusOfPlayer(GameObject i_ObjectToSpawn)
    {
        Vector2 randomPosition = Random.insideUnitCircle * m_SpawnRadius;
        Vector3 spawnPosition = (Vector3)randomPosition + m_PlayerTransform.position;

        Instantiate(i_ObjectToSpawn, spawnPosition, Quaternion.identity);
    }
    private void SpawnEnemy()
    {
        SpawnWithinCircleRadiusOfPlayer(m_EnemyPrefab);
    }

}

