using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC Inventory", menuName = "Inventory System/Inventory")]
public class npcInventory : ScriptableObject
{
    public List<InventoryItemData> inventory;
    // public _InventorySystem inventorySystem;

    // private void Start()
    // {
    //     inventorySystem = new _InventorySystem(inventory, inventory.Count);
    // }
}
