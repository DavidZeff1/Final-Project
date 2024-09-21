using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] Character m_CharacterData;
    [SerializeField] float m_MovementSpeed = 300f;
    [SerializeField] private Rigidbody2D m_PlayerRigidBody;
    [SerializeField] Animator m_Animator;
    private Vector2 m_Movement;
    private float m_DeltaX;
    private float m_DeltaY;


    private void Start()
    {
        //m_MovementSpeed = m_CharacterData.movementSpeed;
    }

    private void FixedUpdate()
    {
        m_DeltaX = Input.GetAxis(nameof(InputStr.Horizontal));
        m_DeltaY = Input.GetAxis(nameof(InputStr.Vertical));
        
        m_Movement = new Vector2(m_DeltaX, m_DeltaY) * m_MovementSpeed * Time.deltaTime;

        //m_PlayerRigidBody.velocity = (m_Movement) * m_MovementSpeed;       
        m_PlayerRigidBody.velocity = m_Movement;

        if (m_Movement.magnitude > 0)
        {
            m_Animator.SetBool(nameof(AnimationParams.IsMoving), true);
        }
        else
        {
            m_Animator.SetBool(nameof(AnimationParams.IsMoving), false);
        }

    }

    public enum InputStr
    {
        Horizontal,
        Vertical
    }
    
    enum AnimationParams
    {
        IsMoving
    }
}
