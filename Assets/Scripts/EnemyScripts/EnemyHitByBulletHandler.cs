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
        if (collision.CompareTag("Bullet"))
        {
            BulletDataScript bulletScript = collision.GetComponent<BulletDataScript>();
            if (bulletScript != null)
            {
                float bulletDamage = bulletScript.GetBulletDamage();

                m_EnemyHealthHandler.SetEnemyHealth(-bulletDamage);

                
                if (m_EnemyHealthHandler.GetEnemyHealth() <= 0)
                {
                    Destroy(gameObject); 
                }

                StartCoroutine(ChangeColorTemporarily());
            }
        }
    }
    private IEnumerator ChangeColorTemporarily()
    {
        m_SpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        m_SpriteRenderer.color = m_OriginalColor;
    }
}

