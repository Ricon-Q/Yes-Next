using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _BuyDisplay : _DynamicInventoryDisplay
{
    [Header("Shop Mode")]
    [SerializeField] private _ShopNpcDisplay shopNpcDisplay;

    protected override void Start()
    {
        inventorySystem = new _InventorySystem(10);
        CreateInventorySlot();
        
        RefreshDynamicInventory(this.inventorySystem);
        // inventorySystem.SaveInventory();
    }

    public void ExitShopMode()
    {
        // inventorySystem.LoadInventory();        
        inventorySystem.ClearInventory();
        RefreshDynamicInventory(this.inventorySystem);
    }

    public override void SlotClicked(_InventorySlot_UI clickedUISlot)
    {
        if(clickedUISlot.AssignedInventorySlot.itemId != -1)
        {   
            shopNpcDisplay.totalBuyPrice -= PlayerInventoryManager.Instance.itemDataBase.Items[clickedUISlot.AssignedInventorySlot.itemId].BuyPrice;
            shopNpcDisplay.UpdatePriceText();

            clickedUISlot.AssignedInventorySlot.RemoveFromStack(1);
            RefreshDynamicInventory(this.inventorySystem);
        }
    }
    public void Reset()
    {
        // inventorySystem.LoadInventory();
        inventorySystem.ClearInventory();
        RefreshDynamicInventory(this.inventorySystem);
    }

    private void OnApplicationQuit() 
    {
        // inventorySystem.Clear();
    }

    public void ConfirmDeal()
    {
        foreach (var itemSlot in inventorySystem.inventorySlots)
        {   
            if(itemSlot.itemId != -1)
            {
                switch(PlayerInventoryManager.Instance.itemDataBase.Items[itemSlot.itemId].ItemType)
                {
                    case ItemType.Herb:
                        PlayerInventoryManager.Instance.herbInventory.AddToInventory(itemSlot.itemId, itemSlot.stackSize);
                        break;
                    case ItemType.Seed:
                        PlayerInventoryManager.Instance.herbInventory.AddToInventory(itemSlot.itemId, itemSlot.stackSize);
                        break;
                    case ItemType.Potion:
                        PlayerInventoryManager.Instance.potionInventory.AddToInventory(itemSlot.itemId, itemSlot.stackSize);
                        break;
                    default:
                        PlayerInventoryManager.Instance.playerInventory.AddToInventory(itemSlot.itemId, itemSlot.stackSize);            
                        break;
                }
            }
        }
        // playerInventory.SaveInventory();
        Reset();
    }
}
