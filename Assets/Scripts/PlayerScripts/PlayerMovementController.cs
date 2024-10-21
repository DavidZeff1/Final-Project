using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] Character m_CharacterData;
    [SerializeField] float m_NormalMovementSpeed = 300f;
    [SerializeField] private Rigidbody2D m_PlayerRigidBody;
    [SerializeField] Animator m_Animator;
    private float m_CurrentMovementSpeed;
    private Vector2 m_Movement;
    private float m_DeltaX;
    private float m_DeltaY;
    private BoxCollider2D m_BoxCollider;
    private Vector3 m_OriginalScale;

    private void Start()
    {
        m_BoxCollider = GetComponent<BoxCollider2D>();
        m_OriginalScale = transform.localScale;
        m_CurrentMovementSpeed = m_NormalMovementSpeed;
        GameEventSystem.OnPlayerChangeSpeed += IncreaseSpeed;
        GameEventSystem.OnPlayerShrink += ShrinkPlayer;
    }

    private void FixedUpdate()
    {
        m_DeltaX = 0;
        m_DeltaY = 0;

        GetHorizontalArrowKey();
        GetVerticalArrowKey();

        m_Movement = m_CurrentMovementSpeed * Time.deltaTime * new Vector2(m_DeltaX, m_DeltaY);
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

    private void OnDestroy()
    {
        GameEventSystem.OnPlayerChangeSpeed -= IncreaseSpeed;
        GameEventSystem.OnPlayerShrink -= ShrinkPlayer;

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

    public void IncreaseSpeed(float i_SpeedBoost, float i_Duration)
    {
        m_CurrentMovementSpeed += i_SpeedBoost;
        StartCoroutine(ResetSpeedAfterDuration(i_Duration));
    }

    public void ShrinkPlayer(float i_ScaleChange, float i_SizeChangeTime)
    {
        transform.localScale = m_OriginalScale * i_ScaleChange;
        if (m_BoxCollider != null)
        {
            m_BoxCollider.size *= i_ScaleChange;
        }

        StartCoroutine(ResetSizeAfterDelay(i_SizeChangeTime));

    }

    private IEnumerator ResetSizeAfterDelay(float i_SizeChangeTime)
    {
        yield return new WaitForSeconds(i_SizeChangeTime);

        transform.localScale = m_OriginalScale;
    }

    public void ResetSize()
    {
        transform.localScale = m_OriginalScale;
        if (m_BoxCollider != null)
        {
            m_BoxCollider.size /= m_OriginalScale.magnitude;
        }
    }

    private IEnumerator ResetSpeedAfterDuration(float i_Duration)
    {
        yield return new WaitForSeconds(i_Duration);
        m_CurrentMovementSpeed = m_NormalMovementSpeed;
    }

}

