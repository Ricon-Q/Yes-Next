using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Default,
    Food,
    Ingredient,
    Medicine,
    Tool,
    Cloth,
    Farm
}

[CreateAssetMenu(menuName = "Inventory System/Inventory Item/Item Data")]
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