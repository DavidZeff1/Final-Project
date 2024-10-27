using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.PlayerLoop;

public class InventoryUI : MonoBehaviour
{
    public Transform m_itemsParent;
    public GameObject m_inventorySlotPrefab;

    private readonly Dictionary<InventoryItem, GameObject> m_itemUIs = new Dictionary<InventoryItem, GameObject>();

    InventoryManager m_inventoryManager;

    private void OnEnable()
    {
        m_inventoryManager = FindObjectOfType<InventoryManager>();
        if (m_inventoryManager != null)
        {
            m_inventoryManager.OnItemAdded += HandleItemAdded;
            m_inventoryManager.OnItemRemoved += HandleItemRemoved;
            RefreshUI();
        }
    }

    private void OnDisable()
    {
        if (m_inventoryManager != null)
        {
            m_inventoryManager.OnItemAdded -= HandleItemAdded;
            m_inventoryManager.OnItemRemoved -= HandleItemRemoved;
        }
    }

    private void HandleItemAdded(InventoryItem item, int quantity)
    {
        if (m_itemUIs.ContainsKey(item))
        {
            UpdateItemUI(item, quantity);
        }
        else
        {
            AddNewItemUI(item, quantity);
        }
    }
    
    private void HandleItemRemoved(InventoryItem item, int quantity)
    {
        if (m_itemUIs.ContainsKey(item))
        {
            UpdateItemUI(item, quantity);
        }
        
        RefreshUI();
    }

    private void AddNewItemUI(InventoryItem item, int quantity)
    {
        GameObject slot = Instantiate(m_inventorySlotPrefab, m_itemsParent);
        Image iconImage = slot.GetComponentInChildren<Image>();
        TextMeshProUGUI quantityText = slot.GetComponentInChildren<TextMeshProUGUI>();
        Button slotButton = slot.GetComponent<Button>();

        if (item == null || iconImage == null || quantityText == null)
        {
            Debug.LogError("ERROR MISSING ITEM/ICONIMAGE/QUANTITYTEXT IN InventoryUI");
            return;
        }

        iconImage.sprite = item.m_SpriteIcon;
        quantityText.text = quantity.ToString();
        if (item.itemType == ItemType.WEAPON) 
        { 
            iconImage.transform.localScale = new Vector2(2f, 2f);
        }

        slotButton.onClick.AddListener(() => OnItemClicked(item));
        if (!m_itemUIs.ContainsKey(item))
        {
            m_itemUIs[item] = slot;
        }
    }
    
    private void OnItemClicked(InventoryItem item)
    {
        switch (item.itemType)
        {
            case ItemType.HEALTH:
                UseHealthItem(item);
                break;
            case ItemType.SPEED_INCREASE:
                UseSpeedIncreaseItem(item);
                break;
            case ItemType.WEAPON:
                UseWeaponOnPlayer(item);
                break;
            case ItemType.SIZE_MANIPULATOR:
                UseSizeManipulator(item);
                break;
        }

        if (item.itemType != ItemType.WEAPON)
        {
            m_inventoryManager.RemoveItem(item, 1);
            RefreshUI();
        }
        
        if (inventoryContains(item))
        {
            UpdateItemUI(item, m_inventoryManager.GetInventory()[item]);
        }
        else
        {
            RefreshUI();
        }
    }

    private void UseHealthItem(InventoryItem item)
    {
        GameEventSystem.OnPlayerChangeHealth?.Invoke(item.m_EffectAmount); 
    }

    private void UseSpeedIncreaseItem(InventoryItem item)
    {
        GameEventSystem.OnPlayerChangeSpeed?.Invoke(item.m_EffectAmount, item.m_EffectDuration);
    }

    private void UseWeaponOnPlayer(InventoryItem item)
    {
        if (item.m_WeaponPrefab == null)
        {
            Debug.LogError("ERROR! item.weaponPrefab NULL");
            return;
        }

        PlayerWeaponSlot weaponSlot = FindObjectOfType<PlayerWeaponSlot>();

        if (weaponSlot != null)
        {
            weaponSlot.EquipWeapon(item.m_WeaponPrefab);
        }

    }
    
    private void UseSizeManipulator(InventoryItem item)
    {
        GameEventSystem.OnPlayerShrink.Invoke(item.m_EffectAmount, item.m_EffectDuration);
        GameEventSystem.OnPlayerChangeSpeed?.Invoke(200, item.m_EffectDuration);
    }

    private void UpdateItemUI(InventoryItem item, int quantity)
    {
        if (inventoryContains(item) && item.itemType != ItemType.WEAPON)
        {
            if (quantity > 0)
            {
                TextMeshProUGUI quantityText = m_itemUIs[item].transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                if (quantityText != null)
                {
                    quantityText.text = quantity.ToString();
                }

            }
            else
            {
                Destroy(m_itemUIs[item]);
                m_itemUIs.Remove(item);
            }
        }

    }
    
    private bool inventoryContains(InventoryItem item)
    {
        return InventoryManager.Instance.GetInventory().ContainsKey(item);
    }

    private void RefreshUI()
    {
        foreach (Transform child in m_itemsParent)
        {
            Destroy(child.gameObject);
        }

        m_itemUIs.Clear();

        foreach (var entry in m_inventoryManager.GetInventory())
        {
            AddNewItemUI(entry.Key, entry.Value);
        }
    }
}
