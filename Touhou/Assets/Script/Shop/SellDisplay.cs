using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class SellDisplay : MonoBehaviour
{
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int Y_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public InventoryObject inventory;
    public ShopPlayerDisplay shopPlayerInventory;
    // public InventoryObject emptyInventory;
    Dictionary<GameObject, InventorySlot> itemDisplayed = 
        new Dictionary<GameObject, InventorySlot>();
    public GameObject inventoryPrefab;

    
    private void Start()
    {
        inventory.Save();
        CreateSlots();
    }

    private void Update() 
    {
        // Debug.Log("Update");
        DisplaySlot();
    }
    public void EnterShopMode()
    {
    }

    public void ExitShopMode()
    {
        inventory.Load();
        // Reset();
    }

    public void CreateSlots()
    {
        // 슬롯 생성, 이벤트 트리거 생성
        itemDisplayed = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            // AddEvent(obj, EventTriggerType.PointerClick, delegate { OnClick(obj); });
            AddEvent(obj, EventTriggerType.PointerClick, delegate(BaseEventData data) { OnClick(obj, data); });

            itemDisplayed.Add(obj, inventory.Container.Items[i]);
        }
    }
    public void DisplaySlot()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemDisplayed)
        {
            if (_slot.Value.ID >= 0)
            {
                // 아이템 이미지 스프라이트 설정
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = 
                    inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;
                // 아이템 이미지 투명도 설정
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = 
                    new Color(1, 1, 1, 1);
                //아이템 개수 설정 (1개 일시 "", n개 일시 "n"으로 표시)
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = 
                    _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = 
                    new Color(1, 1, 1, 0);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }
    
    public Vector3 GetPosition(int i)
    {
        // 슬롯 위치 계산
        return new Vector3
        (
            X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)),
            Y_START + (-Y_SPACE_BETWEEN_ITEM * (i/NUMBER_OF_COLUMN)), 
            0f
        );
    }

    public void Reset()
    {
        // Reset 함수가 호출되면 해당 인벤토리만 로드하도록 수정합니다.
        Debug.Log(gameObject.name + " : Reset | inventory name : " + inventory.name);
        inventory.Load();
    }

    public void AddItem(Item _item, int amount = 1)
    {
        inventory.AddItem(_item, amount);
    }

    // private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    // {
    //     EventTrigger trigger = obj.GetComponent<EventTrigger>();
    //     var eventTrigger = new EventTrigger.Entry();
    //     eventTrigger.eventID = type;
    //     eventTrigger.callback.AddListener(action);
    //     trigger.triggers.Add(eventTrigger);
    // }

    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }


    // public void OnClick(GameObject obj)
    // {
    //     if(itemDisplayed[obj].ID != -1)
    //     {
    //         Item clickedItem = itemDisplayed[obj].item;

    //         // Retrieve the ItemObject from the ItemDatabaseObject using the clicked item's Id
    //         ItemObject itemObjectToAdd = inventory.database.GetItem[clickedItem.Id];

    //         // Create a new Item object here using the constructor which takes an ItemObject
    //         Item itemToAdd = new Item(itemObjectToAdd);

    //         shopPlayerInventory.inventory.AddItem(itemToAdd, 1);
    //         inventory.RemoveItem(itemDisplayed[obj].item, 1);
    //         shopPlayerInventory.totalSellPrice -= itemToAdd.SellPrice;
    //     }
    // }

    public void OnClick(GameObject obj, BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;

        if(itemDisplayed[obj].ID != -1)
        {
            Item clickedItem = itemDisplayed[obj].item;
            ItemObject itemObjectToAdd = inventory.database.GetItem[clickedItem.Id];
            Item itemToAdd = new Item(itemObjectToAdd);

            if (pointerData.button == PointerEventData.InputButton.Left)
            {
                // Left click action
                shopPlayerInventory.inventory.AddItem(itemToAdd, 1);
                inventory.RemoveItem(itemDisplayed[obj].item, 1);
                shopPlayerInventory.totalSellPrice -= itemToAdd.SellPrice;
            }
            else if (pointerData.button == PointerEventData.InputButton.Right)
            {
                // Right click action
                
                if(itemDisplayed[obj].item.Countable == true)
                {
                    if(itemDisplayed[obj].amount >= 10)
                    {
                        shopPlayerInventory.inventory.AddItem(itemToAdd, 10);
                        inventory.RemoveItem(itemDisplayed[obj].item, 10);
                        shopPlayerInventory.totalSellPrice -= itemToAdd.SellPrice * 10;
                    }
                    else
                    {
                        shopPlayerInventory.inventory.AddItem(itemToAdd, itemDisplayed[obj].amount);
                        shopPlayerInventory.totalSellPrice -= itemToAdd.SellPrice * itemDisplayed[obj].amount;
                        inventory.RemoveItem(itemDisplayed[obj].item, itemDisplayed[obj].amount);
                    } 
                }
                else
                {
                    shopPlayerInventory.inventory.AddItem(itemToAdd, 1);
                    inventory.RemoveItem(itemDisplayed[obj].item, 1);
                    shopPlayerInventory.totalSellPrice -= itemToAdd.SellPrice;
                }
                  
            }
        }
    }
    private void OnApplicationQuit() 
    {
        // 어플리케이션이 종료될 때, 모든 인벤토리를 정리하도록 수정합니다.
        inventory.Clear();
    }

    public void ConfirmDeal()
    {
        inventory.Load();
    }
}
