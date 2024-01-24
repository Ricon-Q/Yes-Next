using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionStand : _DynamicInventoryDisplay
{
    public void EnterCraftMode()
    {
        _CraftManager.Instance.object_PotionStand.SetActive(true);
        this.inventorySystem = PlayerInventoryManager.Instance.potionInventory;    
        RefreshDynamicInventory(this.inventorySystem);
    }
    public void ExitToolMode()
    {
        _CraftManager.Instance.object_PotionStand.SetActive(false);
    }
}
