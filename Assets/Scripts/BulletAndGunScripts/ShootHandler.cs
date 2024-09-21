using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootHandler : MonoBehaviour
{
    [SerializeField] private GameObject m_BulletPrefab;   
    [SerializeField] private Transform m_GunTransform;    
    [SerializeField] float m_ShootingInterval = 1f;                

    private void Start()
    {
        InvokeRepeating(nameof(ShootAtEnemies), m_ShootingInterval, m_ShootingInterval);
    }

    private void ShootAtEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length > 0)
        {
           Instantiate(m_BulletPrefab, m_GunTransform.position, m_GunTransform.rotation);
        }
    }
    public void UpdateShootingInterval(float newInterval)
    {
        CancelInvoke(nameof(ShootAtEnemies));
        m_ShootingInterval = newInterval;
        InvokeRepeating(nameof(ShootAtEnemies), m_ShootingInterval, m_ShootingInterval);
    }
}


