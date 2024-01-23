using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using UnityEngine.InputSystem;

public class _DynamicInventoryDisplay : _InventoryDisplay
{
    [SerializeField] protected _InventorySlot_UI slotPrefab;
    public ItemType itemType;

    public GameObject infoPanel;
    [SerializeField] private _InventorySlot infoSlot;
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

    public void RefreshDynamicInventory(_InventorySystem invToDisplay)
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
    // public virtual void ChangeCategory(string type)
    // {
    //     if(mouseInventoryItem.AssignedInventorySlot.itemId != -1) return;

    //     itemType = (ItemType) Enum.Parse(typeof(ItemType), type);
    //     if(itemType == ItemType.Ingredient || itemType == ItemType.Medicine)
    //     {
    //         // inventorySystem = medicalInventorySystem;
    //         // CreateInventorySlot();
    //         // RefreshDynamicInventory(inventorySystem);
    //     }
    //     else
    //     {
    //         // inventorySystem = backpackInventorySystem;
    //         CreateInventorySlot();
    //         RefreshDynamicInventory(inventorySystem);
    //     }
    //     foreach (var item in slotDictionary.Keys)
    //     {
    //         item.UpdateCategorySlot(item.AssignedInventorySlot, itemType);
    //     }
    // }
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
        if(value == 99) infoSlot.RemoveFromStack(infoSlot.stackSize);
        else infoSlot.RemoveFromStack(value);

        RefreshDynamicInventory(this.inventorySystem);
        infoPanel.SetActive(false);
        isInfoOpen = false;
    }

    public void ToggleInfo(_InventorySlot AssignedInventorySlot, bool rightClick)
    {
        if(rightClick)
        {
            if(!infoPanel) return;
            else if(infoPanel && AssignedInventorySlot.itemId != -1 && !isInfoOpen)
            {
                infoPanel.SetActive(true);
                isInfoOpen = true;
                infoSlot = AssignedInventorySlot;
                infoPanel.transform.position = Mouse.current.position.ReadValue();
                nameText.text = PlayerInventoryManager.Instance.itemDataBase.Items[AssignedInventorySlot.itemId].name;
                descriptionText.text = PlayerInventoryManager.Instance.itemDataBase.Items[AssignedInventorySlot.itemId].Description;
            }
            else if(infoPanel && AssignedInventorySlot.itemId != -1 && isInfoOpen)
            {
                infoSlot = AssignedInventorySlot;
                infoPanel.transform.position = Mouse.current.position.ReadValue();
                nameText.text = PlayerInventoryManager.Instance.itemDataBase.Items[AssignedInventorySlot.itemId].name;
                descriptionText.text = PlayerInventoryManager.Instance.itemDataBase.Items[AssignedInventorySlot.itemId].Description;
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
