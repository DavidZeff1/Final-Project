using UnityEngine;

[CreateAssetMenu(fileName = "NewPowerUseEvent", menuName = "Events/Power Use Event")]
public class PowerUseEvent : ScriptableObject
{
    public delegate void PowerUseAction(int powerIndex);
    public event PowerUseAction OnPowerUsed;

    //whovever triggers this event needs to pass a int parameter ( m_PowerUseEvent?.Raise(i_PowerIndex); )
    //and whichever function is subscribed to this event ( m_PowerUseEvent.OnPowerUsed += HandlePowerUse; ) 
    //needs to take a int paramater ( private void HandlePowerUse(int powerIndex); ) 

    public void Raise(int powerIndex)
    {
        OnPowerUsed?.Invoke(powerIndex);
    }
}