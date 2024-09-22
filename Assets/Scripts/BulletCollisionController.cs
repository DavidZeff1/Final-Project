using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionController : MonoBehaviour
{
    BulletMovementController m_Controller;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_Controller = GetComponent<BulletMovementController>();


        // target hit by bullet
        if (collision.CompareTag(m_Controller.GetTarget()))
        {
            //destroy bullet
            Destroy(gameObject);
        }
    }
}
