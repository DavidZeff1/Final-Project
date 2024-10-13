using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GameEventManager : MonoBehaviour
{
    public static GameEventManager instance;

    public event Action OnBulletSpawnRateIncrease;
    public event Action OnPlayerMovementInversion;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TriggerBulletSpawnRateIncrease()
    {
        OnBulletSpawnRateIncrease?.Invoke();
    }

    public void TriggerPlayerMovementInversion()
    {
        OnPlayerMovementInversion?.Invoke();
    }
}

