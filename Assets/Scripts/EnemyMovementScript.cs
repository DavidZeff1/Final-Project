using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D m_RigidBody;
    [SerializeField] float m_Speed = 3;
    private Transform m_PlayerTransform;
    [SerializeField] Transform m_EnemyTransform;
    // Start is called before the first frame update
    void Start()
    {
        m_PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        //if the player is behind the enemy then the enemy needs to head "backwards"
        if (m_PlayerTransform.position.x < m_EnemyTransform.position.x)
        {
            m_Speed *= -1;
        }
    }

    // Update is called once per frame
    void Update()
    { 
        m_RigidBody.velocity = new Vector2(m_Speed, m_RigidBody.velocity.y);
    }
}
