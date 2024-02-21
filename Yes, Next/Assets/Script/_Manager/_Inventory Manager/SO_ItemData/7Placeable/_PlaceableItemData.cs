using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Inventory Item/Placeable Item")]
public class _PlaceableItemData : InventoryItemData
{
    [Header("PlaceableObject")]
    public _PlaceableHolder _placeObject;
    // public BoxCollider2D _previewCollider;


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
