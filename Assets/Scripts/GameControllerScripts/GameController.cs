using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject m_EnemyPrefab;
    [SerializeField] private GameObject m_WeaponSpeedUpPrefab;
    [SerializeField] private GameObject m_HealthPotionpPrefab;
    [SerializeField] private Transform m_PlayerTransform; 
    [SerializeField] private float m_SpawnRadius = 5f; 
    [SerializeField] private float m_SpawnInterval = 5f; 

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, m_SpawnInterval);
        InvokeRepeating(nameof(SpawnWeaponSpeedUp), 2f, m_SpawnInterval);
        InvokeRepeating(nameof(SpawnHealthPotion), 3f, m_SpawnInterval);
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
    private void SpawnWeaponSpeedUp()
    {
        SpawnWithinCircleRadiusOfPlayer(m_WeaponSpeedUpPrefab);
    }
    private void SpawnHealthPotion()
    {
        SpawnWithinCircleRadiusOfPlayer(m_HealthPotionpPrefab);
    }

}

