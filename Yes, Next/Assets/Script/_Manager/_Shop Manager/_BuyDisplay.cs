using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _BuyDisplay : _DynamicInventoryDisplay
{
    [Header("Shop Mode")]
    [SerializeField] private _ShopNpcDisplay shopNpcDisplay;

    protected override void Start()
    {
        inventorySystem = new _InventorySystem(8);
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

    public long ConfirmDeal(out long price)
    {
        price = 0;
        foreach (var itemSlot in inventorySystem.inventorySlots)
        {   
            if(itemSlot.itemId != -1)
            {
                switch(PlayerInventoryManager.Instance.itemDataBase.Items[itemSlot.itemId].ItemType)
                {
                    case ItemType.Herb:
                        if(PlayerInventoryManager.Instance.herbInventory.HasFreeSlot(out _InventorySlot freeSlot0))
                        {
                            PlayerInventoryManager.Instance.herbInventory.AddToInventory(itemSlot.itemId, itemSlot.stackSize); 
                            price += PlayerInventoryManager.Instance.itemDataBase.Items[itemSlot.itemId].BuyPrice * itemSlot.stackSize;
                            itemSlot.ClearSlot();
                        }
                        else
                            Debug.Log("No Free Slot");
                        break;
                    case ItemType.Seed:
                        if(PlayerInventoryManager.Instance.herbInventory.HasFreeSlot(out _InventorySlot freeSlot01))
                        {
                            PlayerInventoryManager.Instance.herbInventory.AddToInventory(itemSlot.itemId, itemSlot.stackSize);
                            price += PlayerInventoryManager.Instance.itemDataBase.Items[itemSlot.itemId].BuyPrice * itemSlot.stackSize;
                            itemSlot.ClearSlot();
                        }
                        else
                            Debug.Log("No Free Slot");
                        break;
                    case ItemType.Potion:
                        if(PlayerInventoryManager.Instance.potionInventory.HasFreeSlot(out _InventorySlot freeSlot02))
                        {
                            PlayerInventoryManager.Instance.potionInventory.AddToInventory(itemSlot.itemId, itemSlot.stackSize);
                            price += PlayerInventoryManager.Instance.itemDataBase.Items[itemSlot.itemId].BuyPrice * itemSlot.stackSize;
                            itemSlot.ClearSlot();
                        }
                        else
                            Debug.Log("No Free Slot");
                        break;
                    default:
                        if(PlayerInventoryManager.Instance.playerInventory.HasFreeSlot(out _InventorySlot freeSlot03))
                        {
                            PlayerInventoryManager.Instance.playerInventory.AddToInventory(itemSlot.itemId, itemSlot.stackSize);
                            price += PlayerInventoryManager.Instance.itemDataBase.Items[itemSlot.itemId].BuyPrice * itemSlot.stackSize;
                            itemSlot.ClearSlot();
                        }        
                        else
                            Debug.Log("No Free Slot");
                        break;
                }
            }
        }
        // playerInventory.SaveInventory();
        RefreshDynamicInventory(this.inventorySystem);
        return price;
    }
}
