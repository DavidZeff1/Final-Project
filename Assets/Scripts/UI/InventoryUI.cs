using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.PlayerLoop;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventorySlotPrefab;

    private readonly Dictionary<InventoryItem, GameObject> itemUIs = new Dictionary<InventoryItem, GameObject>();

    InventoryManager inventoryManager;

    private void OnEnable()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        if (inventoryManager != null)
        {
            inventoryManager.OnItemAdded += HandleItemAdded;
            inventoryManager.OnItemRemoved += HandleItemRemoved;
            RefreshUI();
        }
    }

    private void OnDisable()
    {
        if (inventoryManager != null)
        {
            inventoryManager.OnItemAdded -= HandleItemAdded;
            inventoryManager.OnItemRemoved -= HandleItemRemoved;
        }
    }

    private void HandleItemAdded(InventoryItem item, int quantity)
    {
        if (itemUIs.ContainsKey(item))
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
        if (itemUIs.ContainsKey(item))
        {
            UpdateItemUI(item, quantity);
        }
        
        RefreshUI();
    }

    private void AddNewItemUI(InventoryItem item, int quantity)
    {
        GameObject slot = Instantiate(inventorySlotPrefab, itemsParent);
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
        if (!itemUIs.ContainsKey(item))
        {
            itemUIs[item] = slot;
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
            inventoryManager.RemoveItem(item, 1);
            RefreshUI();
        }
        
        if (inventoryContains(item))
        {
            UpdateItemUI(item, inventoryManager.GetInventory()[item]);
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
                TextMeshProUGUI quantityText = itemUIs[item].transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                if (quantityText != null)
                {
                    quantityText.text = quantity.ToString();
                }

            }
            else
            {
                Destroy(itemUIs[item]);
                itemUIs.Remove(item);
            }
        }

    }
    
    private bool inventoryContains(InventoryItem item)
    {
        return InventoryManager.Instance.GetInventory().ContainsKey(item);
    }

    private void RefreshUI()
    {
        foreach (Transform child in itemsParent)
        {
            Destroy(child.gameObject);
        }

        itemUIs.Clear();

        foreach (var entry in inventoryManager.GetInventory())
        {
            AddNewItemUI(entry.Key, entry.Value);
        }
    }
}
