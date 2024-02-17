using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class _DynamicInventoryDisplay : _InventoryDisplay
{
    [Header("Slot Prefab")]
    [SerializeField] protected _InventorySlot_UI slotPrefab;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Awake()
    {
        CreateInventorySlot();
    }

    public virtual void RefreshDynamicInventory(_InventorySystem invToDisplay)
    {
        ClearSlots();
        inventorySystem = invToDisplay;
        if(inventorySystem != null) { inventorySystem.OnInventorySlotChanged += UpdateSlot; }
        AssignSlot(invToDisplay);
    }

    public virtual void CreateInventorySlot()
    {
        slotDictionary = new Dictionary<_InventorySlot_UI, _InventorySlot>();

        if(inventorySystem == null) return;

        for (int i = 0; i < inventorySystem.inventorySize; i++)
        {
            var uiSlot = Instantiate(slotPrefab, transform);
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
            var uiSlot = Instantiate(slotPrefab, transform);
            slotDictionary.Add(uiSlot, invToDisplay.inventorySlots[i]);
            uiSlot.Init(invToDisplay.inventorySlots[i]);
            uiSlot.UpdateUISlot();
        }
    }
    protected void ClearSlots()
    {
        foreach (var item in transform.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }

        if(slotDictionary != null) slotDictionary.Clear();
    }

    private void OnDisable()
    {
        if(inventorySystem != null) { inventorySystem.OnInventorySlotChanged -= UpdateSlot; }    
    }
    private void OnEnable()
    {
        RefreshDynamicInventory(this.inventorySystem);
    }
}
