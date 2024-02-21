using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    private static PlantManager instance;

    public static PlantManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // ====================================================== /
    public bool _plantMode;

    [Header("Plant Area")]
    public Vector2 _areaCenter;
    public float _areaWidth;
    public float _areaHeight;

    [Header("Seed Prefab")]
    [SerializeField] private _SeedPrefab _seedItemData;


    public void SetPlantMode(bool val)
    {
        _plantMode = val;
    }

    public void SetArea(Vector2 center, float width, float height)
    {
        _areaCenter = center;
        _areaWidth = width;
        _areaHeight = height;
    }

    public void SetAreaZero()
    {
        _areaCenter = Vector2.zero;
        _areaWidth = 0f;
        _areaHeight = 0f;
    }

    public void SeedPlant(Vector3 mousePosition, _SeedItemData seedItemData)
    {
        // 프리팹 생성
        var Plant = Instantiate(_seedItemData, mousePosition, PlantManager.instance.transform.rotation);
        Plant.SetData(mousePosition, seedItemData, _TimeManager.Instance.timeData);
        // 프리팹에 아이템 데이터 전달
        // 씨앗 데이터 (위치, 씬, 심은 날짜, seedItemData 저장)
    }
}

public class PlantData
{
    
}
