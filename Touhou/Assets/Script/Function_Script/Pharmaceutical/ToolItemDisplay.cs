using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ToolItemDisplay : InventoryDisplay
{
    public MedicineCraftSystem medicineCraftSystem;
    [SerializeField] protected InventorySlot_UI slotPrefab;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Awake()
    {
        CreateInventorySlot();
    }

    public void RefreshDynamicInventory(InventorySystem invToDisplay)
    {
        ClearSlots();
        inventorySystem = invToDisplay;
        if(inventorySystem != null) { inventorySystem.OnInventorySlotChanged += UpdateSlot; }
        AssignItem();
        AssignSlot(invToDisplay);
    }
    public void UpdateItemData()
    {
        inventorySystem.InventorySlots[0].UpdateInventorySlot
            (medicineCraftSystem.mainItemData, medicineCraftSystem.mainItemDataAmount);
        
        inventorySystem.InventorySlots[1].UpdateInventorySlot
            (medicineCraftSystem.subItemData, medicineCraftSystem.subItemDataAmount);
        
        inventorySystem.InventorySlots[2].UpdateInventorySlot
            (medicineCraftSystem.resultItemData, medicineCraftSystem.resultItemDataAmount);

        RefreshDynamicInventory(this.InventorySystem);
    }

    public void AssignItem()
    {
        medicineCraftSystem.mainItemData = inventorySystem.InventorySlots[0].ItemData;
        medicineCraftSystem.mainItemDataAmount = inventorySystem.InventorySlots[0].StackSize;
        
        medicineCraftSystem.subItemData = inventorySystem.InventorySlots[1].ItemData;
        medicineCraftSystem.subItemDataAmount = inventorySystem.InventorySlots[1].StackSize;
        
        medicineCraftSystem.resultItemData = inventorySystem.InventorySlots[2].ItemData;
        medicineCraftSystem.resultItemDataAmount = inventorySystem.InventorySlots[2].StackSize;
    }

    public virtual void CreateInventorySlot()
    {
        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

        if(inventorySystem == null) return;

        for (int i = 0; i < inventorySystem.InventorySize; i++)
        {
            var uiSlot = Instantiate(slotPrefab, transform);
            if(i == 2) uiSlot.isCraftResultSlot = true;
            slotDictionary.Add(uiSlot, inventorySystem.InventorySlots[i]);
            uiSlot.Init(inventorySystem.InventorySlots[i]);
            uiSlot.UpdateUISlot();
            
        }
    }

    public override void AssignSlot(InventorySystem invToDisplay)
    {
        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

        if(invToDisplay == null) return;

        for (int i = 0; i < invToDisplay.InventorySize; i++)
        {
            var uiSlot = Instantiate(slotPrefab, transform);
            if(i == 2) uiSlot.isCraftResultSlot = true;
            slotDictionary.Add(uiSlot, invToDisplay.InventorySlots[i]);
            uiSlot.Init(invToDisplay.InventorySlots[i]);
            uiSlot.UpdateUISlot();
        }
    }

     private void ClearSlots()
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
        onSlotClicked -= AssignItem;
    }
    private void OnEnable()
    {
        RefreshDynamicInventory(this.inventorySystem);
        onSlotClicked += AssignItem;
    }
}
