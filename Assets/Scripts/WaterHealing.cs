using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class WaterHealing : MonoBehaviour
{
    [SerializeField] float m_HeatlhToAdd;
    [SerializeField] float m_HealingWait;
    private PlayerHealthController m_PlayerHealth;
    private float m_HealingTimer;

    private void Start()
    {
        m_PlayerHealth = FindObjectOfType<PlayerHealthController>();
        if (m_PlayerHealth == null)
        {
            Debug.Log("Player Health Controller Not Found");
        }
    }

    private void OnTriggerStay2D(Collider2D i_Other)
    {
        if (i_Other.CompareTag("Player"))
        {
            m_HealingTimer += Time.deltaTime;
            if (m_HealingTimer >= m_HealingWait)
            {
                m_PlayerHealth.SetPlayerHealth(m_HeatlhToAdd);
                m_HealingTimer = 0f;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D i_Other)
    {
        if (i_Other.CompareTag("Player"))
        {
            m_HealingTimer = 0;
        }
    }
}
