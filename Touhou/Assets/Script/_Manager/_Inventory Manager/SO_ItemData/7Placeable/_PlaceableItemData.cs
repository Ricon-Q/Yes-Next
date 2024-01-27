using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Inventory Item/Placeable Item")]
public class _PlaceableItemData : InventoryItemData
{
    public GameObject _placeObject;

    public override void Interact(Vector3 _mousePosition)
    {
        Debug.Log("Interact Placeable Item Data : " + DisplayName);
        Instantiate(_placeObject, _mousePosition, _placeObject.transform.rotation);     
    }

    public override void Replaceable()
    {
        base.Replaceable();
    }
}
