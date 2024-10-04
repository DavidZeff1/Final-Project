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
            Debug.Log("Inventory field is empty");
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
