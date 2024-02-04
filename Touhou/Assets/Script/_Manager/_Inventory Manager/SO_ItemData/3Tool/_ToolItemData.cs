using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Inventory Item/Tool Item")]
public class _ToolItemData : InventoryItemData
{
    public override void Interact()
    {
        Debug.Log("Interact Tool Item Data : " + DisplayName);
    }
}
