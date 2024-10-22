using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHealthHandler : MonoBehaviour
{
    [SerializeField] private float m_EnemyHealth;
    [SerializeField] private EnemyDataScript m_EnemyData;
    [SerializeField] private bool m_IsBoss;
    private TMP_Text m_CountdownText;

    private void Start()
    {
        if (m_IsBoss)
        {
            m_CountdownText = GameObject.Find("BossCountdownText")?.GetComponent<TMP_Text>();
        }

        GameEventSystem.OnEnemyHit += SetEnemyHealth;
        m_EnemyHealth = m_EnemyData.GetEnemyHealth();

    }

    private void OnDestroy()
    {
        GameEventSystem.OnEnemyHit -= SetEnemyHealth;
    }

    public float GetEnemyHealth()
    {
        return m_EnemyHealth;
    }

    public void SetEnemyHealth(EnemyHitByBulletHandler enemyHit, float m_HealthToAdd)
    {
        if (enemyHit == GetComponent<EnemyHitByBulletHandler>())
        {
            m_EnemyHealth += m_HealthToAdd;
            if (m_EnemyHealth <= 0)
            {
                HandleEnemyDeath();
            }
        }
    }

    private void HandleEnemyDeath()
    {
        Destroy(gameObject);

        if (m_IsBoss)
        {
            if (m_CountdownText != null)
            {
                m_CountdownText.text = "Boss Killed, Level Completed!\nWill Be Transitioning to next level in a few seconds";
                m_CountdownText.color = Color.red;
                GameEventSystem.OnEnemyBossDeath?.Invoke();
            }

        }
    }

}
