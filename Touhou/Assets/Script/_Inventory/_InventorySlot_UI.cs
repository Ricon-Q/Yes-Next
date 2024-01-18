using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class _InventorySlot_UI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private _InventorySlot assignedInventorySlot;
    [SerializeField] private TextMeshProUGUI itemName = null;
    [SerializeField] private TextMeshProUGUI itemPrice = null;
    public ItemType type = default;
    public UnityEvent onRightClick;

    public bool isCraftResultSlot = false;

    private Button button;

    public _InventorySlot AssignedInventorySlot => assignedInventorySlot;
    public _InventoryDisplay ParentDisplay {get; private set;}

    private void Awake()
    {
        ClearSlot();
        button = GetComponent<Button>();
        button?.onClick.AddListener(OnUISlotClick);

        ParentDisplay = transform.parent.GetComponent<_InventoryDisplay>();
    }
    public void Init(_InventorySlot slot)
    {
        assignedInventorySlot = slot;
        UpdateUISlot(slot);
    }
    public void UpdateUISlot(_InventorySlot slot)
    {
        if(type == default)
        {
            if(slot.itemId != -1)
            {
                itemSprite.sprite = PlayerInventoryManager.Instance.itemDataBase.Items[slot.itemId].Icon;
                itemSprite.color = Color.white;

                if(slot.stackSize > 1) itemCount.text = slot.stackSize.ToString();
                else itemCount.text = "";
                UpdateNamePrice();
            }
            else
            {
                itemSprite.sprite = null;
                itemSprite.color = Color.clear;
                itemCount.text = "";
            }
        }
        else
        {
            if(slot.itemId != -1)
            {
                itemSprite.sprite = PlayerInventoryManager.Instance.itemDataBase.Items[slot.itemId].Icon;
                if(PlayerInventoryManager.Instance.itemDataBase.Items[slot.itemId].ItemType != type)
                {
                    itemSprite.color = new Color(1, 1, 1, .3f);
                }
                else
                {
                    itemSprite.color = Color.white;
                }

                if(slot.stackSize > 1) itemCount.text = slot.stackSize.ToString();
                else itemCount.text = "";
                UpdateNamePrice();
            }
            else
            {
                itemSprite.sprite = null;
                itemSprite.color = Color.clear;
                itemCount.text = "";
            }
        }
    }
    public void UpdateCategorySlot(_InventorySlot slot, ItemType type)
    {
        this.type = type;
        if(slot.itemId == -1) return;
        else if(type == default)
        {
            itemSprite.color = Color.white;
        }
        else
        {
            if(PlayerInventoryManager.Instance.itemDataBase.Items[slot.itemId].ItemType != type)
            {
                itemSprite.color = new Color(1, 1, 1, .3f);
            }
            else
            {
                itemSprite.color = Color.white;
            }
        }
    }
    public void UpdateUISlot()
    {
        if(assignedInventorySlot != null) UpdateUISlot(assignedInventorySlot);
    }
    public void ClearSlot()
    {
        assignedInventorySlot?.ClearSlot();
        itemSprite.sprite = null;
        itemSprite.color = Color.clear;
        itemCount.text = "";
    }
    public void OnUISlotClick()
    {
        if(PlayerInventoryManager.Instance.isInventoryOpen) 
        { 
            // Debug.Log("UI Left, info true");
            PlayerInventoryManager.Instance.invToDisplay.ToggleInfo(AssignedInventorySlot, false); 
        }
        // if (ParentDisplay) SlotClicked(this);
        ParentDisplay?.SlotClicked(this);
    }
    public void UpdateNamePrice()
    {
        if(!itemName) return;
        else
        {
            itemName.text = PlayerInventoryManager.Instance.itemDataBase.Items[AssignedInventorySlot.itemId].name.ToString();
            itemPrice.text = PlayerInventoryManager.Instance.itemDataBase.Items[AssignedInventorySlot.itemId].BuyPrice.ToString();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if(PlayerInventoryManager.Instance.isInventoryOpen) 
            { 
                PlayerInventoryManager.Instance.invToDisplay.ToggleInfo(AssignedInventorySlot, true); 
            }
        }
    }
}
