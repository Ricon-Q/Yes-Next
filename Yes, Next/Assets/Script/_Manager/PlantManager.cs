using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public List<PlantData> _plantDatas = new List<PlantData>();

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
        _plantDatas.Add(new PlantData(SceneManager.GetActiveScene().name, mousePosition, seedItemData.ID, _TimeManager.Instance.timeData));
    }
}

public class PlantData
{
    public string _sceneName;
    public Vector3 _position;
    public int _itemDataId;
    public _TimeData _plantedDay;

    public PlantData(string sceneName, Vector3 position, int itemDataId, _TimeData timeData)
    {
        _sceneName =sceneName;
        _position =position;
        _itemDataId =itemDataId;
        _plantedDay =timeData;
    }
}
