using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[ES3Serializable]
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
[System.Serializable]
public class InventorySystem : ScriptableObject
{
    public string savePath;
    [SerializeField] public List<InventorySlot> InventorySlots;
    // public List<InventorySlot> InventorySlots => inventorySlots;
    public int InventorySize => InventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd)
    {
        if(ContainItem(itemToAdd, out List<InventorySlot> invSlot)) // Check whether item exists in inventory 
        {
            foreach (var slot in invSlot)
            {
                if(slot.RoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }
        }

        if(HasFreeSlot(out InventorySlot freeSlot))    // Get the first available slot
        {
            freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
            OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
        }

        return false;
    }

    public bool ContainItem(InventoryItemData itemToAdd, out List<InventorySlot> invSlot)
    {
        invSlot = InventorySlots.Where(i => i.ItemData == itemToAdd).ToList();
        // Debug.Log(invSlot.Count);
        return invSlot == null ? false : true;
    }

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(i => i.ItemData == null);
        return freeSlot == null ? false : true;
    }

    public int GetItemCount(InventoryItemData itemToFind)
    {
        int itemCount = 0;

        // Check if the item exists in the inventory
        if (ContainItem(itemToFind, out List<InventorySlot> invSlot))
        {
            // If it does, sum up the amount in each slot
            foreach (var slot in invSlot)
            {
                itemCount += slot.StackSize;
            }
        }

        return itemCount;
    }

    [ContextMenu("Save Inventory")]
    public void Save()
    {
        // string saveData = JsonUtility.ToJson(this, true);
        // BinaryFormatter bf = new BinaryFormatter();
        // FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        // bf.Serialize(file, saveData);
        // file.Close();
    }

    [ContextMenu("Load Inventory")]
    public void Load()
    {
        // if(File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        // {
        //     BinaryFormatter bf = new BinaryFormatter();
        //     FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
        //     JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
        //     file.Close();
        // }
    }

    [ContextMenu("Clear Inventory")]
    public void Clear()
    {
        int tmp = InventorySize;
        InventorySlots = new List<InventorySlot>(tmp);
        for (int i = 0; i < tmp; i++)
        {
            InventorySlots.Add(new InventorySlot());
        }
    }
}
