using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Medicine Object", menuName = "InventoryTest/Items/Medicine")]
public class MedicineObject : ItemObject
{
    private void Awake()
    {
        countable = true;
        type = ItemType.Medicine;
    }
}
