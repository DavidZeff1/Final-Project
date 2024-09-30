using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void FixedUpdate()
    {
        m_DeltaX = 0;
        m_DeltaY = 0;

        GetHorizontalArrowKey();
        GetVerticalArrowKey();

        m_Movement = m_MovementSpeed * Time.deltaTime * new Vector2(m_DeltaX, m_DeltaY);
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

    private void GetVerticalArrowKey()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            m_DeltaY = 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            m_DeltaY = -1;
        }
    }
    private void GetHorizontalArrowKey()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_DeltaX = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            m_DeltaX = 1;
        }
    }
}

