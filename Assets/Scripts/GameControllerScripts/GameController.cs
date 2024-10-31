using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameState m_bossState;
    public GameState m_normalState;
    [SerializeField] private GameObject m_EnemyPrefab;
    [SerializeField] private GameObject m_Boss1Prefab;
    [SerializeField] private GameObject m_Boss2Prefab;
    [SerializeField] private GameObject m_Boss3Prefab;
    [SerializeField] private GameObject m_ShootingEnemyPrefab;
    [SerializeField] private GameObject m_WeaponSpeedUpPrefab;
    [SerializeField] private GameObject m_HealthPotionpPrefab;
    [SerializeField] private GameObject m_BossBorderPrefab;
    [SerializeField] private GameObject m_Enemy2Prefab;
    [SerializeField] private GameObject m_Enemy3Prefab;
    [SerializeField] private Transform m_PlayerTransform;
    [SerializeField] private TextMeshProUGUI m_BossCountdownText;
    [SerializeField] private float m_CountdownTime = 10f;
    private bool m_BossFightStarted = false;
    private string m_sceneName;
    [SerializeField] private float m_SpawnRadius = 5f; 
    [SerializeField] private float m_SpawnInterval = 5f; 

    private void Start()
    {
        m_sceneName = SceneManager.GetActiveScene().name;
        var beacon = FindObjectOfType<Beacon>();
        //trigger normal state
        beacon.gameStateChannel.StateEntered(m_normalState);

        StartCoroutine(CountdownToBoss());
        switch (m_sceneName)
        {
            case "Level 1 Scene":

                InvokeRepeating(nameof(SpawnEnemy), 1f, m_SpawnInterval);
                InvokeRepeating(nameof(SpawnEnemy2), 2f, m_SpawnInterval);
                InvokeRepeating(nameof(SpawnWeaponSpeedUp), 2f, m_SpawnInterval);
                InvokeRepeating(nameof(SpawnHealthPotion), 3f, m_SpawnInterval);
                InvokeRepeating(nameof(SpawnShootingEnemy), 4f, m_SpawnInterval);
                break;
            case "Level 2 scene":
                InvokeRepeating(nameof(SpawnEnemy), 1f, m_SpawnInterval);
                InvokeRepeating(nameof(SpawnEnemy2), 1.5f, m_SpawnInterval);
                InvokeRepeating(nameof(SpawnEnemy3), 3f, m_SpawnInterval);
                InvokeRepeating(nameof(SpawnWeaponSpeedUp), 2f, m_SpawnInterval);
                InvokeRepeating(nameof(SpawnHealthPotion), 3f, m_SpawnInterval);
                break;
            case "Level 2 Scene":
                InvokeRepeating(nameof(SpawnEnemy), 1f, m_SpawnInterval);
                InvokeRepeating(nameof(SpawnEnemy2), 1.5f, m_SpawnInterval);
                InvokeRepeating(nameof(SpawnEnemy3), 2f, m_SpawnInterval);
                InvokeRepeating(nameof(SpawnWeaponSpeedUp), 2f, m_SpawnInterval);
                InvokeRepeating(nameof(SpawnHealthPotion), 2f, m_SpawnInterval);
                break;
            case "Level 3 Scene":
                InvokeRepeating(nameof(SpawnEnemy), 1f, m_SpawnInterval);
                InvokeRepeating(nameof(SpawnEnemy2), 1.2f, m_SpawnInterval);
                InvokeRepeating(nameof(SpawnEnemy3), 3f, m_SpawnInterval);
                InvokeRepeating(nameof(SpawnWeaponSpeedUp), 2f, m_SpawnInterval);
                InvokeRepeating(nameof(SpawnHealthPotion), 2f, m_SpawnInterval);
                InvokeRepeating(nameof(SpawnShootingEnemy), 2f, m_SpawnInterval);
                break;
        }
    }

    private void SpawnWithinCircleRadiusOfPlayer(GameObject i_ObjectToSpawn)
    {
        if (m_PlayerTransform == null)
        {
            return;
        }
        Vector2 randomPosition = Random.insideUnitCircle * m_SpawnRadius;
        Vector3 spawnPosition = (Vector3)randomPosition + m_PlayerTransform.position;

        Instantiate(i_ObjectToSpawn, spawnPosition, Quaternion.identity);
    }
    private void SpawnWithPlayerAsCenter(GameObject m_BossBorderPrefab)
    {
        if(m_PlayerTransform != null)
        {
            Instantiate(m_BossBorderPrefab, m_PlayerTransform.position, Quaternion.identity);
        }
        
    }

    private void SpawnEnemy()
    {
        SpawnWithinCircleRadiusOfPlayer(m_EnemyPrefab);
    }
    private void SpawnEnemy3()
    {
        SpawnWithinCircleRadiusOfPlayer(m_Enemy3Prefab);
    }
    private void SpawnWeaponSpeedUp()
    {
        SpawnWithinCircleRadiusOfPlayer(m_WeaponSpeedUpPrefab);
    }
    private void SpawnHealthPotion()
    {
        SpawnWithinCircleRadiusOfPlayer(m_HealthPotionpPrefab);
    }
    private void SpawnShootingEnemy()
    {
        SpawnWithinCircleRadiusOfPlayer(m_ShootingEnemyPrefab);
    }

    private void SpawnBossBorder()
    {
        SpawnWithPlayerAsCenter(m_BossBorderPrefab);
    }
    private void SpawnBoss()
    {
        var beacon = FindObjectOfType<Beacon>();
        //trigger bossState
        beacon.gameStateChannel.StateEntered(m_bossState);

        switch (m_sceneName)
        {
            case "Level 1 Scene":
                SpawnWithinCircleRadiusOfPlayer(m_Boss1Prefab);
                break;
            case "Level 2 scene":
                SpawnWithinCircleRadiusOfPlayer(m_Boss2Prefab);
                break;
            case "Level 2 Scene":
                SpawnWithinCircleRadiusOfPlayer(m_Boss2Prefab);
                break;
            case "Level 3 Scene":
                SpawnWithinCircleRadiusOfPlayer(m_Boss3Prefab);
                break;
        }

    }

    private void SpawnEnemy2()
    {
        SpawnWithinCircleRadiusOfPlayer(m_Enemy2Prefab);
    }

    private IEnumerator CountdownToBoss()
    {
        while (m_CountdownTime > 0)
        {
            m_BossCountdownText.text = m_CountdownTime.ToString();
            yield return new WaitForSeconds(1f);
            m_CountdownTime--;
        }

        Invoke(nameof(SpawnBoss), 0.1f);
        Invoke(nameof(SpawnBossBorder),0.1f);
        m_BossCountdownText.text = "Boss Fight!";
        SpawnWithPlayerAsCenter(m_BossBorderPrefab);
    }


}

