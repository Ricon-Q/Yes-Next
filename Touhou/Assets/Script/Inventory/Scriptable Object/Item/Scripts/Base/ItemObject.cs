using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Tool,
    Crop,
    Default
}
public abstract class ItemObject : ScriptableObject
{
    public int Id;
    public Sprite uiDisplay;
    public ItemType type;
    public bool countable;
    public int buyPrice;
    public int sellPrice;
    public bool craftable;
    public bool sellable;
    [TextArea(15, 20)] public string description;   
}

[System.Serializable]
public class Item
{
    public string Name;
    public ItemType type;
    public int Id;
    public int BuyPrice;
    public int SellPrice;
    public bool Sellable;
    public bool Craftable;
    public bool Countable;

    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.Id;
        BuyPrice = item.buyPrice;
        SellPrice = item.sellPrice;
        Sellable = item.sellable;
        Craftable = item.craftable;
        Countable = item.countable;

    }
}

