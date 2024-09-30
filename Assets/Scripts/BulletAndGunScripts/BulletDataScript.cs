using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDataScript : MonoBehaviour
{
    [SerializeField] private BulletDataSO m_BulletData;
    public float GetBulletDamage()
    {
        return m_BulletData.damageAmount;
    }
    public float GetBulletSpeed()
    {
        return m_BulletData.speed;
    }
    public string GetBulletName()
    {
        return m_BulletData.bulletName;
    }
    public string GetBulletTarget()
    {
        return m_BulletData.target;
    }
}
