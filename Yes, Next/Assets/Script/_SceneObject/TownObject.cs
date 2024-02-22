using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownObject : MonoBehaviour
{
    [SerializeField] private _SeedPrefab _seedPrefab;

    private void Start()
    {
        InitiateObject();
        InitiatePlant();
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

    private void InitiatePlant()
    {
        foreach (var item in PlantManager.Instance._plantDatas)
        {
            if(item._sceneName != "Town") continue;
            _SeedItemData _seedItemData = PlayerInventoryManager.Instance.itemDataBase.Items[item._itemDataId] as _SeedItemData;
            var Plant = Instantiate(_seedPrefab, item._position, this.transform.rotation);  
            Plant.SetData(item._position, _seedItemData, item._plantedDay);
            Debug.Log("Spawn");
        }
    }
}
