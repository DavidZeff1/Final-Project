using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeWeaponDataScript : MonoBehaviour
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

}
