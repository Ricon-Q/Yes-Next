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

    public void TrySeedPlant(Vector3 mousePosition, _SeedItemData seedItemData)
    {
        // 현재 씬 이름 가져오기
        string currentSceneName = SceneManager.GetActiveScene().name;

        // 식물을 심을 수 있는지 확인
        if (CanPlantSeed(mousePosition, currentSceneName))
            SeedPlant(mousePosition, seedItemData); // 조건을 만족하면 식물 심기
        else
            PixelCrushers.DialogueSystem.DialogueManager.ShowAlert("이미 해당 위치에 식물이 존재합니다.");
    }

    public void SeedPlant(Vector3 mousePosition, _SeedItemData seedItemData)
    {
        // 프리팹 생성
        var Plant = Instantiate(_seedItemData, mousePosition, PlantManager.instance.transform.rotation);
        Plant.SetData(mousePosition, seedItemData, _TimeManager.Instance.timeData);

        // 프리팹에 아이템 데이터 전달
        // 씨앗 데이터 (위치, 씬, 심은 날짜, seedItemData 저장)
        _plantDatas.Add(new PlantData(SceneManager.GetActiveScene().name, mousePosition, seedItemData.ID, new _TimeData(_TimeManager.Instance.timeData)));
    }

    public bool CanPlantSeed(Vector3 position, string sceneName)
    {
        foreach (var plantData in _plantDatas)
        {
            // 동일한 씬(scene)과 위치에 식물이 존재하는지 확인
            if (plantData._sceneName == sceneName && plantData._position == position)
            {
                return false; // 동일한 위치에 식물이 이미 존재함
            }
        }
        return true; // 식물을 심을 수 있음
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
