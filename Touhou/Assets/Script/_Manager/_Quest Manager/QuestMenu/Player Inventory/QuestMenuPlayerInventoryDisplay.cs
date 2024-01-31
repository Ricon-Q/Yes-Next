using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMenuPlayerInventoryDisplay : _DynamicInventoryDisplay
{
    public void EnterQuestMenu()
    {
        RefreshDynamicInventory(PlayerInventoryManager.Instance.playerInventory);
    }

    public void ExitQuestMenu()
    {
        
    }

    // override public void CreateInventorySlot()
    // {
    //     slotDictionary = new Dictionary<_InventorySlot_UI, _InventorySlot>();

    //     if(inventorySystem == null) return;

    //     for (int i = 0; i < 12 * inventorySystem.inventoryLevel; i++)
    //     {
    //         var uiSlot = Instantiate(slotPrefab, transform);
    //         slotDictionary.Add(uiSlot, inventorySystem.inventorySlots[i]);
    //         uiSlot.Init(inventorySystem.inventorySlots[i]);
    //         uiSlot.UpdateUISlot();
    //     }
    // }
}
