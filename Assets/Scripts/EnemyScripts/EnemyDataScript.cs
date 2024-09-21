using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDataScript : MonoBehaviour
{
    [SerializeField] private Character m_EnemyData;
    public float GetEnemyDamage()
    {
        return m_EnemyData.damage;
    }
    public float GetEnemySpeed()
    {
        return m_EnemyData.movementSpeed;
    }
    public string GetEnemyName()
    {
        return m_EnemyData.characterName;
    }
    public float GetEnemyHealth()
    {
        return m_EnemyData.health;
    }
}
