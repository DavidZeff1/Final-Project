using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPowerShooterController : MonoBehaviour
{
    //whovever triggers this event needs to pass a int parameter ( m_PowerUseEvent?.Raise(i_PowerIndex); )
    //and whichever function is subscribed to this event ( m_PowerUseEvent.OnPowerUsed += HandlePowerUse; ) 
    //needs to take a int paramater ( private void HandlePowerUse(int powerIndex); ) 

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
    [SerializeField] private float m_SlowMotionFactor = 0.4f;
    [SerializeField] private float m_SlowMotionDuration = 4f;
    private const float PowerCooldownDuration = 5f;
    private bool[] m_PowerCooldowns = new bool[4];
    private GameObject m_CurrentLaser;

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKey(KeyCode.Q)) && !m_PowerCooldowns[0])
        {
            UsePower(0);
        }

        if ((Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKey(KeyCode.R)) && !m_PowerCooldowns[1])
        {
            UsePower(1);
        }

        if ((Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKey(KeyCode.E)) && !m_PowerCooldowns[2])
        {
            UsePower(2);
        }

        if ((Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKey(KeyCode.F)) && !m_PowerCooldowns[3])
        {
            UsePower(3);
        } 
    }

    private void UsePower(int i_PowerIndex)
    {
        switch (i_PowerIndex)
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
            
            case 3:
                StartCoroutine(SlowTime());
                break;
        }
        //trigger the m_PowerUseEvent 
        m_PowerUseEvent?.Raise(i_PowerIndex);
        StartCoroutine(PowerCooldownRoutine(i_PowerIndex));
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
        m_CurrentLaser = Instantiate(m_LazerPrefab, spawnPosition, Quaternion.identity);
        m_CurrentLaser.transform.rotation = Quaternion.Euler(0, 0, angle);
        m_CurrentLaser.transform.parent = transform;
        m_CurrentLaser.transform.position = new Vector3(spawnPosition.x, spawnPosition.y, 0);
    }

    private IEnumerator PowerCooldownRoutine(int powerIndex)
    {
        m_PowerCooldowns[powerIndex] = true;
        yield return new WaitForSeconds(PowerCooldownDuration);
        m_PowerCooldowns[powerIndex] = false;
    }

    private IEnumerator SlowTime()
    {
        Time.timeScale = m_SlowMotionFactor;

        yield return new WaitForSecondsRealtime(m_SlowMotionDuration);

        Time.timeScale = 1f;

    }
}

