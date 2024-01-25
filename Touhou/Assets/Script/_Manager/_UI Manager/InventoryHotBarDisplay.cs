using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InventoryHotBarDisplay : MonoBehaviour
{
    private int _hotBarLine = 0;
    public _InventorySystem _inventorySystem;

    [SerializeField] protected Dictionary<HotBarInventorySlot_Ui, _InventorySlot> slotDictionary;
    // public _InventorySystem InventorySystem => inventorySystem;
    public Dictionary<HotBarInventorySlot_Ui, _InventorySlot> SlotDictionary => slotDictionary;

    [SerializeField] private HotBarInventorySlot_Ui slotPrefab;

    private void Start() 
    {
        _inventorySystem = PlayerInventoryManager.Instance.playerInventory; 
        CreateInventorySlot();
        RefreshDynamicInventory(_inventorySystem);
    }

    private void Update() 
    {
        // RefreshDynamicInventory(_inventorySystem); 
    }


    public void CreateInventorySlot()
    {
        slotDictionary = new Dictionary<HotBarInventorySlot_Ui, _InventorySlot>();

        if(_inventorySystem == null) return;

        for (int i = 0; i < 12; i++)
        {
            var uiSlot = Instantiate(slotPrefab, transform);
            slotDictionary.Add(uiSlot, _inventorySystem.inventorySlots[i]);
            uiSlot.Init(_inventorySystem.inventorySlots[i]);
            uiSlot.UpdateUISlot();
            // _hotBarSelected._hotBarItems.Add(uiSlot.gameObject);
        }
    }

    public UnityAction<_InventorySlot> OnInventorySlotChanged;

    public void RefreshDynamicInventory(_InventorySystem invToDisplay)
    {
        ClearSlots();
        _inventorySystem = invToDisplay;
        if(_inventorySystem != null) { _inventorySystem.OnInventorySlotChanged += UpdateSlot; }
        AssignSlot(invToDisplay);
    }

    public void AssignSlot(_InventorySystem invToDisplay)
    {
        slotDictionary = new Dictionary<HotBarInventorySlot_Ui, _InventorySlot>();

        if(invToDisplay == null) return;

        for (int i = 0; i < 12; i++)
        {
            var uiSlot = Instantiate(slotPrefab, transform);
            slotDictionary.Add(uiSlot, invToDisplay.inventorySlots[i]);
            uiSlot.Init(invToDisplay.inventorySlots[i]);
            uiSlot.UpdateUISlot();
        }
    }

    public void ClearSlots()
    {
        foreach (var item in transform.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }

        if(slotDictionary != null) slotDictionary.Clear();
    }

    private void UpdateSlot(_InventorySlot updatedSlot)
    {
        foreach (var slot in SlotDictionary)
        {
            if(slot.Value == updatedSlot)   // slot value - the "Under the hood" Inventory slot.
            {
                slot.Key.UpdateUISlot(updatedSlot); // slot key - the UI representation of the value
            }
        }
    }
}
