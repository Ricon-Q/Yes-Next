using System.Collections;
using System.Collections.Generic;
using ES3PlayMaker;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    
    public void IsActive()
    {
        Debug.Log("Data Manager Is " + gameObject.activeSelf);
    }
    
    // string myKey = "myKey";
    // int myValue;
    string path;

    public int currentSaveIndex = -1;

    public MainMenu mainMenu;

    [System.Serializable]
    public class SaveData
    {
        public string LastSceneName; // 저장했을때의 위치해 있던 씬 이름
        public Vector3 playerPosition; // 플레이어의 위치
        public PlayerData playerData;
    }

    public void StartGame(int saveIndex)
    {
        currentSaveIndex = saveIndex;
        SceneManager.LoadScene("Game Setup");
    }

    public void SaveSlot()
    {
        // currentSaveIndex = saveIndex;
        
        SaveData saveData = new SaveData();

        saveData.LastSceneName = SceneManager.GetActiveScene().name;
        saveData.playerPosition = _PlayerManager.Instance.transform.position;
        saveData.playerData = _PlayerManager.Instance.playerData;

        path = "Saves/SaveSlot" + currentSaveIndex.ToString() + ".es3";
        ES3.Save("SaveData", saveData, path);
        
        // Debug.Log(saveIndex + " Save to : " + path);
    }

    public void LoadSlot(int loadIndex)
    {
        currentSaveIndex = loadIndex;
        path = "Saves/SaveSlot" + loadIndex.ToString() + ".es3";
        if(ES3.FileExists(path))
        {
            SaveData loadData = ES3.Load<SaveData>("SaveData", path);
            // loadData = ES3.Load<SaveData>("SaveData", path);

            _PlayerManager.Instance.transform.position = loadData.playerPosition;            
            _PlayerManager.Instance.playerData = loadData.playerData;

            FadeInOutManager.Instance.ChangeScene(loadData.LastSceneName);
        }
            // Debug.Log(loadIndex + " Load from : " + path);
        // else
            // Debug.Log("No such path");            
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
