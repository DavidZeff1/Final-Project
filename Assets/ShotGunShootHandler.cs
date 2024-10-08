using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunShootHandler : MonoBehaviour
{

    [SerializeField] private GameObject m_BulletPrefab;
    [SerializeField] private GameObject m_NoAimBulletPrefab;
    [SerializeField] private GameObject m_RightBulletPrefab;
    [SerializeField] private Transform m_GunTransform;
    [SerializeField] float m_ShootingInterval = 1f;
    [SerializeField] private string m_Target;

    private void Start()
    {
        InvokeRepeating(nameof(ShootAtTarget), m_ShootingInterval, m_ShootingInterval);
    }

    private void ShootAtTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(m_Target);

        if (targets.Length > 0)
        {
            GameObject Bullet = Instantiate(m_BulletPrefab, m_GunTransform.position, m_GunTransform.rotation);

            GameObject bullet2 = Instantiate(m_NoAimBulletPrefab, m_GunTransform.position, m_GunTransform.rotation);
            Vector3 shootDirection = Quaternion.Euler(0, 0, Random.Range(0, 360)) * Vector3.up;
            bullet2.GetComponent<Rigidbody2D>().velocity = shootDirection * 5f;

            GameObject bullet3= Instantiate(m_NoAimBulletPrefab, m_GunTransform.position, m_GunTransform.rotation);
            Vector3 shootDirection2 = Quaternion.Euler(0, 0, Random.Range(0, 360)) * Vector3.down;
            bullet3.GetComponent<Rigidbody2D>().velocity = shootDirection * 5f;


            Bullet.GetComponent<BulletMovementController>().SetTarget(m_Target);
        }
    }
    public void UpdateShootingInterval(float newInterval)
    {
        CancelInvoke(nameof(ShootAtTarget));
        m_ShootingInterval = newInterval;
        InvokeRepeating(nameof(ShootAtTarget), m_ShootingInterval, m_ShootingInterval);
    }
}
