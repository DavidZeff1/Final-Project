using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject m_EnemyPrefab;
    [SerializeField] private GameObject m_Boss1Prefab;
    [SerializeField] private GameObject m_ShootingEnemyPrefab;
    [SerializeField] private GameObject m_WeaponSpeedUpPrefab;
    [SerializeField] private GameObject m_HealthPotionpPrefab;
    [SerializeField] private GameObject m_BossBorderPrefab;
    [SerializeField] private Transform m_PlayerTransform;
    [SerializeField] private TextMeshProUGUI m_BossCountdownText;
    private float m_CountdownTime = 10f;
    private bool m_BossFightStarted = false;
    [SerializeField] private float m_SpawnRadius = 5f; 
    [SerializeField] private float m_SpawnInterval = 5f; 

    private void Start()
    {
        StartCoroutine(CountdownToBoss());
        Invoke(nameof(SpawnBoss), 10f);
        Invoke(nameof(SpawnBossBorder), 10f);
        InvokeRepeating(nameof(SpawnEnemy), 1f, m_SpawnInterval);
        InvokeRepeating(nameof(SpawnWeaponSpeedUp), 2f, m_SpawnInterval);
        InvokeRepeating(nameof(SpawnHealthPotion), 3f, m_SpawnInterval);
        InvokeRepeating(nameof(SpawnShootingEnemyPotion), 4f, m_SpawnInterval);
    }

    private void SpawnWithinCircleRadiusOfPlayer(GameObject i_ObjectToSpawn)
    {
        Vector2 randomPosition = Random.insideUnitCircle * m_SpawnRadius;
        Vector3 spawnPosition = (Vector3)randomPosition + m_PlayerTransform.position;

        Instantiate(i_ObjectToSpawn, spawnPosition, Quaternion.identity);
    }
    private void SpawnWithPlayerAsCenter(GameObject m_BossBorderPrefab)
    {
        Instantiate(m_BossBorderPrefab, m_PlayerTransform.position, Quaternion.identity);
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
    private void SpawnShootingEnemyPotion()
    {
        SpawnWithinCircleRadiusOfPlayer(m_ShootingEnemyPrefab);
    }

    private void SpawnBossBorder()
    {
        SpawnWithPlayerAsCenter(m_BossBorderPrefab);
    }
    private void SpawnBoss()
    {
        SpawnWithinCircleRadiusOfPlayer(m_Boss1Prefab);
    }

    private IEnumerator CountdownToBoss()
    {
        while (m_CountdownTime > 0)
        {
            m_BossCountdownText.text = Mathf.Ceil(m_CountdownTime).ToString();
            yield return new WaitForSeconds(1f);
            m_CountdownTime--;
        }

        m_BossCountdownText.text = "Boss Fight!";
        SpawnWithPlayerAsCenter(m_BossBorderPrefab);
    }


}

