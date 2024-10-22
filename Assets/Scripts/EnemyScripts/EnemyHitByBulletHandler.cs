using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

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
            HandleBulletHit(bulletData, Color.red);
        }
        else if (collision.CompareTag("Fireball") && collision.TryGetComponent<BulletDataScript>(out var bulletScript))
        {
            HandleBulletHit(bulletScript, Color.blue);
        }
    }
    private void HandleBulletHit(BulletDataScript bulletData, Color hitColor)
    {
        if (bulletData != null)
        {
            GameEventSystem.OnEnemyHit?.Invoke(this, -bulletData.GetBulletDamage());
            //Debug.Log("hit");
            /*m_EnemyHealthHandler.SetEnemyHealth(-bulletData.GetBulletDamage());

            if (m_EnemyHealthHandler.GetEnemyHealth() <= 0)
            {
                HandleEnemyDeath();
            }*/

            StartCoroutine(ChangeColorTemporarily(hitColor));
        }
    }

    /*private void HandleEnemyDeath()
    {
        Debug.Log("dead");
        Destroy(gameObject);

        if (m_IsBoss)
        {
            if (m_CountdownText != null)
            {
                m_CountdownText.text = "Boss Killed, Level Completed!\nWill Be Transitioning to next level in a few seconds";
                m_CountdownText.color = Color.red;
            }

        }
    }*/
    

    private IEnumerator ChangeColorTemporarily(Color i_Color)
    {
        m_SpriteRenderer.color = i_Color;
        yield return new WaitForSeconds(0.1f);
        m_SpriteRenderer.color = m_OriginalColor;
    }
}



