using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunShootHandler : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float timeBetweenBarrages = 1f;  
    [SerializeField] private int numberOfBullets = 20;       
    [SerializeField] private float bulletSpeed = 5f;           

    private void Start()
    {
        StartCoroutine(ShootBurst());
    }

    private IEnumerator ShootBurst()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenBarrages);
            for (int i = 0; i < numberOfBullets; i++)
            {
                float angle = i * (360f / numberOfBullets);
                FireBullet(angle);
            }
        }
    }

    private void FireBullet(float angle)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Vector3 shootDirection = Quaternion.Euler(0, 0, angle) * Vector3.up;
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletSpeed;
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

