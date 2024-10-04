using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public event Action<InventoryItem, int> OnItemAdded;
    public event Action<InventoryItem, int> OnItemRemoved;

    private Dictionary<InventoryItem, int> inventory = new Dictionary<InventoryItem, int>();

    public void AddItem(InventoryItem item, int quantity = 1)
    {
        if (inventory.ContainsKey(item))
        {
            int newQuantity = inventory[item] + quantity;
            inventory[item] = Mathf.Min(newQuantity, item.m_MaxStack);
            quantity = newQuantity > item.m_MaxStack ? item.m_MaxStack - (newQuantity - quantity) : quantity;
        }
        else
        {
            inventory[item] = Mathf.Min(quantity, item.m_MaxStack);
        }

        OnItemAdded?.Invoke(item, quantity);
    }

    public void RemoveItem(InventoryItem item, int quantity = 1)
    {
        if (inventory.ContainsKey(item))
        {
            inventory[item] -= quantity;
            if (inventory[item] <= 0)
            {
                inventory.Remove(item);
            }
            OnItemRemoved?.Invoke(item, quantity);
        }
    }

    public IReadOnlyDictionary<InventoryItem, int> GetInventory()
    {
        return inventory;
    }
}
