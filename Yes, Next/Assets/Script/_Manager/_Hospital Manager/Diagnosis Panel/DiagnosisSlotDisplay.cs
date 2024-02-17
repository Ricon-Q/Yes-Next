using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagnosisSlotDisplay : _DynamicInventoryDisplay
{
    [Header("Hospital Slot Prefab")]
    [SerializeField] protected DiagnosisSlot_Ui _diagnosisSlotPrefab;
    override public void CreateInventorySlot()
    {
        slotDictionary = new Dictionary<_InventorySlot_UI, _InventorySlot>();

        if(inventorySystem == null) return;

        for (int i = 0; i < inventorySystem.inventorySize; i++)
        {
            var uiSlot = Instantiate(_diagnosisSlotPrefab, transform);
            slotDictionary.Add(uiSlot, inventorySystem.inventorySlots[i]);
            uiSlot.Init(inventorySystem.inventorySlots[i]);
            uiSlot.UpdateUISlot();
        }
    }
    
    public override void AssignSlot(_InventorySystem invToDisplay)
    {
        slotDictionary = new Dictionary<_InventorySlot_UI, _InventorySlot>();

        if(invToDisplay == null) return;

        for (int i = 0; i < invToDisplay.inventorySize; i++)
        {
            var uiSlot = Instantiate(_diagnosisSlotPrefab, transform);
            slotDictionary.Add(uiSlot, invToDisplay.inventorySlots[i]);
            uiSlot.Init(invToDisplay.inventorySlots[i]);
            uiSlot.UpdateUISlot();
        }
    }
}
