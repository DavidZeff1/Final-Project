using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionController : MonoBehaviour
{
    BulletDataScript m_BulletDataS;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_BulletDataS = GetComponent<BulletDataScript>();

        if (collision.CompareTag(m_BulletDataS.GetBulletTarget()))
        {
            Destroy(gameObject);
        }
    }
}
