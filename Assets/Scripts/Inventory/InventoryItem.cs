using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName="Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public string m_ItemName;
    public Sprite m_SpriteIcon;
    public ItemType itemType;
    public int m_MaxStack = 1;
    public int effectAmount;  
    public float effectDuration;
    public GameObject weaponPrefab;
}

public enum ItemType
{
    HEALTH,
    SPEED_INCREASE,
    WEAPON
}
