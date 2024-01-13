using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[ES3Serializable]
[System.Serializable]
public class InventorySlot
{
    [SerializeField] public InventoryItemData ItemData;
    [SerializeField] private int stackSize;

    // public InventoryItemData ItemData => itemData;
    public int StackSize => stackSize;

    public InventorySlot(InventoryItemData source, int amount)
    {
        ItemData = source;
        stackSize = amount;
    }

    public InventorySlot()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        ItemData = null;
        stackSize = -1;
    }

    public void AssignItem(InventorySlot invSlot)
    {
        if(ItemData == invSlot.ItemData) AddToStack(invSlot.stackSize);
        else
        {
            ItemData = invSlot.ItemData;
            stackSize = 0;
            AddToStack(invSlot.stackSize);
        }
    }

    public void UpdateInventorySlot(InventoryItemData data, int amount)
    {
        ItemData = data;
        stackSize = amount;
    }

    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining)
    {
        amountRemaining = ItemData.MaxStackSize - stackSize;
        return RoomLeftInStack(amountToAdd);
    }

    public bool RoomLeftInStack(int amountToAdd)
    {
        if(stackSize + amountToAdd <= ItemData.MaxStackSize) { return true; }
        else { return false; }
    }

    public void AddToStack(int amount)
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
        if(stackSize <= 0)
        {
            ClearSlot();
        }
    }

    public bool SplitStack(out InventorySlot splitStack)
    {
        if(stackSize <= 1)
        {
            splitStack = null;
            return false;
        }

        int halfStack = Mathf.RoundToInt(stackSize / 2);
        RemoveFromStack(halfStack);
        
        splitStack = new InventorySlot(ItemData, halfStack);
        return true;
    }

    public bool PickUpOneStack(out InventorySlot oneStack)
    {
        if(stackSize <= 1)
        {
            oneStack = null;
            return false;
        }

        RemoveFromStack(1);
        oneStack = new InventorySlot(ItemData, 1);
        return true;
    }
}
