using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

// 플레이어가 상인에게 판매하는 물건 Display, 중앙 하단에 해당
public class SellDisplay : DynamicInventoryDisplay
{
    // 중앙 하단 Slot에 해당하는 인벤토리, 상점 모드가 아닐 시에는 Empty여야 한다.
    // 해당 스크립트에서는 InventoryDisplay스크립트의 InventorySystem inventorySystem이다
    // public InventorySystem BuyInventory;    

    public ShopPlayerDisplay shopPlayerDisplay;
    
    protected override void Start()
    {
        inventorySystem.Save();
        // CreateInventorySlot();
    }

    private void Update() 
    {
        // Debug.Log("Update");
        // DisplaySlot();
    }
    public void EnterShopMode()
    {
    }

    public void ExitShopMode()
    {
        inventorySystem.Load();
        RefreshDynamicInventory(this.inventorySystem);
    }


    public void Reset()
    {
        // Reset 함수가 호출되면 해당 인벤토리만 로드하도록 수정합니다.
        // Debug.Log(gameObject.name + " : Reset | inventory name : " + inventory.name);
        inventorySystem.Load();
        RefreshDynamicInventory(this.inventorySystem);
        shopPlayerDisplay.UpdatePriceText();
    }
    public override void SlotClicked(InventorySlot_UI clickedUISlot)
    {
        if(clickedUISlot.AssignedInventorySlot.ItemData)
        {
            shopPlayerDisplay.totalSellPrice -= clickedUISlot.AssignedInventorySlot.ItemData.SellPrice;
            shopPlayerDisplay.UpdatePriceText();

            shopPlayerDisplay.InventorySystem.AddToInventory(clickedUISlot.AssignedInventorySlot.ItemData, 1);
            clickedUISlot.AssignedInventorySlot.RemoveFromStack(1);
            RefreshDynamicInventory(this.inventorySystem);
            shopPlayerDisplay.RefreshDynamicInventory(shopPlayerDisplay.InventorySystem);
        }
    }

    public void ConfirmDeal()
    {
        Reset();
    }
}
