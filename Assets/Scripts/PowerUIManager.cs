using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUIManager : MonoBehaviour
{
    //whoever has the m_PowerUseEvent scriptable object can subscribe to the OnPowerUsed event
    //(also can trigger the event by doing: m_PowerUseEvent?.Raise(i_PowerIndex);)
    //so in order to subscribe or trigger the event we need the scriptable object

    //whovever triggers this event needs to pass a int parameter ( m_PowerUseEvent?.Raise(i_PowerIndex); )
    //and whichever function is subscribed to this event ( m_PowerUseEvent.OnPowerUsed += HandlePowerUse; ) 
    //needs to take a int paramater ( private void HandlePowerUse(int powerIndex); ) 


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
        //images are type radial thats why they fill up in a circular shape.
        
        while (timer < duration)
        {
            timer += Time.deltaTime;
            m_PowerIcons[powerIndex].fillAmount = 1f - (timer / duration);
            //yield return null is necsasary so that the ui would update properly 
            //if wasnt co routine the while loop would finish within a frame and the ui will fill immediatley 
            //so it will look like no effect took place
            yield return null;
        }

        m_PowerIcons[powerIndex].fillAmount = 1f;
    }
}


