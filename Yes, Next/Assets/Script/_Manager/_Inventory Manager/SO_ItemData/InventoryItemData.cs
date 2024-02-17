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
    public string tag;
    public Sprite Icon;
    public Sprite _previewSprite;
    public int MaxStackSize;
    
    public bool Sellable;
    public bool Buyable;
    public long SellPrice;
    public long BuyPrice;

    public virtual void Interact()
    {
    }

    public virtual void Interact(Vector3 _mousePosition)
    {

    }
    
    // Placeable Item Data 전용
    public virtual void Replaceable()
    {

    }

}