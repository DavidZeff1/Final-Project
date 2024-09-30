using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitByBulletHandler : MonoBehaviour
{
    [SerializeField] private EnemyHealthHandler m_EnemyHealthHandler;
    [SerializeField] private SpriteRenderer m_SpriteRenderer; 
    private Color m_OriginalColor = Color.white;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject Bullet = collision.gameObject;
        BulletDataScript bulletData = Bullet.GetComponent<BulletDataScript>();

        if (collision.CompareTag("Bullet") && bulletData != null && bulletData.GetBulletTarget().Equals("Enemy"))
        {
            if (bulletData != null)
            {
                m_EnemyHealthHandler.SetEnemyHealth(-bulletData.GetBulletDamage());

                if (m_EnemyHealthHandler.GetEnemyHealth() <= 0)
                {
                    Destroy(gameObject); 
                }

                StartCoroutine(ChangeColorTemporarily(Color.red));
            }
        }
        if (collision.CompareTag("Fireball"))
        {
            if (collision.TryGetComponent<BulletDataScript>(out var bulletScript))
            {
                m_EnemyHealthHandler.SetEnemyHealth(-bulletData.GetBulletDamage());

                if (m_EnemyHealthHandler.GetEnemyHealth() <= 0)
                {
                    Destroy(gameObject);
                }

                StartCoroutine(ChangeColorTemporarily(Color.blue));
            }
        }
    }
    private IEnumerator ChangeColorTemporarily(Color i_Color)
    {
        m_SpriteRenderer.color = i_Color;
        yield return new WaitForSeconds(0.1f);
        m_SpriteRenderer.color = m_OriginalColor;
    }
}

