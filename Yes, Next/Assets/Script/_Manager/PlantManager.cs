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
    public Vector3 _areaCenter;
    public float _areaWidth;
    public float _areaHeight;

    [Header("Seed Prefab")]
    [SerializeField] private _SeedPrefab _seedItemData;
    public List<_SeedPrefab> _seedPrefabs;

    public List<PlantData> _plantDatas = new List<PlantData>();

    [Header("Destroy Garden")]
    [SerializeField] private GameObject _destroyGardenPanel;

    private void Start() 
    {
        CloseDestroyGardenPanel();
    }

    public void OpenDestroyGardenPanel()
    {
        _destroyGardenPanel.SetActive(true);
    }
    public void CloseDestroyGardenPanel()
    {
        _destroyGardenPanel.SetActive(false);
        PlayerMovement.SetMoveMode(true);
    }

    public void DestroyGarden()
    {
        _PlaceableHolder[] gardens = FindObjectsOfType<_PlaceableHolder>();
        DeletePlantsInArea(SceneManager.GetActiveScene().name);

        // 각 _SeedPrefab 오브젝트를 순회합니다.
        foreach (var garden in gardens)
        {
            // _SeedPrefab의 _position이 주어진 position과 같은지 검사합니다.
            if (garden.transform.position == _areaCenter) // Vector3 비교는 정밀도에 따라 수정이 필요할 수 있습니다.
                // 같다면 해당 오브젝트를 삭제합니다.
                {
                    ObjectManager.Instance.RemoveObject(garden._originalItemData.ID, garden.transform.position);
                    Destroy(garden.gameObject);
                }
        }
            

        PlayerMovement.SetMoveMode(true);
        CloseDestroyGardenPanel();
    }

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

    public void CanHarvest(Vector3 position, string sceneName)
    {
        foreach (var plantData in _plantDatas)
        {
            // 동일한 씬(scene)과 위치에 식물이 존재하는지 확인
            if (plantData._sceneName == sceneName && plantData._position == position)
            {
                // 위치에 식물이 있다면 해당 식물이 수확 가능한지 (식물의 스프라이트가 최대인지) 확인
                int daysSincePlanted = _TimeManager.Instance.DaysSince(plantData._plantedDay);
                _SeedItemData tmpSeedData = PlayerInventoryManager.Instance.itemDataBase.Items[plantData._itemDataId] as _SeedItemData;
                if(daysSincePlanted >= tmpSeedData._sprites.Count-1)
                {
                    // 인벤토리에 공간이 있다면 작물 수확
                    if(PlayerInventoryManager.Instance.AddToInventory(tmpSeedData._outputItem.ID, 1))
                    {
                        UiManager.Instance.inventoryHotBarDisplay.RefreshDynamicInventory(PlayerInventoryManager.Instance.playerInventory);

                        // 위치에 있는 작물 데이터 삭제
                        DeletePlant(position, sceneName);
                    }
                    // 인벤토리에 공간이 없다면 수확 실패
                    else
                        PixelCrushers.DialogueSystem.DialogueManager.ShowAlert("인벤토리에 공간이 없습니다.");

                    return;
                }
            }
        }
    }

    public void DeletePlant(Vector3 position, string sceneName)
    {
        foreach (var plantData in _plantDatas)
        {
            // 동일한 씬(scene)과 위치에 식물이 존재하는지 확인
            if (plantData._sceneName == sceneName && plantData._position == position)
            {
                _plantDatas.Remove(plantData);
                DeleteSeedPrefabAtPosition(position);
                return;
            }
        }
    }

    public void DeleteSeedPrefabAtPosition(Vector3 position)
    {
        // _SeedPrefab 컴포넌트를 가진 모든 오브젝트를 찾습니다.
        _SeedPrefab[] seedPrefabs = FindObjectsOfType<_SeedPrefab>();

        // 각 _SeedPrefab 오브젝트를 순회합니다.
        foreach (var seedPrefab in seedPrefabs)
            // _SeedPrefab의 _position이 주어진 position과 같은지 검사합니다.
            if (seedPrefab._position == position) // Vector3 비교는 정밀도에 따라 수정이 필요할 수 있습니다.
                // 같다면 해당 오브젝트를 삭제합니다.
                Destroy(seedPrefab.gameObject);
    }

    public void DeletePlantsInArea(string sceneName)
    {
        List<PlantData> plantsToRemove = new List<PlantData>();

        // 영역 안에 있는 식물 찾기
        foreach (var plantData in _plantDatas)
        {
            if (plantData._sceneName == sceneName && IsPlantInArea(plantData._position))
            {
                Debug.Log("Add Plant To Remove : " + plantData._position);
                plantsToRemove.Add(plantData);
            }
        }

        // 찾은 식물 삭제
        foreach (var plant in plantsToRemove)
        {
            _plantDatas.Remove(plant);
            DeleteSeedPrefabAtPosition(plant._position);
        }
    }

    private bool IsPlantInArea(Vector3 plantPosition)
    {
        float halfWidth = _areaWidth / 2;
        float halfHeight = _areaHeight / 2;
        
        return plantPosition.x >= _areaCenter.x - halfWidth &&
            plantPosition.x <= _areaCenter.x + halfWidth &&
            plantPosition.z >= _areaCenter.z - halfHeight &&
            plantPosition.z <= _areaCenter.z + halfHeight;
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
