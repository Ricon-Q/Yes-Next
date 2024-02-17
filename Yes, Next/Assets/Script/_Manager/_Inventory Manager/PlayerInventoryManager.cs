using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
    public bool isInventoryOpen = false;

    [Header("Main Inventory")]
    public int playerInventoryLevel = 1;
    public _InventorySystem playerInventory;
    public PlayerInventoryDisplay playerInvToDisplay;
    public GameObject playerInventoryObject;


    [Header("Herb Inventory")]
    public int herbInventoryLevel = 1;
    public _InventorySystem herbInventory;

    [Header("Potion Invenytory")]
    public int potionInventoryLevel = 1;
    public _InventorySystem potionInventory;

    [Header("ItemDataBase")]
    public ItemDatabaseObject itemDataBase;

    [Header("Category Button")]
    [SerializeField] private Button _mainInventoryButton;
    [SerializeField] private Button _herbInventoryButton;
    [SerializeField] private Button _potionInventoryButton;


    private void Start() 
    {
        // playerInventory = new _InventorySystem(playerInventoryLevel * 12);
        InventoryCanvas.SetActive(false);
        playerInventoryObject.SetActive(false);
    }

    public void GeneratePlayerInventory()
    {
        playerInventory = new _InventorySystem(playerInventoryLevel * 12);
        
        switch (herbInventoryLevel)
        {
            case 1:
                herbInventory = new _InventorySystem(10);
                break;
            case 2:
                herbInventory = new _InventorySystem(20);
                break;
            case 3:
                herbInventory = new _InventorySystem(30);
                break;
            default:
                break;
        }

        switch (potionInventoryLevel)
        {
            case 1:
                potionInventory = new _InventorySystem(10);
                break;
            case 2:
                potionInventory = new _InventorySystem(20);
                break;
            case 3:
                potionInventory = new _InventorySystem(30);
                break;
            default:
                break;
        }
        playerInvToDisplay.inventorySystem = playerInventory;
        UiManager.Instance.inventoryHotBarDisplay.RefreshDynamicInventory(PlayerInventoryManager.Instance.playerInventory);
    
    }
    
    // 불필요 함수
    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        InventoryCanvas.SetActive(isInventoryOpen);

        // playerInvToDisplay.isInfoOpen = false;

        playerInventoryObject.SetActive(isInventoryOpen);
        UiManager.Instance.inventoryHotBarDisplay.RefreshDynamicInventory(PlayerInventoryManager.Instance.playerInventory);
    
    }
    public void ChangeInventory(int index)
    {
        EnableAllButton();
        switch(index)
        {
             case 0:
                playerInvToDisplay.inventorySystem = playerInventory;
                playerInvToDisplay.RefreshDynamicInventory(playerInvToDisplay.inventorySystem);
                _mainInventoryButton.interactable = false;
                break;
            case 1:
                playerInvToDisplay.inventorySystem = herbInventory;
                playerInvToDisplay.RefreshDynamicInventory(playerInvToDisplay.inventorySystem);
                _herbInventoryButton.interactable = false;
                break;
            case 2:
                playerInvToDisplay.inventorySystem = potionInventory;
                playerInvToDisplay.RefreshDynamicInventory(playerInvToDisplay.inventorySystem);
                _potionInventoryButton.interactable = false;
                break;
            default:
                break;
        }
    }
    private void EnableAllButton()
    {
        _mainInventoryButton.interactable = true;
        _herbInventoryButton.interactable = true;
        _potionInventoryButton.interactable = true;
    }

    public void AddToInventory(int itemIdToAdd, int amountToAdd)
    {
        switch (itemDataBase.Items[itemIdToAdd].ItemType)
        {
            case ItemType.Seed:
                herbInventory.AddToInventory(itemIdToAdd, amountToAdd);
                break;
            case ItemType.Herb:
                herbInventory.AddToInventory(itemIdToAdd, amountToAdd);
                break;
            case ItemType.Potion:
                potionInventory.AddToInventory(itemIdToAdd, amountToAdd);
                break;
            case ItemType.Default:
                playerInventory.AddToInventory(itemIdToAdd, amountToAdd);
                break;
        }
    }
}

[System.Serializable]
public class _InventorySystem
{
    // 인벤토리 레벨
    public int inventoryLevel;
    // 레벨당 인벤토리
    public List<_InventorySlot> inventorySlots;

    // 인벤토리 수
    public int inventorySize;

    public _InventorySystem()
    {
        // inventorySize = inventorySlots.Count;
        inventoryLevel = 1;
        inventorySlots = new List<_InventorySlot>(inventorySize);
        for (int i = 0; i < inventorySize*inventoryLevel; i++)
        {
            inventorySlots.Add(new _InventorySlot());
        }
    }

    // public _InventorySystem(int inventoryLevel)
    // {
    //     this.inventorySize = 12;
    //     this.inventoryLevel = inventoryLevel;
    //     inventorySlots = new List<_InventorySlot>(12 * inventoryLevel);
    //     for (int i = 0; i < 12 * inventoryLevel; i++)
    //     {
    //         inventorySlots.Add(new _InventorySlot());
    //     }
    // }

    public _InventorySystem(int inventorySize)
    {
        // inventorySize = inventorySlots.Count;
        this.inventorySize = inventorySize;
        inventoryLevel = 1;
        inventorySlots = new List<_InventorySlot>(inventorySize);
        for (int i = 0; i < inventorySize; i++)
        {
            inventorySlots.Add(new _InventorySlot());
        }
    }

    public void InventoryLevelUp(int inventorySize)
    {
        inventoryLevel += 1;
        this.inventorySize += inventorySize;

        while (inventorySlots.Count < this.inventorySize)
        {
            inventorySlots.Add(new _InventorySlot());
        }
    }

    public _InventorySystem(List<InventoryItemData> inventory, int inventorySize) //NPC용 생성자
    {
        this.inventorySize = inventorySize;
        inventoryLevel = 5;
        inventorySlots = new List<_InventorySlot>(inventorySize);
        for (int i = 0; i < inventorySize; i++)
        {
            inventorySlots.Add(new _InventorySlot(inventory[i].ID, 1));
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
        invSlot = inventorySlots.Where(item => item.itemId == itemIdToAdd).ToList();
        // Debug.Log(invSlot.Count);
        return invSlot.Count > 0;
    }

    public bool HasFreeSlot(out _InventorySlot freeSlot)
    {
        freeSlot = inventorySlots.FirstOrDefault(i => i.itemId == -1);
        return freeSlot == null ? false : true;
    }

    public void SaveInventory()
    {
        DataManager.Instance.SaveInventory();
    }

    public void LoadInventory()
    {
        DataManager.Instance.LoadInventory(DataManager.Instance.currentSaveIndex);
    }

    public void ClearInventory()
    {
        inventorySlots.Clear();
        inventoryLevel = 0;
        inventorySlots = new List<_InventorySlot>(inventorySize);
        for (int i = 0; i < inventorySize; i++)
        {
            inventorySlots.Add(new _InventorySlot());
        }
    }

}


[System.Serializable]
public class _InventorySlot
{
    [SerializeField] public int itemId;
    [SerializeField] public int stackSize;
    
    public bool isCraftResultSlot = false;

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
