using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        // enemy hit by bullet
        if (collision.CompareTag("Enemy"))
        {
            //destroy bullet
            Destroy(gameObject);
        }
    }
}
