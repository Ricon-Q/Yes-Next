using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcInventoryHolder : MonoBehaviour
{
    public npcInventory npcInventory;
    public _InventorySystem inventorySystem;

    private void Start()
    {
        inventorySystem = new _InventorySystem(npcInventory.inventory, npcInventory.inventory.Count);
    }
}
