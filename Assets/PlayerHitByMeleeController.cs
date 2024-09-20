using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitByMeleeController : MonoBehaviour
{
    [SerializeField] private PlayerHealthController m_PlayerHealthHandler;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;
    private Color m_OriginalColor = Color.white;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Enemy"))
        {
            m_PlayerHealthHandler.SetPlayerHealth(-1);
            m_PlayerHealthHandler.GetPlayerHealth();
            if (m_PlayerHealthHandler.GetPlayerHealth() <= 0)
            {
                 Destroy(gameObject); 
            }
            StartCoroutine(ChangeColorTemporarily());
        }
    }
    private IEnumerator ChangeColorTemporarily()
    {
        m_SpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        m_SpriteRenderer.color = m_OriginalColor;
    }
}
