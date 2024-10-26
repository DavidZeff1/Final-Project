
using UnityEngine;


public class CameraController : MonoBehaviour
{
    [SerializeField] Transform m_PlayerTransform; 
    [SerializeField] Transform m_CameraTransform;
    private void Update()
    {
        if(m_PlayerTransform != null)
        {
            m_CameraTransform.position = new Vector3(m_PlayerTransform.position.x, m_PlayerTransform.position.y, -10);
        }
    }
}

