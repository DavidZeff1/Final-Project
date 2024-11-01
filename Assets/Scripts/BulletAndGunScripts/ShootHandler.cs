using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootHandler : MonoBehaviour
{
    [SerializeField] private GameObject m_BulletPrefab;   
    [SerializeField] private Transform m_GunTransform;    
    [SerializeField] float m_ShootingInterval = 1f;
    [SerializeField] private string m_Target;

    private void Start()
    {
        GameEventSystem.OnPlayerUpdateShootingInterval += UpdateShootingInterval;
        InvokeRepeating(nameof(ShootAtTarget), m_ShootingInterval, m_ShootingInterval);
    }

    private void OnDestroy()
    {
        GameEventSystem.OnPlayerUpdateShootingInterval -= UpdateShootingInterval;
    }

    private void ShootAtTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(m_Target);

        if (targets.Length > 0)
        {
          GameObject Bullet =  Instantiate(m_BulletPrefab, m_GunTransform.position, m_GunTransform.rotation);
          Bullet.GetComponent<BulletMovementController>().SetTarget(m_Target);
        }
    }
    public void UpdateShootingInterval(float newInterval)
    {
        CancelInvoke(nameof(ShootAtTarget));
        m_ShootingInterval = newInterval;
        InvokeRepeating(nameof(ShootAtTarget), m_ShootingInterval, m_ShootingInterval);
    }

    public void DisableScript()
    {
        this.enabled = false;
    }

    public void EnableScript()
    {
        this.enabled = true;
    }
}


