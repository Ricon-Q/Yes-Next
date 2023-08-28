using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopPlayerDisplay : MonoBehaviour
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
    
    private void Start() 
    {
        CreateSlots();
    }
    private void Update()
    {
        DisplaySlot();
    }

    public void CreateSlots()
    {
        // 슬롯 생성, 이벤트 트리거 생성
        itemDisplayed = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

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
}
