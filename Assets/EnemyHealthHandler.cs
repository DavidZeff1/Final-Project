using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthHandler : MonoBehaviour
{
    [SerializeField] private float m_EnemyHealth;
    [SerializeField] private EnemyDataScript m_EnemyData;
    public void Start()
    {
        m_EnemyHealth = m_EnemyData.GetEnemyHealth();
    }
    public float GetEnemyHealth()
    {
        return m_EnemyHealth;
    }

    public void SetEnemyHealth(float m_HealthToAdd)
    {
        m_EnemyHealth += m_HealthToAdd;
    }

}
