using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_PlayerHealthText;
    [SerializeField] private float m_PlayerMaxHealth;
    [SerializeField] private float m_PlayerHealth;
    [SerializeField] private PlayerDataScript m_PlayerData;
    private void Start()
    {
        m_PlayerHealth = m_PlayerData.GetEnemyHealth();
        UpdateHealthText();
    }

    public float GetPlayerHealth()
    {
        return m_PlayerHealth;
    }

    public void SetPlayerHealth(float m_HealthToAdd)
    {
        UpdateHealthText();
        m_PlayerHealth= ( ( m_PlayerHealth + m_HealthToAdd) > m_PlayerMaxHealth) ? m_PlayerMaxHealth : (m_PlayerHealth + m_HealthToAdd);
    }

    private void UpdateHealthText()
    {
        m_PlayerHealthText.text = m_PlayerHealth.ToString();
    }
}
