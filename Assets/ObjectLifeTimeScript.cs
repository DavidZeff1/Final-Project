using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLifeTimeScript : MonoBehaviour
{
    [SerializeField] private float m_LifeTimeInSeconds = 5;
    void Start()
    {
        Destroy(gameObject, m_LifeTimeInSeconds);
    }

    
}
