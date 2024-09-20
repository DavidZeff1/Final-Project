using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovementController : MonoBehaviour
{
    [SerializeField] private BulletDataScript m_BulletData;
    [SerializeField] private float m_BulletSpeed; 
    [SerializeField] private Rigidbody2D m_BulletRB;    
    private Transform m_TargetEnemy;                    

    private void Start()
    {
        FindEnemy();
        m_BulletSpeed = m_BulletData.GetBulletSpeed();
        if (m_TargetEnemy != null)
        {
            Vector2 direction = (m_TargetEnemy.position - transform.position);
            m_BulletRB.velocity = direction * m_BulletSpeed;
        }
    }

    private void FindEnemy()
    {
        // find a enemy in the scene
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

        //do nothing if no enemies found
        if (enemy == null)
        {
            return;
        }
        //set the target
        m_TargetEnemy = enemy.transform;
        
    }
}

