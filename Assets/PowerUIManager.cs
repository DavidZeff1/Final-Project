using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUIManager : MonoBehaviour
{
    [SerializeField] private PowerUseEvent m_PowerUseEvent; 
    [SerializeField] private Image[] m_PowerIcons; 

    private void OnEnable()
    {
        m_PowerUseEvent.OnPowerUsed += HandlePowerUse;
    }

    private void OnDisable()
    {
        m_PowerUseEvent.OnPowerUsed -= HandlePowerUse;
    }

    private void HandlePowerUse(int powerIndex)
    {
        StartCoroutine(PowerCooldownUIRoutine(powerIndex, 5f));
    }

    private IEnumerator PowerCooldownUIRoutine(int powerIndex, float duration)
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            m_PowerIcons[powerIndex].fillAmount = 1f - (timer / duration);
            yield return null;
        }

        m_PowerIcons[powerIndex].fillAmount = 1f;
    }
}


