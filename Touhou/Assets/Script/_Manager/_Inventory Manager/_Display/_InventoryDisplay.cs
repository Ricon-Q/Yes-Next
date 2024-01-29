using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class _InventoryDisplay : MonoBehaviour
{
    [SerializeField] protected _MouseItemData mouseInventoryItem;
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

        // 제작용 슬롯 확인
        if(clickedUISlot.isCraftResultSlot && clickedUISlot.AssignedInventorySlot.itemId == -1)
            return;

        // Clicked slot has an item - mouse doesn't have an item - pick up that item.
        if(clickedUISlot.AssignedInventorySlot.itemId != -1 && mouseInventoryItem.AssignedInventorySlot.itemId == -1)
        {
            // If player is holding Shift key? split the stack
            if(isShiftPressd && clickedUISlot.AssignedInventorySlot.SplitStack(out _InventorySlot halfStackSlot))
            {
                mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
                clickedUISlot.UpdateUISlot();

                return;
            }
            else if(isCtrlPressed && clickedUISlot.AssignedInventorySlot.PickUpOneStack(out _InventorySlot oneStack))
            {
                Debug.Log("Ctrl Pressed");
                mouseInventoryItem.UpdateMouseSlot(oneStack);
                clickedUISlot.UpdateUISlot();

                return;
            }
            else
            {
                mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
                clickedUISlot.ClearSlot();
                return;
            }
        }

        // Clicked slot doesn't have an item - Mouse does have item - place the mouse item into the empty slot.
        if(clickedUISlot.AssignedInventorySlot.itemId == -1 && mouseInventoryItem.AssignedInventorySlot.itemId != -1)
        {
            clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
            clickedUISlot.UpdateUISlot();

            mouseInventoryItem.ClearSlot();
        }
        
        // Both slots have an item - decide what to do
        if(clickedUISlot.AssignedInventorySlot.itemId != -1 && mouseInventoryItem.AssignedInventorySlot.itemId != -1)
        {
            // Are both item the same? If so combine them
            bool isSameItem = clickedUISlot.AssignedInventorySlot.itemId == mouseInventoryItem.AssignedInventorySlot.itemId;
            if(isSameItem && clickedUISlot.AssignedInventorySlot.RoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.itemId))
            {
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

    private void SwapSlots(_InventorySlot_UI clickedUISlot)
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
