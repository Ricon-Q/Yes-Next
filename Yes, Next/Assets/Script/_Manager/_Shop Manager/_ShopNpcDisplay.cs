using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class _ShopNpcDisplay : _DynamicInventoryDisplay
{
    public _InventorySystem emptyInventorySystem;

    // 중앙 상단에 해당하는 InventorySystem
    public _BuyDisplay buyDisplay;

    // 중앙 상단 구매 물건의 총합
    public TextMeshProUGUI priceText;

    // 거래 진행시 팝업 창에 나오는 구매 가격의 총합
    public TextMeshProUGUI totalBuyPriceText;
    public long totalBuyPrice;

    protected override void Start()
    {
        // this.inventorySystem = emptyInventorySystem;
        // CreateInventorySlot();
    }

    public void EnterShopMode(_InventorySystem inventorySystem)
    {
        totalBuyPrice = 0;
        this.inventorySystem = inventorySystem;
        CreateInventorySlot();
        RefreshDynamicInventory(this.inventorySystem);
        UpdatePriceText();
    }

    public override void CreateInventorySlot()
    {
        slotDictionary = new Dictionary<_InventorySlot_UI, _InventorySlot>();

        if(inventorySystem == null) return;

        for (int i = 0; i < inventorySystem.inventorySize; i++)
        {
            if(inventorySystem.inventorySlots[i].itemId != -1) continue;
            var uiSlot = Instantiate(slotPrefab, transform);
            slotDictionary.Add(uiSlot, inventorySystem.inventorySlots[i]);
            uiSlot.Init(inventorySystem.inventorySlots[i]);
            uiSlot.UpdateUISlot();
        }
    }
    public override void SlotClicked(_InventorySlot_UI clickedUISlot)
    {
        if(clickedUISlot.AssignedInventorySlot.itemId != -1) 
        { 
            if(buyDisplay.inventorySystem.HasFreeSlot(out _InventorySlot freeSlot))
            {
                buyDisplay.inventorySystem.AddToInventory(clickedUISlot.AssignedInventorySlot.itemId, 1);
                buyDisplay.RefreshDynamicInventory(buyDisplay.inventorySystem);
                
                totalBuyPrice += PlayerInventoryManager.Instance.itemDataBase.Items[clickedUISlot.AssignedInventorySlot.itemId].BuyPrice;
                UpdatePriceText();
            }
        }
    }
    public void UpdatePriceText()
    {
        priceText.text = totalBuyPrice.ToString("n0");
        totalBuyPriceText.text = "-" + totalBuyPrice.ToString("n0");
    }
    public void Reset()
    {
        totalBuyPrice = 0;
        UpdatePriceText();
    }
    public void ConfirmDeal(long remindPrice)
    {
        totalBuyPrice = remindPrice;
        UpdatePriceText();
    }
}
