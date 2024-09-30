using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOneMovementController : MonoBehaviour
{
    [SerializeField] private Character m_EnemyData;
    private Transform m_PlayerTransform;
    private Vector3 m_StartingPosition;
    private float m_MovementSpeed;
    private float m_DistanceToPlayerThreshold = 1.0f; 

    private void Start()
    {
        m_StartingPosition = transform.position;
        m_MovementSpeed = m_EnemyData.movementSpeed;
        m_PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (m_PlayerTransform != null)
        {
            MoveTowardsPlayer();

            if (Vector3.Distance(transform.position, m_PlayerTransform.position) <= m_DistanceToPlayerThreshold)
            {
                TeleportBackToStart();
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (m_PlayerTransform.position - transform.position).normalized;
        transform.position += m_MovementSpeed * Time.deltaTime * direction;
    }

    private void TeleportBackToStart()
    {
        transform.position = m_StartingPosition;
    }
}

