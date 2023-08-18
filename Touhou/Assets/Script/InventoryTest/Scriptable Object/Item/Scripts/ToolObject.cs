using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tool Object", menuName = "InventoryTest/Items/Tool")]
public class ToolObject : ItemObject
{
    public float ToolBonus;
    private void Awake() 
    {
        countable = false;
        type = ItemType.Tool;    
    }
}
