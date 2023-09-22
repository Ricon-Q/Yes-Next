using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.EventSystems;

public class DynamicInventoryDisplay : InventoryDisplay
{
    [SerializeField] protected InventorySlot_UI slotPrefab;
    public ItemType itemType;

    public GameObject infoPanel;
    [SerializeField] private InventorySlot infoSlot;
    public bool isInfoOpen;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Awake()
    {
        CreateInventorySlot();
        if(infoPanel) infoPanel.SetActive(false);
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
        }
    }
    public virtual void ChangeCategory(string type)
    {
        itemType = (ItemType) Enum.Parse(typeof(ItemType), type);
        foreach (var item in slotDictionary.Keys)
        {
            item.UpdateCategorySlot(item.AssignedInventorySlot, itemType);
        }
    }
    public override void AssignSlot(InventorySystem invToDisplay)
    {
        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

        if(invToDisplay == null) return;

        for (int i = 0; i < invToDisplay.InventorySize; i++)
        {
            var uiSlot = Instantiate(slotPrefab, transform);
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
    }
    private void OnEnable()
    {
        RefreshDynamicInventory(this.inventorySystem);
    }

    public void InfoRemoveItem(int value)
    {
        if(value == 99) infoSlot.RemoveFromStack(infoSlot.StackSize);
        else infoSlot.RemoveFromStack(value);

        RefreshDynamicInventory(this.inventorySystem);
        infoPanel.SetActive(false);
        isInfoOpen = false;
    }

    public void ToggleInfo(InventorySlot AssignedInventorySlot, bool rightClick)
    {
        if(rightClick)
        {
            if(!infoPanel) return;
            else if(infoPanel && AssignedInventorySlot.ItemData && !isInfoOpen)
            {
                infoPanel.SetActive(true);
                isInfoOpen = true;
                infoSlot = AssignedInventorySlot;
                infoPanel.transform.position = Mouse.current.position.ReadValue();
                nameText.text = AssignedInventorySlot.ItemData.name;
                descriptionText.text = AssignedInventorySlot.ItemData.Description;
            }
            else if(infoPanel && AssignedInventorySlot.ItemData && isInfoOpen)
            {
                infoSlot = AssignedInventorySlot;
                infoPanel.transform.position = Mouse.current.position.ReadValue();
                nameText.text = AssignedInventorySlot.ItemData.name;
                descriptionText.text = AssignedInventorySlot.ItemData.Description;
            }
            else if(infoPanel && isInfoOpen) 
            {
                infoPanel.SetActive(false);
                isInfoOpen = false;
            }
        }
        else
        {
           if(infoPanel && isInfoOpen) 
           {
                infoPanel.SetActive(false);
                isInfoOpen = false;
           }
        }
    }
}
