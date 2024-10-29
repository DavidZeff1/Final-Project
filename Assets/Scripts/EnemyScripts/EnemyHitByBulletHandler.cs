using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class EnemyHitByBulletHandler : MonoBehaviour
{
    [SerializeField] private EnemyHealthHandler m_EnemyHealthHandler;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;
    [SerializeField] private GameObject m_ExplosionEffect;
    [SerializeField] private float m_ExplosionRadius = 3f;
    [SerializeField] private int m_ExplosionDamage = 30;
    private Color m_OriginalColor = Color.white;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject Bullet = collision.gameObject;
        BulletDataScript bulletData = Bullet.GetComponent<BulletDataScript>();

        if (collision.CompareTag("Bullet") && bulletData != null && bulletData.GetBulletTarget().Equals("Enemy"))
        {
            HandleBulletHit(bulletData, Color.red);
        }
        else if (collision.CompareTag("Fireball") && collision.TryGetComponent<BulletDataScript>(out var bulletScript))
        {
            HandleBulletHit(bulletScript, Color.blue);
        }
        else if (collision.CompareTag("BulletCannon") && bulletData != null && bulletData.GetBulletTarget().Equals("Enemy"))
        {
            TriggerExplosion(bulletData, Color.yellow);
        }
    }

    private void HandleBulletHit(BulletDataScript bulletData, Color hitColor)
    {
        if (bulletData != null)
        {
            Instantiate(m_ExplosionEffect, transform.position, Quaternion.identity);

            GameEventSystem.OnEnemyHit?.Invoke(this, -bulletData.GetBulletDamage());
            StartCoroutine(ChangeColorTemporarily(hitColor));
        }
    }

    private void TriggerExplosion(BulletDataScript bulletData, Color hitColor)
    {
        Instantiate(m_ExplosionEffect, transform.position, Quaternion.identity);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, m_ExplosionRadius);
        foreach (Collider2D nearbyObject in colliders)
        {
            EnemyHitByBulletHandler enemyHandler = nearbyObject.GetComponent<EnemyHitByBulletHandler>();
            if (enemyHandler != null)
            {
                GameEventSystem.OnEnemyHit?.Invoke(enemyHandler, -m_ExplosionDamage);
                StartCoroutine(enemyHandler.ChangeColorTemporarily(hitColor));
            }
        }

        Destroy(bulletData.gameObject);
    }

    private IEnumerator ChangeColorTemporarily(Color i_Color)
    {
        if (m_SpriteRenderer == null) 
        { 
            yield break;
        }

        m_SpriteRenderer.color = i_Color;
        
        yield return new WaitForSeconds(0.1f);
        
        if (m_SpriteRenderer != null)
        {
            m_SpriteRenderer.color = m_OriginalColor;
        }
    }
}



