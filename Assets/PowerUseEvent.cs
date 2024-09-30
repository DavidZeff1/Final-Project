using UnityEngine;

[CreateAssetMenu(fileName = "NewPowerUseEvent", menuName = "Events/Power Use Event")]
public class PowerUseEvent : ScriptableObject
{
    public delegate void PowerUseAction(int powerIndex);
    public event PowerUseAction OnPowerUsed;

    public void Raise(int powerIndex)
    {
        OnPowerUsed?.Invoke(powerIndex);
    }
}