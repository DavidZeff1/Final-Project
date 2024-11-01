using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_PlayerHealthText;
    [SerializeField] private float m_PlayerMaxHealth = 100f;
    [SerializeField] private float m_PlayerHealth;
    [SerializeField] private PlayerDataScript m_PlayerData;
    [SerializeField] TextMeshProUGUI m_BossCountdownText;

    private void Start()
    {
        UpdateHealthText();
        GameEventSystem.OnPlayerSetMaxHealth?.Invoke(m_PlayerMaxHealth);
        GameEventSystem.OnPlayerChangeHealth += SetPlayerHealth;
    }

    private void OnDestroy()
    {
        GameEventSystem.OnPlayerChangeHealth -= SetPlayerHealth;
    }

    public void SetPlayerHealth(float m_HealthToAdd)
    {
        m_PlayerHealth = ( ( m_PlayerHealth + m_HealthToAdd) > m_PlayerMaxHealth) ? m_PlayerMaxHealth : (m_PlayerHealth + m_HealthToAdd);
        if (m_PlayerHealth <= 0)
        {
            m_PlayerHealth = 0;
            PlayerDeath();
        }
       
        GameEventSystem.OnPlayerSetSliderHealth?.Invoke(m_PlayerHealth);
        UpdateHealthText();
    }

    private void PlayerDeath()
    {
        m_BossCountdownText.text = "Player Died. Game Over!";
        m_BossCountdownText.color = Color.red;
        Destroy(this.gameObject, 1f);
        SceneManager.LoadScene("GameOverScene");
    }

    private void UpdateHealthText()
    {
        m_PlayerHealthText.text = m_PlayerHealth.ToString();
    }
}
