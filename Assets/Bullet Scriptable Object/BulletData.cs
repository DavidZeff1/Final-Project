using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBulletData", menuName = "Bullet/BulletData")]
public class BulletData : ScriptableObject
{
    public string bulletName;
    public float damageAmount;
    public float speed;
}
