using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    [SerializeField] private Character m_EnemyData; 
    private Transform m_PlayerTransform;
    private float m_MovementSpeed;

    private void Start()
    {
        m_MovementSpeed = m_EnemyData.movementSpeed;
        m_PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    private void Update()
    {
        if (m_PlayerTransform != null)
        {
            MoveTowardsPlayer();
        }
        
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (m_PlayerTransform.position - transform.position).normalized;

        transform.position += m_MovementSpeed * Time.deltaTime * direction;
    }
}

