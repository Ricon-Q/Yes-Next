using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;
// using Microsoft.Unity.VisualStudio.Editor;

// 인벤토리 UI 디스플레이
public class DisplayInventory : MonoBehaviour
{
    public MouseItem mouseItem = new MouseItem();

    public GameObject inventoryPrefab;  // 인벤토리 칸 프리팹
    public InventoryObject inventory;   // 디스플레이할 인벤토리

    // 시작 위치, 간격, Column 개수
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public int Y_SPACE_BETWEEN_ITEM;

    public GameObject itemInfoPanel;
    public bool isInfoPanelOpen = false;
    public Image itemImage;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI descriptionText; 
    // public GameObject highlight;


    Dictionary<GameObject, InventorySlot> itemDisplayed = 
        new Dictionary<GameObject, InventorySlot>(); // 게임 오브젝트 - 인벤토리 슬롯 딕셔너리
    // Start is called before the first frame update
    void Start()
    {
        CreateSlots();
        itemInfoPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlot();
    }

    public void UpdateSlot()
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

    public void CreateSlots()
    {
        // 슬롯 생성, 이벤트 트리거 생성
        itemDisplayed = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            // obj.transform.Find("highlight").gameObject.SetActive(false);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            AddEvent(obj, EventTriggerType.PointerClick, delegate(BaseEventData data) { OnClick(obj, data); });

            itemDisplayed.Add(obj, inventory.Container.Items[i]);
        }
    }    

    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
    public void OnEnter(GameObject obj)
    {
        mouseItem.hoverObj = obj;
        if(itemDisplayed.ContainsKey(obj))
            mouseItem.hoverItem = itemDisplayed[obj];
    }
    public void OnExit(GameObject obj)
    {
        mouseItem.hoverObj = null;
        mouseItem.hoverItem = null;
    } 
    public void OnDragStart(GameObject obj)
    {
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(100, 100);
        mouseObject.transform.SetParent(transform.parent);
        if(itemDisplayed[obj].ID >= 0)
        {
            var img = mouseObject.AddComponent<Image>();
            img.sprite = inventory.database.GetItem[itemDisplayed[obj].ID].uiDisplay;
            img.raycastTarget = false;
            obj.transform.GetChild(0).transform.localScale = new Vector3 (0, 0, 0);
            obj.transform.GetChild(1).transform.localScale = new Vector3 (0, 0, 0);

            mouseItem.obj = mouseObject;
            mouseItem.item = itemDisplayed[obj];
        }
    }
    public void OnDragEnd(GameObject obj)
    {   
        if(mouseItem.hoverObj)
        {
            inventory.MoveItem(itemDisplayed[obj], itemDisplayed[mouseItem.hoverObj]); 
        }
        else
        {
            // inventory.RemoveItem(itemDisplayed[obj].item);
        }
        Destroy(mouseItem.obj);
        obj.transform.GetChild(0).transform.localScale = new Vector3 (1, 1, 1);
        obj.transform.GetChild(1).transform.localScale = new Vector3 (1, 1, 1);
        mouseItem.item = null;
    }
    public void OnDrag(GameObject obj)
    {
        if(mouseItem.obj != null)
            mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
    }
    public void OnClick(GameObject obj, BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;
        
        if (pointerData.button == PointerEventData.InputButton.Left)
        {
            // Left click action
            Debug.Log(itemDisplayed[obj].ID);
            if(itemDisplayed[obj].ID >= 0)
            {
                OpenInfoPanel();
                // highlight = obj.transform.Find("highlight").gameObject;
                // highlight.SetActive(true);
                ItemObject itemObjectToShow = inventory.database.GetItem[itemDisplayed[obj].item.Id];
                itemImage.sprite = itemObjectToShow.uiDisplay;
                itemName.text = "<" + itemObjectToShow.name + ">";
                descriptionText.text = itemObjectToShow.description; 
            }
            else
            {
                CloseInfoPanel();
            }
        }
        else if (pointerData.button == PointerEventData.InputButton.Right)
        {  
            // Right click action
            Debug.Log("Right click action");
        }
    }

    public void OpenInfoPanel()
    {
        itemInfoPanel.SetActive(true);
        isInfoPanelOpen = true;
    }
    public void CloseInfoPanel()
    {
        itemInfoPanel.SetActive(false);
        isInfoPanelOpen = false;
    }
}

public class MouseItem
{
    public GameObject obj;
    public InventorySlot item;
    public InventorySlot hoverItem;
    public GameObject hoverObj;
}