using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ShopNpcDisplay : MonoBehaviour
{
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int Y_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public InventoryObject inventory;
    Dictionary<GameObject, InventorySlot> itemDisplayed = 
        new Dictionary<GameObject, InventorySlot>();
    public GameObject inventoryPrefab;

    public BuyDisplay buyDisplay;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI totalBuyPriceText;
    public long totalBuyPrice;
    

    // public ShopMiddleDisplay 

    public void EnterShopMode(InventoryObject inventoryObject)
    {
        totalBuyPrice = 0;
        inventory = inventoryObject;
        CreateSlots();
    }
    public void ExitShopMode()
    {
        // inventory.Clear();
    }

    private void Update()
    {
        DisplaySlot();
        priceText.text = totalBuyPrice.ToString("n0");
        totalBuyPriceText.text = "-" + totalBuyPrice.ToString("n0");
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


    public void OnClick(GameObject obj)
    {
        if(itemDisplayed[obj].ID != -1)
        {
            Item clickedItem = itemDisplayed[obj].item;

            // Retrieve the ItemObject from the ItemDatabaseObject using the clicked item's Id
            ItemObject itemObjectToAdd = inventory.database.GetItem[clickedItem.Id];

            // Create a new Item object here using the constructor which takes an ItemObject
            Item itemToAdd = new Item(itemObjectToAdd);

            buyDisplay.AddItem(itemToAdd);
            totalBuyPrice += itemToAdd.BuyPrice;
        }
    }

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
                buyDisplay.AddItem(itemToAdd);
                totalBuyPrice += itemToAdd.BuyPrice;
            }
            else if (pointerData.button == PointerEventData.InputButton.Right)
            {
                // Right click action
                Debug.Log("right Click");
                
                if(itemDisplayed[obj].item.Countable == true)
                {                   
                    buyDisplay.AddItem(itemToAdd, 10);
                    totalBuyPrice += itemToAdd.BuyPrice * 10;
                }
                else
                {
                    buyDisplay.AddItem(itemToAdd);
                    totalBuyPrice += itemToAdd.BuyPrice;
                }
            }
        }
    }

    public void Reset()
    {
        totalBuyPrice = 0;
    }

    public void ConfirmDeal()
    {
        totalBuyPrice = 0;
    }
}
