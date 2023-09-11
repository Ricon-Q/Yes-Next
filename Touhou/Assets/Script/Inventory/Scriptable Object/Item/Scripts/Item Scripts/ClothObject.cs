using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cloth Object", menuName = "InventoryTest/Items/Cloth")]
public class ClothObject : ItemObject
{
    private void Awake()
    {
        countable = true;
        type = ItemType.Cloth;
    }
}
