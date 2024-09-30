using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifespan : MonoBehaviour
{
    [SerializeField] float m_AmountOfTimeTolive = 5;
    void Start()
    {
        Destroy(gameObject, m_AmountOfTimeTolive);
    }
}
