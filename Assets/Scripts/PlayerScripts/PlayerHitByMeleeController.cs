using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHitByMeleeController : MonoBehaviour
{
    [SerializeField] private PlayerHealthController m_PlayerHealthHandler;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;
    private Color m_OriginalColor = Color.white;
    private bool m_CanTakeDamage = true; 
    [SerializeField] private float M_damageCooldown = 0.2f;
    [SerializeField] TextMeshProUGUI m_BossCountdownText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            GameObject Bullet = collision.gameObject;
            BulletDataScript bulletData = Bullet.GetComponent<BulletDataScript>();

            if(bulletData.GetBulletTarget().Equals("Player") && m_CanTakeDamage)
            {
                m_PlayerHealthHandler.SetPlayerHealth(-bulletData.GetBulletDamage());
                m_PlayerHealthHandler.GetPlayerHealth();

                if (m_PlayerHealthHandler.GetPlayerHealth() <= 0)
                {
                    PlayerDeath(gameObject);
                }

                StartCoroutine(DamageCooldown());
                StartCoroutine(ChangeColorTemporarily());
            }
        }
        if (collision.CompareTag("Enemy") && m_CanTakeDamage)
        {
            m_PlayerHealthHandler.SetPlayerHealth(-1);
            m_PlayerHealthHandler.GetPlayerHealth();

            if (m_PlayerHealthHandler.GetPlayerHealth() <= 0)
            {
                PlayerDeath(gameObject);
            }

            StartCoroutine(DamageCooldown());
            StartCoroutine(ChangeColorTemporarily());
        }
    }

    private void PlayerDeath(GameObject gameObject)
    {
        m_BossCountdownText.text = "Player Died. Game Over!";
        m_BossCountdownText.color = Color.red;
        Destroy(gameObject, 1f);
    }

    private IEnumerator DamageCooldown()
    {
        m_CanTakeDamage = false; 
        yield return new WaitForSeconds(M_damageCooldown);  
        m_CanTakeDamage = true;  
    }
    private IEnumerator ChangeColorTemporarily()
    {
        m_SpriteRenderer.color = Color.red; 
        yield return new WaitForSeconds(0.2f); 
        m_SpriteRenderer.color = m_OriginalColor;  
    }
}

