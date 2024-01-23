using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Default,
    Food,
    Tool,
    Seed,
    Herb,
    Potion,
    Placeable,
    KeyItem
}

[CreateAssetMenu(menuName = "Inventory System/Inventory Item/Item Data")]

[ES3Serializable]
public class InventoryItemData : ScriptableObject 
{
    public int ID;
    public ItemType ItemType;
    public string DisplayName;
    [TextArea(4, 4)]
    public string Description;
    public Sprite Icon;
    public int MaxStackSize;
    
    public bool Shopable;
    public long SellPrice;
    public long BuyPrice;
}