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
            // Get the BulletDataScript from the colliding bullet
            BulletDataScript bulletScript = collision.GetComponent<BulletDataScript>();
            if (bulletScript != null)
            {
                // Get the bullet damage from the BulletDataScript
                float bulletDamage = bulletScript.GetBulletDamage();

                // Reduce enemy health
                m_EnemyHealthHandler.SetEnemyHealth(-bulletDamage);

                // Check if the enemy's health is less than or equal to 0
                if (m_EnemyHealthHandler.GetEnemyHealth() <= 0)
                {
                    Destroy(gameObject); // Destroy the enemy
                }

                // Start the color change coroutine
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

