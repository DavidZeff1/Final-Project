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
    private Vector2 m_OriginalScale;
    private Vector2 m_FlipScale;
    private Vector2 m_ShrinkScale;

    private void Start()
    {
        m_BoxCollider = GetComponent<BoxCollider2D>();
        m_OriginalScale = transform.localScale;
        m_FlipScale = new Vector2(m_OriginalScale.x, m_OriginalScale.y);
        m_ShrinkScale = new Vector2(1, 1);
        m_CurrentMovementSpeed = m_NormalMovementSpeed;
        GameEventSystem.OnPlayerChangeSpeed += IncreaseSpeed;
        GameEventSystem.OnPlayerShrink += ShrinkPlayer;
    }

    private void Update()
    {
        m_DeltaX = 0;
        m_DeltaY = 0;

        GetHorizontalArrowKey();
        GetVerticalArrowKey();
    }

    private void FixedUpdate()
    {
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
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            m_DeltaY = 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            m_DeltaY = -1;
        }
    }
    
    private void GetHorizontalArrowKey()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            m_DeltaX = -1;
            FlipSprite(false);
            GameEventSystem.OnPlayerFlip?.Invoke(false);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            m_DeltaX = 1;
            FlipSprite(true);
            GameEventSystem.OnPlayerFlip?.Invoke(true);
        }
    }

    public void IncreaseSpeed(float i_SpeedBoost, float i_Duration)
    {
        m_CurrentMovementSpeed += i_SpeedBoost;
        StartCoroutine(ResetSpeedAfterDuration(i_Duration));
    }

    public void ShrinkPlayer(float i_ScaleChange, float i_SizeChangeTime)
    {
        m_ShrinkScale = new Vector2(i_ScaleChange, i_ScaleChange);
        if (m_BoxCollider != null)
        {
            m_BoxCollider.size *= i_ScaleChange;
        }

        ApplyCombinedScale();
        StartCoroutine(ResetSizeAfterDelay(i_SizeChangeTime));

    }

    private IEnumerator ResetSizeAfterDelay(float i_SizeChangeTime)
    {
        yield return new WaitForSeconds(i_SizeChangeTime);

        m_ShrinkScale = new Vector2(1, 1);
        if (m_BoxCollider != null)
        {
            m_BoxCollider.size /= m_ShrinkScale.x;
        }

        ApplyCombinedScale();
    }

    private IEnumerator ResetSpeedAfterDuration(float i_Duration)
    {
        yield return new WaitForSeconds(i_Duration);
        m_CurrentMovementSpeed = m_NormalMovementSpeed;
    }

    private void FlipSprite(bool i_FacingRight)
    {
        if (i_FacingRight)
        {
            m_FlipScale.x = Mathf.Abs(m_OriginalScale.x);
        }
        else
        {
            m_FlipScale.x = -Mathf.Abs(m_OriginalScale.x);
        }

        ApplyCombinedScale();
    }

    private void ApplyCombinedScale()
    {
        transform.localScale = new Vector3(m_FlipScale.x * m_ShrinkScale.x, m_FlipScale.y * m_ShrinkScale.y, 1);
    }
}

