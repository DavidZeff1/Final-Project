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
    [SerializeField] TextMeshProUGUI m_BossCountdownText;
    [SerializeField] HealthBar m_HealthBar;

    private void Start()
    {
        m_PlayerHealth = m_PlayerData.GetEnemyHealth();
        UpdateHealthText();
        m_HealthBar.SetMaxHealth(m_PlayerMaxHealth);

        GameEventSystem.OnPlayerChangeHealth += SetPlayerHealth;

    }
    private void OnDestroy()
    {
        GameEventSystem.OnPlayerChangeHealth -= SetPlayerHealth;
    }

    public float GetPlayerHealth()
    {
        return m_PlayerHealth;
    }

    public void SetPlayerHealth(float m_HealthToAdd)
    {
        m_PlayerHealth = ( ( m_PlayerHealth + m_HealthToAdd) > m_PlayerMaxHealth) ? m_PlayerMaxHealth : (m_PlayerHealth + m_HealthToAdd);

        if (m_PlayerHealth <= 0)
        {
            m_PlayerHealth = 0;
            PlayerDeath();
        }
     
        UpdateHealthText();
        m_HealthBar.SetSlider(m_PlayerHealth);
    }
    private void PlayerDeath()
    {
        m_BossCountdownText.text = "Player Died. Game Over!";
        m_BossCountdownText.color = Color.red;
        Destroy(this.gameObject, 1f);
    }

    private void UpdateHealthText()
    {
        m_PlayerHealthText.text = m_PlayerHealth.ToString();
    }
}
