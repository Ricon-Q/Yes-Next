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
    public int SellPrice;
    [TextArea(15, 20)] public string description;   
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    public bool Countable;
    public int BuyPrice;
    public int SellPrice;

    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.Id;
        Countable = item.countable;
        BuyPrice = item.buyPrice;
        SellPrice = item.SellPrice;
    }
}

