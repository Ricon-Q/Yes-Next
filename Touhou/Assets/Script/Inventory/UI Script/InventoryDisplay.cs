using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseInventoryItem;
    [SerializeField] protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> slotDictionary;
    public InventorySystem InventorySystem => inventorySystem;
    public Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary => slotDictionary;

    protected virtual void Start()
    {

    }
    protected virtual void Awake()
    {

    }
    public abstract void AssignSlot(InventorySystem invToDisplay);
    
    // public abstract void UpdateSlot();
    // public abstract void CreateSlot(InventorySystem invToDisplay);
    protected virtual void UpdateSlot(InventorySlot updatedSlot)
    {
        foreach (var slot in SlotDictionary)
        {
            if(slot.Value == updatedSlot)   // slot value - the "Under the hood" Inventory slot.
            {
                slot.Key.UpdateUISlot(updatedSlot); // slot key - the UI representation of the value
            }
        }
    }

    public virtual void SlotClicked(InventorySlot_UI clickedUISlot)
    {
        // Debug.Log("SlotClicked");
        bool isShiftPressd = Keyboard.current.leftShiftKey.isPressed;
        bool isCtrlPressed = Keyboard.current.leftCtrlKey.isPressed;

        // Clicked slot has an item - mouse doesn't have an item - pick up that item.
        if(clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AssignedInventorySlot.ItemData == null)
        {
            // If player is holding Shift key? split the stack
            if(isShiftPressd && clickedUISlot.AssignedInventorySlot.SplitStack(out InventorySlot halfStackSlot))
            {
                mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
                clickedUISlot.UpdateUISlot();

                return;
            }
            else if(isCtrlPressed && clickedUISlot.AssignedInventorySlot.PickUpOneStack(out InventorySlot oneStack))
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
        if(clickedUISlot.AssignedInventorySlot.ItemData == null && mouseInventoryItem.AssignedInventorySlot.ItemData != null)
        {
            clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
            clickedUISlot.UpdateUISlot();

            mouseInventoryItem.ClearSlot();
        }
        
        // Both slots have an item - decide what to do
        if(clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AssignedInventorySlot.ItemData != null)
        {
            // Are both item the same? If so combine them
            bool isSameItem = clickedUISlot.AssignedInventorySlot.ItemData == mouseInventoryItem.AssignedInventorySlot.ItemData;
            if(isSameItem && clickedUISlot.AssignedInventorySlot.RoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.StackSize))
            {
                // Is the slot stack size + mouse stack size > the slot Max Stack size? if so, take from mouse
                clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
                clickedUISlot.UpdateUISlot();

                mouseInventoryItem.ClearSlot();
            }

            else if(isSameItem && 
                !clickedUISlot.AssignedInventorySlot.RoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.StackSize, out int leftInStack))
            {
                if(leftInStack < 1) SwapSlots(clickedUISlot); // Stack is full, so swap the item
                else    // Slot is not at max, so take what's need from the mouse inventory.
                {
                    int remainingOnMouse = mouseInventoryItem.AssignedInventorySlot.StackSize - leftInStack;

                    clickedUISlot.AssignedInventorySlot.AddToStack(leftInStack);
                    clickedUISlot.UpdateUISlot();

                    var newItem = new InventorySlot(mouseInventoryItem.AssignedInventorySlot.ItemData, remainingOnMouse);
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
    }

    private void SwapSlots(InventorySlot_UI clickedUISlot)
    {
        var clonedSlot = new InventorySlot(
            mouseInventoryItem.AssignedInventorySlot.ItemData, 
            mouseInventoryItem.AssignedInventorySlot.StackSize
            );
        
        mouseInventoryItem.ClearSlot();
        mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);

        clickedUISlot.ClearSlot();
        clickedUISlot.AssignedInventorySlot.AssignItem(clonedSlot);
        clickedUISlot.UpdateUISlot();
    }
}
