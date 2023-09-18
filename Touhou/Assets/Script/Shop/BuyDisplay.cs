using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

// 플레이어가 상인으로부터 사는 물건 Display, 중앙 상단에 해당

public class BuyDisplay : DynamicInventoryDisplay
{

    
    // 중앙 상단 Slot에 해당하는 인벤토리, 상점모드가 아닐 시에는 Empty여야 한다.
    // 해당 스크립트에서는 InventoryDisplay스크립트의 InventorySystem inventorySystem다.
    // public InventorySystem BuyInventory;    

    // 플레이어의 인벤토리, ComfirmDeal을 호출했을때 BuyInventory의 물건들이 playerInventory로 추가된다.
    public InventorySystem playerInventory;

    // Dictionary<GameObject, InventorySlot> itemDisplayed = 
    //     new Dictionary<GameObject, InventorySlot>();
    // public GameObject inventoryPrefab;

    public ShopNpcDisplay shopNpcDisplay;
    
    protected override void Start()
    {
        inventorySystem.Save();
        CreateInventorySlot();
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
        // Reset();
    }

    public void DisplaySlot()
    {
        
    }
    
    // public Vector3 GetPosition(int i)
    // {
    //     // 슬롯 위치 계산
    //     return new Vector3
    //     (
    //         X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)),
    //         Y_START + (-Y_SPACE_BETWEEN_ITEM * (i/NUMBER_OF_COLUMN)), 
    //         0f
    //     );
    // }

    // private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    // {
    //     EventTrigger trigger = obj.GetComponent<EventTrigger>();
    //     var eventTrigger = new EventTrigger.Entry();
    //     eventTrigger.eventID = type;
    //     eventTrigger.callback.AddListener(action);
    //     trigger.triggers.Add(eventTrigger);
    // }

    // private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    // {
    //     EventTrigger trigger = obj.GetComponent<EventTrigger>();
    //     var eventTrigger = new EventTrigger.Entry();
    //     eventTrigger.eventID = type;
    //     eventTrigger.callback.AddListener(action);
    //     trigger.triggers.Add(eventTrigger);
    // }



    // public void OnClick(GameObject obj, BaseEventData data)
    // {
        // PointerEventData pointerData = data as PointerEventData;

        // if (pointerData.button == PointerEventData.InputButton.Left)
        // {
        //     // Left click action
        //     shopNpcDisplay.totalBuyPrice -= itemDisplayed[obj].item.BuyPrice;
        //     inventory.RemoveItem(itemDisplayed[obj].item, 1);
        // }
        // else if (pointerData.button == PointerEventData.InputButton.Right)
        // {
        //     // Right click action
            
        //     if(itemDisplayed[obj].item.Countable == true)
        //     {
        //         if(itemDisplayed[obj].amount >= 10)
        //         {
        //             shopNpcDisplay.totalBuyPrice -= itemDisplayed[obj].item.BuyPrice * 10;
        //             inventory.RemoveItem(itemDisplayed[obj].item, 10);
        //         }
        //         else
        //         {
        //             shopNpcDisplay.totalBuyPrice -= itemDisplayed[obj].item.BuyPrice * itemDisplayed[obj].amount;
        //             inventory.RemoveItem(itemDisplayed[obj].item, itemDisplayed[obj].amount);
        //         }
        //     }
        //     else
        //     {
        //         shopNpcDisplay.totalBuyPrice -= itemDisplayed[obj].item.BuyPrice;
        //         inventory.RemoveItem(itemDisplayed[obj].item, 1);
        //     }
            
        // }
    // }

    public void Reset()
    {
        // Reset 함수가 호출되면 해당 인벤토리만 로드하도록 수정합니다.
        // Debug.Log(gameObject.name + " : Reset | inventory name : " + inventorySystem.name);
        inventorySystem.Load();
    }

    public void AddItem(Item _item, int amount = 1)
    {
        inventory.AddItem(_item, amount);
    }

    private void OnApplicationQuit() 
    {
        // 어플리케이션이 종료될 때, 모든 인벤토리를 정리하도록 수정합니다.
        inventorySystem.Clear();
    }

    public void ConfirmDeal()
    {
        // Iterate over all items in the inventory
        // for (int i = 0; i < inventory.Container.Items.Length; i++)
        // {
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