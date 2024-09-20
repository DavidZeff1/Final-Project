using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataScript : MonoBehaviour
{
    [SerializeField] private Character m_PlayerData;
    public float GetEnemyDamage()
    {
        return m_PlayerData.damage;
    }
    public float GetEnemySpeed()
    {
        return m_PlayerData.movementSpeed;
    }
    public string GetEnemyName()
    {
        return m_PlayerData.characterName;
    }
    public float GetEnemyHealth()
    {
        return m_PlayerData.health;
    }
}
