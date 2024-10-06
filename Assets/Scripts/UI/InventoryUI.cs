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
        else
        {
            Debug.LogError("InventoryManager not found!");
        }
        //InventoryManager.Instance.OnItemAdded += HandleItemAdded;
        //InventoryManager.Instance.OnItemRemoved += HandleItemRemoved;
        //RefreshUI();
    }

    private void OnDisable()
    {
        //InventoryManager.Instance.OnItemAdded -= HandleItemAdded;
        //InventoryManager.Instance.OnItemRemoved -= HandleItemRemoved;
        inventoryManager.OnItemAdded -= HandleItemAdded;
        inventoryManager.OnItemRemoved -= HandleItemRemoved;

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
    }

    private void AddNewItemUI(InventoryItem item, int quantity)
    {  
        GameObject slot = Instantiate(inventorySlotPrefab, itemsParent);
        Image iconImage = slot.GetComponentInChildren<Image>();
        TextMeshProUGUI quantityText = slot.GetComponentInChildren<TextMeshProUGUI>();
        Button slotButton = slot.GetComponent<Button>();

        if (item == null)
        {
            Debug.LogError("Item is null in AddNewItemUI!");
            return;
        }

        if (iconImage == null)
        {
            Debug.LogError("Icon Image component is missing in the slot prefab!");
            return;
        }

        if (quantityText == null)
        {
            Debug.LogError("Text component for quantity is missing in the slot prefab!");
            return;
        }

        
        iconImage.sprite = item.m_SpriteIcon;
        //quantityText.text = quantity > 1 ? quantity.ToString() : ""; doesnt show text for 1 item stack
        quantityText.text = quantity.ToString();

        slotButton.onClick.AddListener(() => OnItemClicked(item));
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

            default:
                Debug.Log("Item type not handled.");
                break;
        }
    }
    private void UseHealthItem(InventoryItem item)
    {
        PlayerHealthController playerHealth = FindObjectOfType<PlayerHealthController>();
        if (playerHealth != null)
        {
            playerHealth.SetPlayerHealth(item.effectAmount);
            Debug.Log($"Used Health Item: Healed for {item.effectAmount}");
        }
    }

    private void UseSpeedIncreaseItem(InventoryItem item)
    {
        PlayerMovementController playerMovement = FindObjectOfType<PlayerMovementController>();
        if (playerMovement != null)
        {
            playerMovement.IncreaseSpeed(item.effectAmount, item.effectDuration);
            Debug.Log($"Used Speed Increase Item: Boosted speed by {item.effectAmount} for {item.effectDuration} seconds");
        }
    }

    private void UpdateItemUI(InventoryItem item, int quantity)
    {
        if (inventoryContains(item))
        {
            itemUIs[item].transform.Find("Quantity").GetComponent<TextMeshProUGUI>().text = quantity.ToString();
        }
        else
        {
            Destroy(itemUIs[item]);
            itemUIs.Remove(item);
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

        //itemUIs.Clear();

        foreach (var entry in inventoryManager.GetInventory())
        {
            AddNewItemUI(entry.Key, entry.Value);
        }
    }
}