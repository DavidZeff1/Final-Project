using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D m_RigidBody;
    [SerializeField] float m_Speed = 3;
    [SerializeField] float m_JumpForce = 5f;
    private bool m_IsOnGround;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsOnGround)
        {
            m_RigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * m_Speed, m_RigidBody.velocity.y);

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                m_RigidBody.AddForce(new Vector2(0f, m_JumpForce), ForceMode2D.Impulse);
            }
        }
        else
        {
            m_RigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * (m_Speed) / 2, m_RigidBody.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_RigidBody.gravityScale *= -1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            m_IsOnGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            m_IsOnGround = false;
        }
    }
}
