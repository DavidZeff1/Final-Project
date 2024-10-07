using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayerAndMultiplyScript : MonoBehaviour
{
    [SerializeField] GameObject m_MiniEnemyPrefab;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 topPosition = transform.position + Vector3.up;
            Vector3 leftPosition = transform.position + Vector3.left;
            Vector3 rightPosition = transform.position + Vector3.right;

            Instantiate(m_MiniEnemyPrefab, topPosition, Quaternion.identity);
            Instantiate(m_MiniEnemyPrefab, leftPosition, Quaternion.identity);
            Instantiate(m_MiniEnemyPrefab, rightPosition, Quaternion.identity);

            Destroy(gameObject, 0.5f);
        }
        
    }
}
