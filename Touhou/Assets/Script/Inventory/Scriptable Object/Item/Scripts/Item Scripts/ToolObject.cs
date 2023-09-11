using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tool Object", menuName = "InventoryTest/Items/Tool")]
public class ToolObject : ItemObject
{
    private void Awake() 
    {
        countable = false;
        type = ItemType.Tool;    
    }
}
