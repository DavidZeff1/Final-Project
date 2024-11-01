using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpPowerupController : MonoBehaviour
{
    [SerializeField] private float m_UpgradeShootingSpeed = 0.5f;
    [SerializeField] private float m_AmountOfHealthToAdd = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WeaponSpeedUpPotion"))
        {
            GameEventSystem.OnPlayerUpdateShootingInterval?.Invoke(m_UpgradeShootingSpeed);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("HealthPotion"))
        {
            GameEventSystem.OnPlayerChangeHealth?.Invoke(m_AmountOfHealthToAdd);          
            Destroy(collision.gameObject);
        }
    }
   
}

