using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerMovement : MonoBehaviour
{
    // Start is called before the first frame updat
    [SerializeField] Transform m_CameraObjectTransform;
    [SerializeField] Transform m_PlayerObjectTransform;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //m_CameraObjectTransform.position = new Vector3(m_PlayerObjectTransform.position.x, m_PlayerObjectTransform.position.y, m_CameraObjectTransform.position.z);
    }
}
