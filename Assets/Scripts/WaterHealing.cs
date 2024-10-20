using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class WaterHealing : MonoBehaviour
{
    [SerializeField] float m_HeatlhToAdd = 5f;
    [SerializeField] float m_HealingWait = 0.5f;
    private float m_HealingTimer;

    private void OnTriggerStay2D(Collider2D i_Other)
    {
        if (i_Other.CompareTag("Player"))
        {
            m_HealingTimer += Time.deltaTime;
            if (m_HealingTimer >= m_HealingWait)
            {
                GameEventSystem.OnPlayerChangeHealth?.Invoke(m_HeatlhToAdd);
                m_HealingTimer = 0f;
                Debug.Log($"Player Healed for: {m_HeatlhToAdd}");
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
