using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBullet", menuName = "Bullet/BulletType")]
public class Bullet : ScriptableObject
{ 
    public string target;
    public float spawnRate;
    public float speed;
}
