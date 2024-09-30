using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBulletData", menuName = "Bullet/BulletDataScript")]
public class BulletDataSO : ScriptableObject
{
    public string bulletName;
    public string target;
    public float damageAmount;
    public float speed;

}
