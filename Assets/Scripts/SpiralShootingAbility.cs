using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpiralShootingAbility : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab; 
    [SerializeField] private Transform firePoint; 
    [SerializeField] private float timeBetweenBarrages = 5f; 
    [SerializeField] private float timeBetweenBullets = 0.05f;
    [SerializeField] private int numberOfBullets = 20; 

    private void Start()
    {
        StartCoroutine(ShootSpiralBarrage());
    }

    private IEnumerator ShootSpiralBarrage()
    {
        while (true) 
        {
            yield return new WaitForSeconds(timeBetweenBarrages);

            for (int i = 0; i < numberOfBullets; i++)
            {
                float angle = i * (360f / numberOfBullets); 
                FireBullet(angle);

                yield return new WaitForSeconds(timeBetweenBullets); 
            }
        }
    }

    private void FireBullet(float angle)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Vector3 shootDirection = Quaternion.Euler(0, 0, angle) * Vector3.up;
        bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * 5f; 
    }
}

