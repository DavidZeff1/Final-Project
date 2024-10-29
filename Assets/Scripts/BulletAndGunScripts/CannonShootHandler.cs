using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShootHandler : MonoBehaviour
{
    [SerializeField] private GameObject m_bulletPrefab;
    [SerializeField] private Transform m_firePoint;
    [SerializeField] private float m_timeBetweenBarrages = 3f;
    [SerializeField] private float m_bulletSpeed = 5f;

    private void Start()
    {
        StartCoroutine(ShootContinuously());
    }

    private void Update()
    {
        AimTowardsMouse();
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

    public void DisableScript()
    {
        this.enabled = false;
    }

    public void EnableScript()
    {
        this.enabled = true;
    }
}
