using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDesSlotDisplay : _DynamicInventoryDisplay
{
    protected override void Start()
    {
        inventorySystem = new _InventorySystem(1);
        CreateInventorySlot();
        
        RefreshDynamicInventory(this.inventorySystem);
    }

    public void ResetSlot()
    {
        if(inventorySystem.inventorySlots[0].itemId != -1)
        {
            PlayerInventoryManager.Instance.AddToInventory(inventorySystem.inventorySlots[0].itemId, inventorySystem.inventorySlots[0].stackSize);
            inventorySystem.inventorySlots[0].ClearSlot();
            RefreshDynamicInventory(this.inventorySystem);
            QuestManager.Instance._inventoryDisplay.RefreshDynamicInventory(QuestManager.Instance._inventoryDisplay.inventorySystem);
        }
        else
            return;     
    }
}
