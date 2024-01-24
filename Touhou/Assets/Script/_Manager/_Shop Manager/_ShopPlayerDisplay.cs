using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class _ShopPlayerDisplay : _DynamicInventoryDisplay
{
    [Header("Shop Mode")]
    [SerializeField] private _SellDisplay sellDisplay;

    // 중앙 가격 text
    [SerializeField] private TextMeshProUGUI sellPriceText;
    // 현재 소지금 text
    [SerializeField] private TextMeshProUGUI currentMoneyText;
    // Total 가격 text
    [SerializeField] private TextMeshProUGUI totalPriceText;
    private _PlayerManager playerManager;
    public long totalSellPrice;

    protected override void Start() 
    {
        playerManager = _PlayerManager.Instance;
    }
    public void EnterShopMode()
    {
        // inventorySystem.SaveInventory();
        totalSellPrice = 0;
        UpdatePriceText();
        RefreshDynamicInventory(PlayerInventoryManager.Instance.playerInventory);
    }

    public void ExitShopMode()
    {
        // Reset();
        // inventorySystem.LoadInventory();
        // RefreshDynamicInventory(PlayerInventoryManager.Instance.playerInventory);
    }

    public void ConfirmDeal()
    {
        totalSellPrice = 0;
        // inventorySystem.SaveInventory();
        UpdatePriceText();
        this.RefreshDynamicInventory(this.inventorySystem);
    }

    public override void SlotClicked(_InventorySlot_UI clickedUISlot)
    {
        // Slot이 빈칸이 아니고, Slot의 아이템이 Shopable일 경우 실행
        if(clickedUISlot.AssignedInventorySlot.itemId != -1 && PlayerInventoryManager.Instance.itemDataBase.Items[clickedUISlot.AssignedInventorySlot.itemId].Shopable)
        {
            totalSellPrice += PlayerInventoryManager.Instance.itemDataBase.Items[clickedUISlot.AssignedInventorySlot.itemId].SellPrice;
            UpdatePriceText();

            sellDisplay.inventorySystem.AddToInventory(clickedUISlot.AssignedInventorySlot.itemId, 1);
            clickedUISlot.AssignedInventorySlot.RemoveFromStack(1);
            this.RefreshDynamicInventory(this.inventorySystem);
            sellDisplay.RefreshDynamicInventory(sellDisplay.inventorySystem);
        }   
    }

    public void UpdatePriceText()
    {
        currentMoneyText.text = playerManager.playerData.money.ToString("n0");
        sellPriceText.text = totalSellPrice.ToString("n0");
        totalPriceText.text = "+" + totalSellPrice.ToString("n0");
    }

    public void Reset()
    {
        totalSellPrice = 0;
        // inventorySystem.LoadInventory();
        // RefreshDynamicInventory(this.inventorySystem);
        UpdatePriceText();
    }
}
