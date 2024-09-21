using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpPowerupController : MonoBehaviour
{
    [SerializeField] private float m_UpgradeShootingSpeed = 0.5f;
    [SerializeField] private float m_AmountOfHealthToAdd = 1f;
    private ShootHandler m_ShootHandler;
    private PlayerHealthController m_HealthController;

    void Start()
    {
        m_ShootHandler = GetComponentInChildren<ShootHandler>();
        m_HealthController = GetComponent<PlayerHealthController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WeaponSpeedUpPotion"))
        {
            if (m_ShootHandler != null)
            {
                m_ShootHandler.UpdateShootingInterval(m_UpgradeShootingSpeed);
            }
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("HealthPotion"))
        {
            if (m_HealthController != null)
            {
                m_HealthController.SetPlayerHealth(m_AmountOfHealthToAdd);
            }
            Destroy(collision.gameObject);
        }
    }
   
}

