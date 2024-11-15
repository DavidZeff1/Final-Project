using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider m_Slider;
    [SerializeField] Gradient m_Gradient;
    [SerializeField] Image m_Fill;

    private void Start()
    {
        GameEventSystem.OnPlayerSetMaxHealth += SetMaxHealth;
        GameEventSystem.OnPlayerSetSliderHealth += SetSlider;
    }

    private void OnDestroy()
    {
        GameEventSystem.OnPlayerSetMaxHealth -= SetMaxHealth;
        GameEventSystem.OnPlayerSetSliderHealth -= SetSlider;
    }

    public void SetMaxHealth(float i_MaxHealth)
    {
        m_Slider.maxValue = i_MaxHealth;        
        m_Fill.color = m_Gradient.Evaluate(m_Slider.normalizedValue);
    }

    public void SetSlider(float i_Health)
    {
        m_Slider.value = i_Health;
        m_Fill.color = m_Gradient.Evaluate(m_Slider.normalizedValue);
    }
}
    