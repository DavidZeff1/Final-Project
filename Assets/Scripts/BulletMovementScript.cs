using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletMovementScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D m_RigidBody;
    [SerializeField] float m_Speed = 3;
    private Transform m_TargetTransform;

    public void SetTarget(string targetTag)
    {
        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);
        if (targetObject != null)
        {
            m_TargetTransform = targetObject.transform;
            Vector2 direction = m_TargetTransform.position - transform.position;
            m_RigidBody.velocity = direction * m_Speed;
        }
    }

    void Start()
    {
        Destroy(gameObject, 5f);
    }
}




