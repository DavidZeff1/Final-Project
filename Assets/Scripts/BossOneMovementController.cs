using UnityEngine;

using UnityEngine;

public class BossOneMovementController : MonoBehaviour
{
    [SerializeField] private Character m_EnemyData;
    private float m_MovementSpeed;
    private Vector3 m_CurrentDirection;
    private float m_ReverseCooldown = 0.5f; 
    private float m_LastReversalTime;

    private void Start()
    {
        m_MovementSpeed = m_EnemyData.movementSpeed;
        SetRandomCardinalDirection();
    }

    private void Update()
    {
        MoveInCurrentDirection();
    }

    private void MoveInCurrentDirection()
    {
        transform.position += m_MovementSpeed * Time.deltaTime * m_CurrentDirection;
    }

    private void SetRandomCardinalDirection()
    {
        int directionIndex = UnityEngine.Random.Range(0, 4);

        switch (directionIndex)
        {
            case 0:
                m_CurrentDirection = Vector3.up; 
                break;
            case 1:
                m_CurrentDirection = Vector3.down; 
                break;
            case 2:
                m_CurrentDirection = Vector3.left; 
                break;
            case 3:
                m_CurrentDirection = Vector3.right; 
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BossBorder") && Time.time >= m_LastReversalTime + m_ReverseCooldown)
        {
            ReverseDirection();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BossBorder") && Time.time >= m_LastReversalTime + m_ReverseCooldown)
        {
            ReverseDirection();
        }
    }
    private void ReverseDirection()
    {
        m_CurrentDirection = -m_CurrentDirection;
        m_LastReversalTime = Time.time;
    }
}





