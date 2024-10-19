using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventSystem
{
    public static Action<float> OnEnemyHit;   
    public static Action<float> OnPlayerChangeHealth;
    public static Action<GameObject> OnEnemyDeath;  
}
