using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

// 상인의 인벤토리이다. 캔버스 상으로 좌측에 해당
public class ShopNpcDisplay : DynamicInventoryDisplay
{
    // 상인의 인벤토리
    // EnterShopMode시 Parameter가 해당 변수로 할당
    // 해당 스크립트에서는 InventoryDisplay스크립트의 InventorySystem inventorySystem다.

    public InventorySystem emptyInventorySystem;

    // 중앙 상단에 해당하는 InventorySystem
    public BuyDisplay buyDisplay;

    // 중앙 상단 구매 물건의 총합
    public TextMeshProUGUI priceText;
    // 거래 진행시 팝업 창에 나오는 구매 가격의 총합
    public TextMeshProUGUI totalBuyPriceText;
    public long totalBuyPrice;
    

    // public ShopMiddleDisplay 
    
    protected override void Start()
    {
        this.inventorySystem = emptyInventorySystem;
        CreateInventorySlot();
    }

    public void EnterShopMode(InventorySystem inventorySystem)
    {
        totalBuyPrice = 0;
        this.inventorySystem = inventorySystem;
        RefreshDynamicInventory(this.inventorySystem);
        UpdatePriceText();
        // CreateInventorySlot();
    }
    public void ExitShopMode()
    {
        // inventory.Clear();
    }

    private void Update()
    {
        // DisplaySlot();
    }

    public override void SlotClicked(InventorySlot_UI clickedUISlot)
    {
        if(clickedUISlot.AssignedInventorySlot.ItemData) 
        { 
            buyDisplay.InventorySystem.AddToInventory(clickedUISlot.AssignedInventorySlot.ItemData, 1);
            buyDisplay.RefreshDynamicInventory(buyDisplay.InventorySystem);
            
            totalBuyPrice += clickedUISlot.AssignedInventorySlot.ItemData.BuyPrice;
            UpdatePriceText();
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

    public void ConfirmDeal()
    {
        totalBuyPrice = 0;
        UpdatePriceText();
    }
}
