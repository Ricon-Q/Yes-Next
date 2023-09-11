using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient Object", menuName = "InventoryTest/Items/Ingredient")]
public class IngredientObject : ItemObject
{
    private void Awake()
    {
        countable = true;
        type = ItemType.Ingredient;
    }
}
