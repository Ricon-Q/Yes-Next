using UnityEngine;
using System;

public class SaveManager : MonoBehaviour
{
    private static SaveManager instance;

    [System.Serializable]
    public class SceneData
    {
        public string sceneName;
        public Vector3 playerPosition;
    }

    public SceneData[] savedSceneData;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static SaveManager Instance
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

    public void SavePlayerPosition(string sceneName, Vector3 position)
    {
        for (int i = 0; i < savedSceneData.Length; i++)
        {
            if (savedSceneData[i].sceneName == sceneName)
            {
                savedSceneData[i].playerPosition = position;
                Debug.Log("SceneName : " + savedSceneData[i].sceneName + " | Position : " + savedSceneData[i].playerPosition);
                return;
            }
        }

        AddNewScene(sceneName, position);

        // SceneData newSceneData = new SceneData();
        // newSceneData.sceneName = sceneName;
        // newSceneData.playerPosition = position;

        // // 배열 크기 확장 및 새로운 SceneData 추가
        // int newArrayLength = savedSceneData.Length + 1;
        // Array.Resize(ref savedSceneData, newArrayLength);
        // savedSceneData[newArrayLength - 1] = newSceneData;

        // Debug.Log("New SceneData created - SceneName : " + newSceneData.sceneName + " | Position : " + newSceneData.playerPosition);

    }

    public Vector3 LoadPlayerPosition(string sceneName)
    {
        for (int i = 0; i < savedSceneData.Length; i++)
        {
            if (savedSceneData[i].sceneName == sceneName)
            {
                Debug.Log("SceneName : " + savedSceneData[i].sceneName + " | Position : " + savedSceneData[i].playerPosition);
                return savedSceneData[i].playerPosition;
            }
        }

        return Vector3.zero; // 기본값
    }

    private void AddNewScene(string sceneName, Vector3 position)
    {
        SceneData newSceneData = new SceneData();
        newSceneData.sceneName = sceneName;
        newSceneData.playerPosition = position;

        // 배열 크기 확장 및 새로운 SceneData 추가
        int newArrayLength = savedSceneData.Length + 1;
        Array.Resize(ref savedSceneData, newArrayLength);
        savedSceneData[newArrayLength - 1] = newSceneData;

        Debug.Log("New SceneData created - SceneName : " + newSceneData.sceneName + " | Position : " + newSceneData.playerPosition);
    }
}