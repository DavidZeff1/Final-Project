using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    [SerializeField] GameObject m_InventoryUI;
    void Start()
    {
        if (m_InventoryUI == null)
        {
            Debug.Log("ERROR! m_InventoryUI null");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            m_InventoryUI.SetActive(!m_InventoryUI.activeSelf);
        }
    }
}
