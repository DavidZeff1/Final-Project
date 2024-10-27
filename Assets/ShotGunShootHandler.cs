using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunShootHandler : MonoBehaviour
{
    [SerializeField] private GameObject m_bulletPrefab;
    [SerializeField] private Transform m_firePoint;
    [SerializeField] private float m_timeBetweenBarrages = 1f;  
    [SerializeField] private int m_numberOfBullets = 20;       
    [SerializeField] private float m_bulletSpeed = 5f;           

    private void Start()
    {
        StartCoroutine(ShootBurst());
    }

    private IEnumerator ShootBurst()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_timeBetweenBarrages);
            for (int i = 0; i < m_numberOfBullets; i++)
            {
                float angle = i * (360f / m_numberOfBullets);
                FireBullet(angle);
            }
        }
    }

    private void FireBullet(float angle)
    {
        GameObject bullet = Instantiate(m_bulletPrefab, m_firePoint.position, m_firePoint.rotation);
        Vector3 shootDirection = Quaternion.Euler(0, 0, angle) * Vector3.up;
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

