using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    [SerializeField] GameObject m_MiniEnemyPrefab;
    private void Start()
    {
        InvokeRepeating(nameof(InsMinions), 1f, 4f);
    }
    
    private void InsMinions()
    {
        Vector3 topPosition = transform.position + Vector3.up;
        Vector3 leftPosition = transform.position + Vector3.left;
        Vector3 rightPosition = transform.position + Vector3.right;

        Instantiate(m_MiniEnemyPrefab, topPosition, Quaternion.identity);
        Instantiate(m_MiniEnemyPrefab, leftPosition, Quaternion.identity);
        Instantiate(m_MiniEnemyPrefab, rightPosition, Quaternion.identity);
    }

}
