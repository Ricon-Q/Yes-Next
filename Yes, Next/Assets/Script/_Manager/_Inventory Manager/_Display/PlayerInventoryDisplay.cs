using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInventoryDisplay : _DynamicInventoryDisplay
{
    override public void CreateInventorySlot()
    {
        slotDictionary = new Dictionary<_InventorySlot_UI, _InventorySlot>();

        if(inventorySystem == null) return;

        for (int i = 0; i < 12 * inventorySystem.inventoryLevel; i++)
        {
            var uiSlot = Instantiate(slotPrefab, transform);
            slotDictionary.Add(uiSlot, inventorySystem.inventorySlots[i]);
            uiSlot.Init(inventorySystem.inventorySlots[i]);
            uiSlot.UpdateUISlot();
        }
    }
    override public void RefreshDynamicInventory(_InventorySystem invToDisplay)
    {
        ClearSlots();
        inventorySystem = invToDisplay;
        if(inventorySystem != null) { inventorySystem.OnInventorySlotChanged += UpdateSlot; }
        AssignSlot(invToDisplay);

        // UiManager.Instance.inventoryHotBarDisplay.RefreshDynamicInventory(PlayerInventoryManager.Instance.playerInventory);
    }

    public void SlotRightClicked(_InventorySlot assignedSlot)
    {
        _itemDescription.UpdateDescription(assignedSlot);
    }

    [Header("Item Description")]
    [SerializeField] private ItemDescription _itemDescription;
    [Header("Money Panel")]
    [SerializeField] private TextMeshProUGUI _moneyText;

    public void UpdateMoneyText()
    {
        _moneyText.text = _PlayerManager.Instance.playerData.money.ToString("n0");
    }
    private void Update() 
    {
        UpdateMoneyText();
    }

    private void OnEnable()
    {
        
    }

    public void ClearMouseItem()
    {
        InventoryItemData tmp = PlayerInventoryManager.Instance.itemDataBase.Items[mouseInventoryItem.AssignedInventorySlot.itemId];
        if(tmp.ItemType == ItemType.KeyItem) PixelCrushers.DialogueSystem.DialogueManager.ShowAlert("버릴 수 없는 아이템 입니다.");
        else mouseInventoryItem.ClearSlot();
    }
}
