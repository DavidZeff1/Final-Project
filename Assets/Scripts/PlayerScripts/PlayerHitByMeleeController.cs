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
    [SerializeField] private float m_damageCooldown = 0.1f;
    [SerializeField] float m_DamageFromMelee = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            HitByBullet(collision);
        }

        if (collision.CompareTag("Enemy") && m_CanTakeDamage)
        {
            HitByEnemyCollision();
        }

        if (collision.CompareTag("Enemy") && m_CanTakeDamage)
        {
            HitByEnemyCollision();
        }
    }

    private void HitByBullet(Collider2D collision)
    {
        GameObject Bullet = collision.gameObject;
        BulletDataScript bulletData = Bullet.GetComponent<BulletDataScript>();

        if (bulletData.GetBulletTarget().Equals("Player") && m_CanTakeDamage)
        {
            GameEventSystem.OnPlayerChangeHealth?.Invoke(-bulletData.GetBulletDamage());
            StartCoroutine(DamageCooldown());
            StartCoroutine(ChangeColorTemporarily());
        }
    }

    private void HitByEnemyCollision()
    {
        GameEventSystem.OnPlayerChangeHealth?.Invoke(-m_DamageFromMelee);
        StartCoroutine(DamageCooldown());
        StartCoroutine(ChangeColorTemporarily());
    }

    private IEnumerator DamageCooldown()
    {
        m_CanTakeDamage = false; 
        yield return new WaitForSeconds(m_damageCooldown);  
        m_CanTakeDamage = true;  
    }
    private IEnumerator ChangeColorTemporarily()
    {
        m_SpriteRenderer.color = Color.red; 
        yield return new WaitForSeconds(0.3f); 
        m_SpriteRenderer.color = m_OriginalColor;  
    }
}

