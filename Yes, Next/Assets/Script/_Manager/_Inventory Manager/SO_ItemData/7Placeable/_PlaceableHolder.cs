using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _PlaceableHolder : MonoBehaviour
{
    public BoxCollider2D _previewCollider;
    public List<AreaData> areaDatas;
    public PlaceableObject _placeableObject;
    public InventoryItemData _originalItemData;
}
