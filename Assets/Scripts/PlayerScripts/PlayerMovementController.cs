using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] Character m_CharacterData;
    [SerializeField] float m_MovementSpeed;
    [SerializeField] private Rigidbody2D m_PlayerRigidBody;
    [SerializeField] Animator m_Animator;
    private Vector2 m_Movement;
    private void Start()
    {
        m_MovementSpeed = m_CharacterData.movementSpeed;
    }

    private void Update()
    {
        m_Movement.x = Input.GetAxisRaw(nameof(InputStr.Horizontal));
        m_Movement.y = Input.GetAxisRaw(nameof(InputStr.Vertical));
        //m_Animator.SetFloat(nameof(AnimationParams.MoveX), m_Movement.x);
        //m_Animator.SetFloat(nameof(AnimationParams.MoveY), m_Movement.y);
        
    }

    private void FixedUpdate()
    {
        m_Animator.SetFloat(nameof(AnimationParams.MoveX), m_Movement.x);
        m_Animator.SetFloat(nameof(AnimationParams.MoveY), m_Movement.y);

        m_PlayerRigidBody.velocity = (m_Movement) * m_MovementSpeed;       
    }

    public enum InputStr
    {
        Horizontal,
        Vertical
    }
    enum AnimationParams
    {
        MoveX,
        MoveY
    }
}
