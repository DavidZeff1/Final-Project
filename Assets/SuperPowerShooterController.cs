using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPowerShooterController : MonoBehaviour
{
    [SerializeField] private GameObject m_FireballPrefab;
    [SerializeField] private Transform m_SpawnPoint;
    [SerializeField] private PowerUseEvent m_PowerUseEvent; 
    private const float PowerCooldownDuration = 5f;
    private bool[] m_PowerCooldowns = new bool[4];

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !m_PowerCooldowns[0]) UsePower(0);
        if (Input.GetKeyDown(KeyCode.W) && !m_PowerCooldowns[1]) UsePower(1);
        if (Input.GetKeyDown(KeyCode.E) && !m_PowerCooldowns[2]) UsePower(2);
        if (Input.GetKeyDown(KeyCode.R) && !m_PowerCooldowns[3]) UsePower(3);
    }

    private void UsePower(int powerIndex)
    {
        if (m_FireballPrefab != null && m_SpawnPoint != null)
        {
            Instantiate(m_FireballPrefab, m_SpawnPoint.position, Quaternion.identity);
        }

        m_PowerUseEvent?.Raise(powerIndex);
        StartCoroutine(PowerCooldownRoutine(powerIndex));
    }

    private IEnumerator PowerCooldownRoutine(int powerIndex)
    {
        m_PowerCooldowns[powerIndex] = true;
        yield return new WaitForSeconds(PowerCooldownDuration);
        m_PowerCooldowns[powerIndex] = false;
    }
}

