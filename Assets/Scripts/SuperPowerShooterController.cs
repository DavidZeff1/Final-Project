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
    private const float PowerCooldownDuration = 5f;
    private bool[] m_PowerCooldowns = new bool[4];

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

                break;
        }

        m_PowerUseEvent?.Raise(powerIndex);
        StartCoroutine(PowerCooldownRoutine(powerIndex));
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

