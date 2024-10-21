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
    public float m_EffectAmount;  
    public float m_EffectDuration;
    public GameObject m_WeaponPrefab;
}

public enum ItemType
{
    HEALTH,
    SPEED_INCREASE,
    WEAPON,
    SIZE_MANIPULATOR
}
