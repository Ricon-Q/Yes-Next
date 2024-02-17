using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class _InventoryDisplay : MonoBehaviour
{
    [Header("Mouse Item Data")]
    [SerializeField] protected _MouseItemData mouseInventoryItem;

    [Header("Inventory System")]
    [SerializeField] public _InventorySystem inventorySystem;
    [SerializeField] protected Dictionary<_InventorySlot_UI, _InventorySlot> slotDictionary;
    // public _InventorySystem InventorySystem => inventorySystem;
    public Dictionary<_InventorySlot_UI, _InventorySlot> SlotDictionary => slotDictionary;

    public delegate void SlotClickedAction();
    public static event SlotClickedAction onSlotClicked;

    protected virtual void Start()
    {

    }
    protected virtual void Awake()
    {

    }
    
    public abstract void AssignSlot(_InventorySystem invToDisplay);

    protected virtual void UpdateSlot(_InventorySlot updatedSlot)
    {
        foreach (var slot in SlotDictionary)
        {
            if(slot.Value == updatedSlot)   // slot value - the "Under the hood" Inventory slot.
            {
                slot.Key.UpdateUISlot(updatedSlot); // slot key - the UI representation of the value
            }
        }
    }

    public virtual void SlotClicked(_InventorySlot_UI clickedUISlot)
    {
        // Debug.Log("SlotClicked");
        bool isShiftPressd = Keyboard.current.leftShiftKey.isPressed;
        bool isCtrlPressed = Keyboard.current.leftCtrlKey.isPressed;

        // 제작용 슬롯 확인 - 제작용 슬롯이고, 마우스 슬롯 != null 일시 리턴
        if(clickedUISlot.AssignedInventorySlot.isCraftResultSlot == true && mouseInventoryItem.AssignedInventorySlot.itemId != -1)
            return;
        // 제작용 슬롯이고, 슬롯 할당되어 있고, 마우스 슬롯 == -1일 경우 -> 마우스로  
        if(clickedUISlot.AssignedInventorySlot.isCraftResultSlot == true && clickedUISlot.AssignedInventorySlot.itemId != -1 && mouseInventoryItem.AssignedInventorySlot.itemId == -1)
        {
            PlayerInventoryManager.Instance.AddToInventory(clickedUISlot.AssignedInventorySlot.itemId, clickedUISlot.AssignedInventorySlot.stackSize);
            clickedUISlot.ClearSlot();
        }
        // 클릭 슬롯 아이템 O - 마우스 슬롯 아이템 X - pick up that item.
        if(clickedUISlot.AssignedInventorySlot.itemId != -1 && mouseInventoryItem.AssignedInventorySlot.itemId == -1)
        {
            // Shift키 누른채로 클릭시 절반 나누기
            if(isShiftPressd && clickedUISlot.AssignedInventorySlot.SplitStack(out _InventorySlot halfStackSlot))
            {
                mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
                clickedUISlot.UpdateUISlot();

                return;
            }
            // Ctrl키 누른채로 클릭시 1개만 가져감
            else if(isCtrlPressed && clickedUISlot.AssignedInventorySlot.PickUpOneStack(out _InventorySlot oneStack))
            {
                Debug.Log("Ctrl Pressed");
                mouseInventoryItem.UpdateMouseSlot(oneStack);
                clickedUISlot.UpdateUISlot();

                return;
            }
            // 그냥 클릭시 전부 가져감
            else
            {
                mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
                clickedUISlot.ClearSlot();
                return;
            }
        }

        // 클릭 슬롯 아이템 X - 마우스 슬롯 아이템 O - 클릭 슬롯으로 아이템 이동.
        if(clickedUISlot.AssignedInventorySlot.itemId == -1 && mouseInventoryItem.AssignedInventorySlot.itemId != -1)
        {
            clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
            clickedUISlot.UpdateUISlot();

            mouseInventoryItem.ClearSlot();
        }
        
        // 모든 슬롯에 아이템 있을 경우 - decide what to do
        if(clickedUISlot.AssignedInventorySlot.itemId != -1 && mouseInventoryItem.AssignedInventorySlot.itemId != -1)
        {
            // 같은 아이템일 경우, If so combine them
            bool isSameItem = clickedUISlot.AssignedInventorySlot.itemId == mouseInventoryItem.AssignedInventorySlot.itemId;
            Debug.Log(isSameItem);
            if(isSameItem && clickedUISlot.AssignedInventorySlot.RoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.stackSize))
            {
                Debug.Log("test 1");
                // Is the slot stack size + mouse stack size > the slot Max Stack size? if so, take from mouse
                clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
                clickedUISlot.UpdateUISlot();

                mouseInventoryItem.ClearSlot();
            }

            else if(isSameItem && 
                !clickedUISlot.AssignedInventorySlot.RoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.stackSize, out int leftInStack))
            {
                if(leftInStack < 1) SwapSlots(clickedUISlot); // Stack is full, so swap the item
                else    // Slot is not at max, so take what's need from the mouse inventory.
                {
                    Debug.Log("test 2");
                    int remainingOnMouse = mouseInventoryItem.AssignedInventorySlot.stackSize - leftInStack;

                    clickedUISlot.AssignedInventorySlot.AddToStack(leftInStack);
                    clickedUISlot.UpdateUISlot();

                    var newItem = new _InventorySlot(mouseInventoryItem.AssignedInventorySlot.itemId, remainingOnMouse);
                    mouseInventoryItem.ClearSlot();
                    mouseInventoryItem.UpdateMouseSlot(newItem);
                }
            }
            // if different items, then swap the items.
            else if(!isSameItem)
            {
                SwapSlots(clickedUISlot);
            }
        }

        onSlotClicked?.Invoke();
    }

    protected void SwapSlots(_InventorySlot_UI clickedUISlot)
    {
        var clonedSlot = new _InventorySlot(
            mouseInventoryItem.AssignedInventorySlot.itemId, 
            mouseInventoryItem.AssignedInventorySlot.stackSize
            );
        
        mouseInventoryItem.ClearSlot();
        mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);

        clickedUISlot.ClearSlot();
        clickedUISlot.AssignedInventorySlot.AssignItem(clonedSlot);
        clickedUISlot.UpdateUISlot();
    }
}
