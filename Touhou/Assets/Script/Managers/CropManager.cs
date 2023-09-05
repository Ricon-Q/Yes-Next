using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CropManager : MonoBehaviour, IDataPersistence
{
    private static CropManager instance;
    public List<CropData> cropDatas;
    public string currentSceneName;
    public static CropManager Instance
    {
        get
        {
            if(instance == null)
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
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;    
    }

    private void OnDisable() 
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;    
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GetCurrentSceneName();
        InitiateCropObject();
    }

    public void OnSceneUnloaded(Scene scene)
    {
        GetCurrentSceneName();
        Debug.Log(currentSceneName + " | OnSceneUnloaded called");
    }

    public void LoadData(GameData data)
    {
        this.cropDatas = data.cropDatas;
    }

    public void SaveData(ref GameData data)
    {
        data.cropDatas = this.cropDatas;
    }

    public void GetCurrentSceneName()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    public void AddCropData(CropData cropData)
    {
        GetCurrentSceneName();
        cropDatas.Add(cropData);
    }

    public void RemoveCropData(CropData cropData)
    {
        GetCurrentSceneName();
        cropDatas.Remove(cropData);
    }

    public void RemoveAllCropDataInScene()
    {
        GetCurrentSceneName();
        foreach (var cropData in cropDatas)
        {
            if(cropData.sceneName == currentSceneName)
            {
                cropDatas.Remove(cropData);
            }
        }
    }

    public void InitiateCropObject()
    {
        GetCurrentSceneName();
        foreach (var cropObject in cropDatas)
        {
            if(cropObject.sceneName == currentSceneName)
            {
                GameObject obj = Resources.Load<GameObject>($"Prefabs/Original/{cropObject.name}");
                GameObject instantiatedObj = Instantiate(
                                                                obj, 
                                                                cropObject.position, 
                                                                Quaternion.identity
                                                            );
                Crop cropScript = instantiatedObj.GetComponent<Crop>();
                cropScript.spawnedBefore = true;
                cropScript.cropData = cropObject;
            }
            
        }
    }
}