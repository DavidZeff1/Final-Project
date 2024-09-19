using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] Character m_CharacterData;
    [SerializeField] float m_MovementSpeed;
    [SerializeField] private Rigidbody2D m_PlayerRigidBody;
    private void Start()
    {
        m_MovementSpeed = m_CharacterData.movementSpeed;
    }

    private void FixedUpdate()
    {
        m_PlayerRigidBody.velocity = ( new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) ) * m_MovementSpeed;
    }
}
