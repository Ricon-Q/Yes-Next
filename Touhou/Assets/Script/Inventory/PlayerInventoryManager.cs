using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventoryManager : MonoBehaviour
{
    private static PlayerInventoryManager instance;

    public static PlayerInventoryManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    // ======================================================= //

    [Header("Inventory")]
    public GameObject InventoryCanvas;
    public _InventorySystem playerInventory;
    public bool isInventoryOpen = false;
    public _DynamicInventoryDisplay invToDisplay;

    [Header("ItemDataBase")]
    public ItemDatabaseObject itemDataBase;

    private void Start() 
    {
        playerInventory = new _InventorySystem();
        InventoryCanvas.SetActive(false);
        invToDisplay.inventorySystem = playerInventory;
    }
    
    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        InventoryCanvas.SetActive(isInventoryOpen);
        invToDisplay.isInfoOpen = false;
        invToDisplay.infoPanel.SetActive(false);
    }
    
}

[System.Serializable]
public class _InventorySystem
{
    // 인벤토리 레벨
    public int inventoryLevel;
    // 레벨당 인벤토리
    [SerializeField]public List<_InventorySlot> InventorySlots;

    // 인벤토리 수
    public int InventorySize = 36;

    public _InventorySystem()
    {
        inventoryLevel = 0;
        InventorySlots = new List<_InventorySlot>(InventorySize);
        for (int i = 0; i < InventorySize; i++)
        {
            InventorySlots.Add(new _InventorySlot());
        }
    }

    public UnityAction<_InventorySlot> OnInventorySlotChanged;

    public bool AddToInventory(int itemIdToAdd, int amountToAdd)
    {
        if(ContainItem(itemIdToAdd, out List<_InventorySlot> invSlot))
        {
            foreach(var slot in invSlot)
            {
                if(slot.RoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd);
                    return true;
                }
            }
        }

        if(HasFreeSlot(out _InventorySlot freeSlot))
        {
            freeSlot.UpdateInventorySlot(itemIdToAdd, amountToAdd);
            return true;
        }

        return false;
    }

    public bool ContainItem(int itemIdToAdd, out List<_InventorySlot> invSlot)
    {
        invSlot = InventorySlots.Where(item => item.itemId == itemIdToAdd).ToList();
        // Debug.Log(invSlot.Count);
        return invSlot.Count > 0;
    }

    public bool HasFreeSlot(out _InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(i => i.itemId == -1);
        return freeSlot == null ? false : true;
    }

    // public int GetItemCount(InventoryItemData itemToFind)
    // {
    //     int itemCount = 0;

    //     // Check if the item exists in the inventory
    //     if (ContainItem(itemToFind, out List<InventorySlot> invSlot))
    //     {
    //         // If it does, sum up the amount in each slot
    //         foreach (var slot in invSlot)
    //         {
    //             itemCount += slot.StackSize;
    //         }
    //     }

    //     return itemCount;
    // }
}


[System.Serializable]
public class _InventorySlot
{
    [SerializeField] public int itemId;
    [SerializeField] public int stackSize;

    public _InventorySlot(int itemId, int stackSize)
    {
        this.itemId = itemId;
        this.stackSize = stackSize;
    }

    public _InventorySlot()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        itemId = -1;
        stackSize = -1;
    }

    public void AssignItem(_InventorySlot invSlot)
    {
        if(itemId == invSlot.itemId) AddToStack(invSlot.stackSize);
        else
        {
            itemId = invSlot.itemId;
            stackSize = 0;
            AddToStack(invSlot.stackSize);
        }
    }

    public void UpdateInventorySlot(int itemId, int amount)
    {
        this.itemId = itemId;
        stackSize = amount;
    }

    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining)
    {
        amountRemaining = PlayerInventoryManager.Instance.itemDataBase.Items[itemId].MaxStackSize - stackSize;
        return RoomLeftInStack(amountToAdd);
    }

    public bool RoomLeftInStack(int amountToAdd)
    {
        if(stackSize + amountToAdd <= PlayerInventoryManager.Instance.itemDataBase.Items[itemId].MaxStackSize) { return true; }
        else { return false; }
    }

    public void AddToStack(int amount)
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
        if(stackSize <= 0)
        {
            ClearSlot();
        }
    }

    public bool SplitStack(out _InventorySlot splitStack)
    {
        if(stackSize <= 1)
        {
            splitStack = null;
            return false;
        }

        int halfStack = Mathf.RoundToInt(stackSize / 2);
        RemoveFromStack(halfStack);
        
        splitStack = new _InventorySlot(itemId, halfStack);
        return true;
    }

    public bool PickUpOneStack(out _InventorySlot oneStack)
    {
        if(stackSize <= 1)
        {
            oneStack = null;
            return false;
        }

        RemoveFromStack(1);
        oneStack = new _InventorySlot(itemId, 1);
        return true;
    }
}
