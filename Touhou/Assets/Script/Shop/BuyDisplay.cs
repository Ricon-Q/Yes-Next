using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;

// 플레이어가 상인으로부터 사는 물건 Display, 중앙 상단에 해당

public class BuyDisplay : DynamicInventoryDisplay
{

    
    // 중앙 상단 Slot에 해당하는 인벤토리, 상점모드가 아닐 시에는 Empty여야 한다.
    // 해당 스크립트에서는 InventoryDisplay스크립트의 InventorySystem inventorySystem다.

    // 플레이어의 인벤토리, ComfirmDeal을 호출했을때 BuyInventory의 물건들이 playerInventory로 추가된다.
    public InventorySystem playerInventory;

    // 가격 text 업데이트를 위한 ShopNPCDisplay
    public ShopNpcDisplay shopNpcDisplay;

    protected override void Start()
    {
        inventorySystem.Save();
    }

    private void Update() 
    {
    }
    public void EnterShopMode()
    {
    }

    public void ExitShopMode()
    {
        inventorySystem.Load();
        RefreshDynamicInventory(this.inventorySystem);
    }

    public override void SlotClicked(InventorySlot_UI clickedUISlot)
    {
        if(clickedUISlot.AssignedInventorySlot.ItemData)
        {   
            shopNpcDisplay.totalBuyPrice -= clickedUISlot.AssignedInventorySlot.ItemData.BuyPrice;
            shopNpcDisplay.UpdatePriceText();

            clickedUISlot.AssignedInventorySlot.RemoveFromStack(1);
            RefreshDynamicInventory(this.inventorySystem);
        }
    }

    public void Reset()
    {
        inventorySystem.Load();
        RefreshDynamicInventory(this.inventorySystem);
    }

    public void ConfirmDeal()
    {
        foreach (var itemData in inventorySystem.InventorySlots)
        {   
            if(itemData.ItemData)
            {
                playerInventory.AddToInventory(itemData.ItemData, itemData.StackSize);
            }
        }
        Reset();

        // Iterate over all items in the inventory
        // for (int i = 0; i < inventorySystem.InventorySize; i++)
        // {
        //     if(inventorySystem.InventorySlots[i].ItemData)
        //     {

        //     }
        //     // Get the current item
        //     InventorySlot currentItem = inventory.Container.Items[i];

        //     // Check if the current item is valid (ID is not -1)
        //     if (currentItem.ID >= 0)
        //     {
        //         // Create a new ItemObject using the ID of the current item
        //         ItemObject itemObjectToAdd = inventory.database.GetItem[currentItem.item.Id];
        //         // Create a new Item using the ItemObject
        //         Item itemToAdd = new Item(itemObjectToAdd);
        //         // Add the item to the playerInventory
        //         playerInventory.AddItem(itemToAdd, currentItem.amount);
        //     }
        // }
        // // Load the inventory
        // inventory.Load();
    }
}