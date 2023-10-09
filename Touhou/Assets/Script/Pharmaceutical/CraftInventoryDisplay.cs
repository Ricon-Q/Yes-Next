using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftInventoryDisplay : InventoryDisplay
{
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
        AssignSlot(invToDisplay);
    }

    public virtual void CreateInventorySlot()
    {
        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

        if(inventorySystem == null) return;

        for (int i = 0; i < inventorySystem.InventorySize; i++)
        {
            var uiSlot = Instantiate(slotPrefab, transform);
            slotDictionary.Add(uiSlot, inventorySystem.InventorySlots[i]);
            uiSlot.Init(inventorySystem.InventorySlots[i]);
            uiSlot.UpdateUISlot();
            // if(inventorySystem.InventorySlots[i].ItemData.ItemType != ItemType.Ingredient) continue;
            // else if (inventorySystem.InventorySlots[i].ItemData.ItemType != ItemType.Medicine) continue;
            // else
            // {
            //     var uiSlot = Instantiate(slotPrefab, transform);
            //     slotDictionary.Add(uiSlot, inventorySystem.InventorySlots[i]);
            //     uiSlot.Init(inventorySystem.InventorySlots[i]);
            //     uiSlot.UpdateUISlot();
            // }
        }
    }

    public override void AssignSlot(InventorySystem invToDisplay)
    {
        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

        if(invToDisplay == null) return;

        for (int i = 0; i < invToDisplay.InventorySize; i++)
        {
            // if(invToDisplay.InventorySlots[i].ItemData.ItemType != ItemType.Ingredient) continue;
            // else if (invToDisplay.InventorySlots[i].ItemData.ItemType != ItemType.Medicine) continue;
            // else
            // {
            var uiSlot = Instantiate(slotPrefab, transform);
            slotDictionary.Add(uiSlot, invToDisplay.InventorySlots[i]);
            uiSlot.Init(invToDisplay.InventorySlots[i]);
            uiSlot.UpdateUISlot();
            // }
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
    }
    private void OnEnable()
    {
        RefreshDynamicInventory(this.inventorySystem);
    }

}
