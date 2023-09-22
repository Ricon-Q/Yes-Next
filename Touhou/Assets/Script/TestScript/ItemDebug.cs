using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDebug : MonoBehaviour
{
    PlayerInventoryHolder playerInventoryHolder;

    private void Start() 
    {
        playerInventoryHolder = PlayerInventoryHolder.Instance;
    }
    public void AddItemData(InventoryItemData data)
    {
        playerInventoryHolder.AddToInventory(data, 1);
    }
}
