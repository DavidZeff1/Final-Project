using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Score Channel", menuName = "Channels/Score", order = 0)]
public class ScoreChannel : ScriptableObject
{
    public event Action<int, string> UpdateScore;

    public void InvokeUpdateScore(int score, string targetName)
    {
        UpdateScore?.Invoke(score, targetName);
    }
}
