using System.Collections;
using System.Collections.Generic;
using ES3PlayMaker;
using UnityEngine;
using UnityEngine.SceneManagement;
using ES3Types;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;

    public static DataManager Instance
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
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }
    
    private void Start() 
    {
    }
    // =========================================================//

    public void IsActive()
    {
        Debug.Log("Data Manager Is " + gameObject.activeSelf);
    }
    
    // string myKey = "myKey";
    // int myValue;
    string path;
    
    public int currentSaveIndex = -1;

    // public MainMenu mainMenu;

    // [System.Serializable]
    // public class PlayerSaveData
    // {
    //     public string LastSceneName; // 저장했을때의 위치해 있던 씬 이름
    //     public Vector3 playerPosition; // 플레이어의 위치
    //     public PlayerData playerData;
    //     public _TimeData timeData;
    // }

    // public class PlayerInventoryData
    // {
    //     public int playerInventoryLevel;
    //     public List<_InventorySlot> playerInventory;
    //     // public Inventory playerInventory;
    // }

    public PlayerSaveData loadData;

    public void StartGame(int saveIndex)
    {
        currentSaveIndex = saveIndex;
        SceneManager.LoadScene("Game Setup");
    }

    public void SaveSlot()
    {
        // currentSaveIndex = saveIndex;
        
        PlayerSaveData saveData = new PlayerSaveData();

        // SaveData 저장
        saveData.LastSceneName = SceneManager.GetActiveScene().name;
        saveData.playerPosition = _PlayerManager.Instance.transform.position;
        saveData.playerData = _PlayerManager.Instance.playerData;
        saveData.timeData = _TimeManager.Instance.timeData;

        path = "Saves/SaveSlot" + currentSaveIndex.ToString() + ".es3";
        ES3.Save("PlayerSaveData", saveData, path);

        // EventData 저장
        EventData _eventData = new EventData();
        _eventData._eventDictionary = EventManager.Instance._eventDictionary;
        ES3.Save("EventData", _eventData, path);

        // 인벤토리 저장
        SaveInventory();

        // 오브젝트 저장
        SaveObjectData();
        
        // Debug.Log(saveIndex + " Save to : " + path);
    }

    public void SaveInventory()
    {
        PlayerInventoryData saveInventory = new PlayerInventoryData();
        saveInventory.playerInventoryLevel = PlayerInventoryManager.Instance.playerInventoryLevel;
        saveInventory.playerInventory = PlayerInventoryManager.Instance.playerInventory.inventorySlots;

        saveInventory.herbInventoryLevel = PlayerInventoryManager.Instance.herbInventoryLevel;
        saveInventory.herbInventory = PlayerInventoryManager.Instance.herbInventory.inventorySlots;
        
        saveInventory.potionInventoryLevel = PlayerInventoryManager.Instance.potionInventoryLevel;
        saveInventory.potionInventory = PlayerInventoryManager.Instance.potionInventory.inventorySlots;

        // saveInventory.playerInventory = PlayerInventoryManager.Instance.playerInventory;

        path = "Saves/SaveSlot" + currentSaveIndex.ToString() + ".es3";
        ES3.Save("PlayerInventoryData", saveInventory, path);
    }

    public void SaveObjectData()
    {
        ObjectSaveData _objectSaveData = new ObjectSaveData();
        _objectSaveData._placeableObjects = ObjectManager.Instance._placeableObjects;

        path = "Saves/SaveSlot" + currentSaveIndex.ToString() + ".es3";
        ES3.Save("ObjectSaveData", _objectSaveData, path);
    }

    public void LoadSlot(int loadIndex)
    {
        currentSaveIndex = loadIndex;
        path = "Saves/SaveSlot" + loadIndex.ToString() + ".es3";
        if(ES3.FileExists(path))
        {
            loadData = ES3.Load<PlayerSaveData>("PlayerSaveData", path);
            // loadData = ES3.Load<SaveData>("SaveData", path);

            _PlayerManager.Instance.transform.position = loadData.playerPosition;            
            _PlayerManager.Instance.playerData = loadData.playerData;
            _TimeManager.Instance.timeData = loadData.timeData;

            // EventData 불러오기
            EventData _loadEventData = ES3.Load<EventData>("EventData", path);
            EventManager.Instance._eventDictionary = _loadEventData._eventDictionary;

            // 오브젝트 불러오기
            LoadObjectData(loadIndex);
        }            
    }

    public void LoadObjectData(int loadIndex)
    {
        path = "Saves/SaveSlot" + loadIndex.ToString() + ".es3";
        ObjectSaveData _loadObjectSaveData = ES3.Load<ObjectSaveData>("ObjectSaveData", path);
        ObjectManager.Instance._placeableObjects = _loadObjectSaveData._placeableObjects;
    }

    public PlayerSaveData LoadInfo(int loadIndex)
    {
        path = "Saves/SaveSlot" + loadIndex.ToString() + ".es3";
        PlayerSaveData result;
        if(ES3.FileExists(path))
        {
            result = ES3.Load<PlayerSaveData>("PlayerSaveData", path);
            return result;
        }
        return null;
    }

    public void LoadInventory(int loadIndex)
    {
        PlayerInventoryData loadInventoryData;
        currentSaveIndex = loadIndex;
        path = "Saves/SaveSlot" + loadIndex.ToString() + ".es3";
        if(ES3.FileExists(path))
        {
            loadInventoryData = ES3.Load<PlayerInventoryData>("PlayerInventoryData", path);
            PlayerInventoryManager.Instance.playerInventoryLevel = loadInventoryData.playerInventoryLevel;
            PlayerInventoryManager.Instance.herbInventoryLevel = loadInventoryData.herbInventoryLevel;
            PlayerInventoryManager.Instance.potionInventoryLevel = loadInventoryData.potionInventoryLevel;
            
            PlayerInventoryManager.Instance.GeneratePlayerInventory();
            
            PlayerInventoryManager.Instance.playerInventory.inventorySlots = loadInventoryData.playerInventory;
            PlayerInventoryManager.Instance.herbInventory.inventorySlots = loadInventoryData.herbInventory;
            PlayerInventoryManager.Instance.potionInventory.inventorySlots = loadInventoryData.potionInventory;
        }
    }

    public bool CheckSaveSlot()
    {
        path = "Saves/SaveSlot" + currentSaveIndex + ".es3";
        if(ES3.FileExists(path))
        {
            Debug.Log("File Exists");
            return true;
        }
        else
        {   
            Debug.Log("File Does Not Exists");
            return false;
        }

    }

    public void DeleteSave(int deleteIndex)
    {
        path = "Saves/SaveSlot" + deleteIndex.ToString() + ".es3";
        if(ES3.FileExists(path))
            ES3.DeleteFile(path);
        else
            Debug.Log("No such path");
    }
}

[System.Serializable]
public class PlayerSaveData
{
    public string LastSceneName; // 저장했을때의 위치해 있던 씬 이름
    public Vector3 playerPosition; // 플레이어의 위치
    public PlayerData playerData;
    public _TimeData timeData;
}

public class PlayerInventoryData
{
    public int playerInventoryLevel;
    public int herbInventoryLevel;
    public int potionInventoryLevel;
    public List<_InventorySlot> playerInventory;
    public List<_InventorySlot> herbInventory;
    public List<_InventorySlot> potionInventory;
    // public Inventory playerInventory;
}

public class EventData
{
    public Dictionary<int, Event> _eventDictionary;
}

public class ObjectSaveData
{
    public List<PlaceableObject> _placeableObjects;
}