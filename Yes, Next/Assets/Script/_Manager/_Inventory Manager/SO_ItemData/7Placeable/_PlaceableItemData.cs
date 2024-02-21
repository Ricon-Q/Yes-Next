using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Inventory Item/Placeable Item")]
public class _PlaceableItemData : InventoryItemData
{
    [Header("PlaceableObject")]
    public List<_PlaceableHolder> _placeObjects;
    public List<Sprite> _previewSprites;
    // public BoxCollider2D _previewCollider;


    public override void Interact(Vector3 _mousePosition, int previewIndex)
    {
        Debug.Log("Interact Placeable Item Data : " + DisplayName);
        Instantiate(_placeObjects[previewIndex], _mousePosition, _placeObjects[previewIndex].transform.rotation);     
    }

    public override void Replaceable()
    {
        base.Replaceable();
    }

}
