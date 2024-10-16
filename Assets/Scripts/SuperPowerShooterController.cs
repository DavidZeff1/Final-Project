using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPowerShooterController : MonoBehaviour
{
    [SerializeField] private GameObject m_FireballPrefab;
    [SerializeField] private GameObject m_ShockWavePrefab;
    [SerializeField] private Transform m_SpawnPoint;
    [SerializeField] private PowerUseEvent m_PowerUseEvent;
    [SerializeField] private float m_ShockwaveRadius = 5f;
    [SerializeField] private float m_ShockwaveForce = 5f;
    [SerializeField] private float m_ShockwaveEffectDuration = 1f;
    [SerializeField] private LayerMask m_EnemyLayer;
    [SerializeField] private GameObject m_LazerPrefab;
    [SerializeField] private Transform m_LaserSpawnPoint;
    [SerializeField] private float m_LazerOffset = 0.5f;
    private const float PowerCooldownDuration = 5f;
    private bool[] m_PowerCooldowns = new bool[4];
    private GameObject currentLaser;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !m_PowerCooldowns[0]) UsePower(0);
        if (Input.GetKeyDown(KeyCode.W) && !m_PowerCooldowns[1]) UsePower(1);
        if (Input.GetKeyDown(KeyCode.E) && !m_PowerCooldowns[2]) UsePower(2);
        if (Input.GetKeyDown(KeyCode.R) && !m_PowerCooldowns[3]) UsePower(3);
    }

    private void UsePower(int powerIndex)
    {
        switch (powerIndex)
        {
            case 0:
                if (m_FireballPrefab != null && m_SpawnPoint != null)
                {
                    Instantiate(m_FireballPrefab, m_SpawnPoint.position, Quaternion.identity);
                }
                
                break;
            
            case 1:
                UseShockWave();
                break;

            case 2:
                UseLazer();
                
                break;
        }

        m_PowerUseEvent?.Raise(powerIndex);
        StartCoroutine(PowerCooldownRoutine(powerIndex));
    }

    private void UseShockWave()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, m_ShockwaveRadius, m_EnemyLayer);

        if (m_ShockWavePrefab != null)
        {
            GameObject shockwave = Instantiate(m_ShockWavePrefab, transform.position, Quaternion.identity);
            Destroy(shockwave, m_ShockwaveEffectDuration);
        }

        foreach (Collider2D enemy in enemies)
        {
            Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                Vector2 direction = (enemy.transform.position - transform.position).normalized;
                enemyRb.AddForce(direction * m_ShockwaveForce, ForceMode2D.Impulse);
                StartCoroutine(LimitVelocity(enemyRb));
            }
        }
    }

    private void UseLazer()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePos - m_LaserSpawnPoint.position).normalized;
        Vector3 spawnPosition = m_LaserSpawnPoint.position + direction * m_LazerOffset;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        mousePos.z = 0;
        currentLaser = Instantiate(m_LazerPrefab, spawnPosition, Quaternion.identity);
        currentLaser.transform.rotation = Quaternion.Euler(0, 0, angle);
        currentLaser.transform.parent = transform;
    }

    private IEnumerator PowerCooldownRoutine(int powerIndex)
    {
        m_PowerCooldowns[powerIndex] = true;
        yield return new WaitForSeconds(PowerCooldownDuration);
        m_PowerCooldowns[powerIndex] = false;
    }

    private IEnumerator LimitVelocity(Rigidbody2D enemyRb)
    {
        float maxVelocity = 5f;
        float slowDownTime = 1f;

        while (enemyRb.velocity.magnitude > maxVelocity)
        {
            enemyRb.velocity = Vector2.Lerp(enemyRb.velocity, Vector2.zero, Time.deltaTime / slowDownTime);
            yield return null;
        }
    }

}

