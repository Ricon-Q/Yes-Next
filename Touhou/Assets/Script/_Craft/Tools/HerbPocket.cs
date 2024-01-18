using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbPocket : _DynamicInventoryDisplay
{
    public void EnterCraftMode() 
    {
        _CraftManager.Instance.object_HerbPocket.SetActive(true);
        this.inventorySystem = PlayerInventoryManager.Instance.playerInventory;    
        RefreshDynamicInventory(this.inventorySystem);
    }
}
