using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CannonShootHandler : MonoBehaviour
{
    [SerializeField] private GameObject m_bulletPrefab;
    [SerializeField] private Transform m_firePoint;
    [SerializeField] private float m_timeBetweenBarrages = 3f;
    [SerializeField] private float m_bulletSpeed = 5f;
    private Vector2 m_OriginalScale;
    private Sprite m_Sprite;
    private void Start()
    {
        m_OriginalScale = transform.localScale;        
        m_Sprite = GetComponent<SpriteRenderer>().sprite;
        GameEventSystem.OnPlayerFlip += FlipSprite;
        StartCoroutine(ShootContinuously());
    }

    private void Update()
    {
        AimTowardsMouse();
    }

    private void OnDestroy()
    {
        GameEventSystem.OnPlayerFlip -= FlipSprite;
    }

    private IEnumerator ShootContinuously()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_timeBetweenBarrages);
            FireBullet();
        }
    }

    private void AimTowardsMouse()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = (mousePosition - (Vector2)m_firePoint.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        
        m_firePoint.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(m_bulletPrefab, m_firePoint.position, m_firePoint.rotation);
        Vector2 shootDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - m_firePoint.position).normalized;
        
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * m_bulletSpeed;
    }
    private void FlipSprite(bool i_FacingRight)
    {
        Vector2 newScale = m_OriginalScale;

        if (i_FacingRight)
        {
            newScale.x = Mathf.Abs(m_OriginalScale.x);
        }
        else
        {
            newScale.x = -Mathf.Abs(m_OriginalScale.x);
        }

        transform.localScale = newScale;
    }

    public void DisableScript()
    {
        this.enabled = false;
    }

    public void EnableScript()
    {
        this.enabled = true;
    }
}
