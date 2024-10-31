using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateManagers : MonoBehaviour
{
    [SerializeField] GameObject m_ManagersPrefab;

    private void Awake()
    {
        var managers = FindObjectOfType<InventoryManager>();
        
        if (managers == null)
        {
            Instantiate(m_ManagersPrefab);
        }
        
        Destroy(gameObject);
    }
    
    
}
