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
    
    string myKey = "myKey";
    int myValue;
    string path;

    public int currentSaveIndex = -1;

    public MainMenu mainMenu;

    [System.Serializable]
    public class SaveData
    {
        public int SaveIndex;
        public string LastSceneName; // 저장했을때의 위치해 있던 씬 이름
        public Vector3 PlayerPosition; // 플레이어의 위치
    }

    public void NewGame(int saveIndex)
    {
        currentSaveIndex = saveIndex;
        SceneManager.LoadScene("Intro Scene");
    }

    public void SaveSlot()
    {
        // currentSaveIndex = saveIndex;
        
        SaveData saveData = new SaveData();

        saveData.SaveIndex = currentSaveIndex;
        saveData.LastSceneName = SceneManager.GetActiveScene().name;
        // saveData.PlayerPosition = PlayerManager.Instance.transform.position;

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
            SaveData loadData = new SaveData();
            loadData = ES3.Load<SaveData>("SaveData", path);
            // ES3.Load<SaveData>(path, out loadData);
            Debug.Log(loadData.LastSceneName);
            SceneManager.LoadScene(loadData.LastSceneName);
        }
            // Debug.Log(loadIndex + " Load from : " + path);
        else
            Debug.Log("No such path");            
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
