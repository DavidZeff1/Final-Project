using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BulletMovementController : MonoBehaviour
{
    [SerializeField] private string m_Target;
    [SerializeField] private BulletDataScript m_BulletData;
    [SerializeField] private float m_BulletSpeed; 
    [SerializeField] private Rigidbody2D m_BulletRB;    
    private Transform m_TargetTransform;                    

    private void Start()
    {
        FindTarget();
        m_BulletSpeed = m_BulletData.GetBulletSpeed();
        if (m_TargetTransform != null)
        {
            Vector2 direction = (m_TargetTransform.position - transform.position);
            m_BulletRB.velocity = direction * m_BulletSpeed;
        }
    }

    private void FindTarget()
    {
        // find a enemy in the scene
        GameObject target = GameObject.FindGameObjectWithTag(m_Target);

        //do nothing if no enemies found
        if (target == null)
        {
            return;
        }
        //set the target
        m_TargetTransform = target.transform;
        
    }
    public void SetTarget(string i_Target)
    {
        m_Target = i_Target;
    }
    public string GetTarget()
    {
        return m_Target;
    }
}

