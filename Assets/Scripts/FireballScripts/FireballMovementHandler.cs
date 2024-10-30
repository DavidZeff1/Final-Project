using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballMovementHandler : MonoBehaviour
{
    [SerializeField] private string m_Target;
    [SerializeField] private float m_FireballSpeed;
    [SerializeField] private Rigidbody2D m_FireballRB;
    private Transform m_TargetTransform;

    private void Start()
    {
        FindTarget();
        if (m_TargetTransform != null)
        {
            Vector2 direction = (m_TargetTransform.position - transform.position).normalized;
            m_FireballRB.velocity = direction * m_FireballSpeed;
            //calculate the rotation based on the direction and then covert radians to degrees
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

    }

    private void FindTarget()
    {
        GameObject target = GameObject.FindGameObjectWithTag(m_Target);
        if (target != null)
        {
            m_TargetTransform = target.transform;
        }
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

