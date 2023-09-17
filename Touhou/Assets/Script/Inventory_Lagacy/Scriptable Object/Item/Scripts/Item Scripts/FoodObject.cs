using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food Object", menuName = "InventoryTest/Items/Food")]
public class FoodObject : ItemObject
{
    private void Awake()
    {
        countable = true;
        type = ItemType.Food;
    }
}
