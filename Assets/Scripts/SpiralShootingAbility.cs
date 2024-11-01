using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpiralShootingAbility : MonoBehaviour
{//basically the same as the shotGunShooter only here we delay each bullet to give it that spiral effect 
    [SerializeField] private GameObject m_bulletPrefab; 
    [SerializeField] private Transform m_firePoint; 
    [SerializeField] private float m_timeBetweenBarrages = 5f; 
    [SerializeField] private float m_timeBetweenBullets = 0.05f;
    [SerializeField] private float m_shootSpeed = 5f;
    [SerializeField] private int m_numberOfBullets = 20; 

    private void Start()
    {
        StartCoroutine(ShootSpiralBarrage());
    }

    private IEnumerator ShootSpiralBarrage()
    {
        while (true) 
        {
            yield return new WaitForSeconds(m_timeBetweenBarrages);

            for (int i = 0; i < m_numberOfBullets; i++)
            {
                FireBullet(i * (360f / m_numberOfBullets));

                yield return new WaitForSeconds(m_timeBetweenBullets); 
            }
        }
    }

    private void FireBullet(float angle)
    {
        GameObject bullet = Instantiate(m_bulletPrefab, m_firePoint.position, m_firePoint.rotation);
        Vector3 shootDirection = new(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0f);
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * m_shootSpeed; 
    }
}

