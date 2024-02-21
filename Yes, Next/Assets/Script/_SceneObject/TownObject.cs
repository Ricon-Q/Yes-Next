using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownObject : MonoBehaviour
{
    private void Start()
    {
        InitiateObject();
    }

    private void InitiateObject()
    {
        foreach (var item in ObjectManager.Instance._placeableObjects)
        {
            if(item._spawnScene != "Town") continue;
            _PlaceableItemData _PlaceableItemData = PlayerInventoryManager.Instance.itemDataBase.Items[item._placeableItemDataId] as _PlaceableItemData;
            Instantiate(_PlaceableItemData._placeObjects[item._previewIndex], item._position, _PlaceableItemData._placeObjects[item._previewIndex].transform.rotation);     
            Debug.Log("Spawn");
        }
    }
}
