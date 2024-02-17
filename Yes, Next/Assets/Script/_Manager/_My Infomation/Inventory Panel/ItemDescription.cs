using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescription : MonoBehaviour
{
    [Header("Description")]
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _itemStackSize;
    [SerializeField] private TextMeshProUGUI _itemDescription;
    private InventoryItemData _itemData;
    
    public void UpdateDescription(_InventorySlot clickedUISlot)
    {
        
        if(clickedUISlot.itemId != -1)
        {
            _itemData = PlayerInventoryManager.Instance.itemDataBase.Items[clickedUISlot.itemId];
            // _itemIcon.color.a = 1f;
            _itemIcon.color = Color.white;
            _itemIcon.sprite = _itemData.Icon;

            _itemName.text = _itemData.DisplayName;
            _itemStackSize.text = string.Format("{0} / {1}", clickedUISlot.stackSize, _itemData.MaxStackSize);
            _itemDescription.text = _itemData.Description;
        }
        else if(clickedUISlot.itemId == -1 || clickedUISlot.itemId == _itemData.ID)
        {
            _itemIcon.color = Color.clear;
            _itemIcon.sprite = null;

            _itemName.text = "";
            _itemStackSize.text = "";
            _itemDescription.text = "아이템을 우클릭하여 상세 정보 확인";
        }
    }
}
