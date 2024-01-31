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
}
