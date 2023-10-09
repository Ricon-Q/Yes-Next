using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InventorySlot_UI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private InventorySlot assignedInventorySlot;
    [SerializeField] private TextMeshProUGUI itemName = null;
    [SerializeField] private TextMeshProUGUI itemPrice = null;
    public ItemType type = default;
    public UnityEvent onRightClick;

    public bool isCraftResultSlot = false;

    private Button button;

    public InventorySlot AssignedInventorySlot => assignedInventorySlot;
    public InventoryDisplay ParentDisplay {get; private set;}

    private void Awake()
    {
        ClearSlot();
        button = GetComponent<Button>();
        button?.onClick.AddListener(OnUISlotClick);

        ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();
    }
    public void Init(InventorySlot slot)
    {
        assignedInventorySlot = slot;
        UpdateUISlot(slot);
    }
    public void UpdateUISlot(InventorySlot slot)
    {
        if(type == default)
        {
            if(slot.ItemData != null)
            {
                itemSprite.sprite = slot.ItemData.Icon;
                itemSprite.color = Color.white;

                if(slot.StackSize > 1) itemCount.text = slot.StackSize.ToString();
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
            if(slot.ItemData != null)
            {
                itemSprite.sprite = slot.ItemData.Icon;
                if(slot.ItemData.ItemType != type)
                {
                    itemSprite.color = new Color(1, 1, 1, .3f);
                }
                else
                {
                    itemSprite.color = Color.white;
                }

                if(slot.StackSize > 1) itemCount.text = slot.StackSize.ToString();
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
    public void UpdateCategorySlot(InventorySlot slot, ItemType type)
    {
        this.type = type;
        if(!slot.ItemData) return;
        else if(type == default)
        {
            itemSprite.color = Color.white;
        }
        else
        {
            if(slot.ItemData.ItemType != type)
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
        if(InventoryManager.Instance.isInventoryOpen) 
        { 
            // Debug.Log("UI Left, info true");
            InventoryManager.Instance.invToDisplay.ToggleInfo(AssignedInventorySlot, false); 
        }
        // if (ParentDisplay) SlotClicked(this);
        ParentDisplay?.SlotClicked(this);
    }
    public void UpdateNamePrice()
    {
        if(!itemName) return;
        else
        {
            itemName.text = AssignedInventorySlot.ItemData.name.ToString();
            itemPrice.text = AssignedInventorySlot.ItemData.BuyPrice.ToString();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if(InventoryManager.Instance.isInventoryOpen) 
            { 
                InventoryManager.Instance.invToDisplay.ToggleInfo(AssignedInventorySlot, true); 
            }
        }
    }
}
