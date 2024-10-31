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
        DontDestroyOnLoad(this);
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
            if (newQuantity > item.m_MaxStack)
            {
                quantity = item.m_MaxStack - (newQuantity - quantity);
            }
            
        }
        else
        {
            inventory[item] = Mathf.Min(quantity, item.m_MaxStack);
        }

        OnItemAdded?.Invoke(item, inventory[item]);  
    }

    public void RemoveItem(InventoryItem item, int quantity = 1)
    {
        if (inventory.ContainsKey(item))
        {
            inventory[item] -= quantity;
            if (inventory[item] <= 0)
            {
                inventory.Remove(item);
                OnItemRemoved?.Invoke(item, 0); 
            }
            else
            {
                OnItemRemoved?.Invoke(item, inventory[item]);  
            }
        }
    }

    public void UseItem(InventoryItem item)
    {
        if (inventory.ContainsKey(item))
        {
            RemoveItem(item, 1);  
        }
    }

    public IReadOnlyDictionary<InventoryItem, int> GetInventory()
    {
        return inventory;
    }

}
