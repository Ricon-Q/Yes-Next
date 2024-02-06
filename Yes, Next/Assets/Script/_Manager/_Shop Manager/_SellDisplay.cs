using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _SellDisplay : _DynamicInventoryDisplay
{
    [Header("Shop Mode")]
    [SerializeField] private _ShopPlayerDisplay shopPlayerDisplay;

    protected override void Start()
    {
        inventorySystem = new _InventorySystem(8);
        CreateInventorySlot();
        RefreshDynamicInventory(this.inventorySystem);
        // inventorySystem = new _InventorySystem(10);
        // inventorySystem.SaveInventory();
    }

    public void ExitShopMode()
    {
        // inventorySystem.LoadInventory();     
        ResetPlayerInventory();   
        inventorySystem.ClearInventory();
        RefreshDynamicInventory(this.inventorySystem);
    }
    public void Reset()
    {
        // inventorySystem.LoadInventory();

        ResetPlayerInventory();
        
        inventorySystem.ClearInventory();
        RefreshDynamicInventory(this.inventorySystem);
        shopPlayerDisplay.UpdatePriceText();
    }

    public void ResetPlayerInventory()
    {
        for (int i = 0; i < inventorySystem.inventorySize; i++)
        {
            // if(inventorySystem.inventorySlots[i].itemId != -1)
            // {
            //     PlayerInventoryManager.Instance.playerInventory.AddToInventory(inventorySystem.inventorySlots[i].itemId, inventorySystem.inventorySlots[i].stackSize);
            //     shopPlayerDisplay.RefreshDynamicInventory(shopPlayerDisplay.inventorySystem);
            // }
            if(inventorySystem.inventorySlots[i].itemId != -1)
            {
                switch(PlayerInventoryManager.Instance.itemDataBase.Items[inventorySystem.inventorySlots[i].itemId].ItemType)
                {
                    case ItemType.Herb:
                        PlayerInventoryManager.Instance.herbInventory.AddToInventory(inventorySystem.inventorySlots[i].itemId, inventorySystem.inventorySlots[i].stackSize);
                        break;
                    case ItemType.Seed:
                        PlayerInventoryManager.Instance.herbInventory.AddToInventory(inventorySystem.inventorySlots[i].itemId, inventorySystem.inventorySlots[i].stackSize);
                        break;
                    case ItemType.Potion:
                        PlayerInventoryManager.Instance.potionInventory.AddToInventory(inventorySystem.inventorySlots[i].itemId, inventorySystem.inventorySlots[i].stackSize);
                        break;
                    default:
                        PlayerInventoryManager.Instance.playerInventory.AddToInventory(inventorySystem.inventorySlots[i].itemId, inventorySystem.inventorySlots[i].stackSize);            
                        break;
                }
                shopPlayerDisplay.RefreshDynamicInventory(shopPlayerDisplay.inventorySystem);
            }            
        }
    }

    public override void SlotClicked(_InventorySlot_UI clickedUISlot)
    {
        if(clickedUISlot.AssignedInventorySlot.itemId != -1)
        {
            shopPlayerDisplay.totalSellPrice -= PlayerInventoryManager.Instance.itemDataBase.Items[clickedUISlot.AssignedInventorySlot.itemId].SellPrice;
            shopPlayerDisplay.UpdatePriceText();

            shopPlayerDisplay.inventorySystem.AddToInventory(clickedUISlot.AssignedInventorySlot.itemId, 1);
            clickedUISlot.AssignedInventorySlot.RemoveFromStack(1);
            RefreshDynamicInventory(this.inventorySystem);
            shopPlayerDisplay.RefreshDynamicInventory(shopPlayerDisplay.inventorySystem);
        }
    }

    private void OnApplicationQuit() 
    {
        // inventorySystem.Clear();
    }

    public void ConfirmDeal()
    {
        inventorySystem.ClearInventory();
        
        RefreshDynamicInventory(this.inventorySystem);
        // Reset();
    }
}
