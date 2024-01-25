using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Inventory Item/Placeable Item")]
public class _PlaceableItemData : InventoryItemData
{
    [SerializeField] private GameObject _placeObject;

    public override void Interact(Vector3 mousePosition)
    {
        Debug.Log("Interact Placeable Item Data : " + DisplayName);
    }
}
