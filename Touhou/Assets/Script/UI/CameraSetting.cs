using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CameraSetting : MonoBehaviour
{
    public List<string> cameraOffSceneList = new List<string>();

    public GameObject cameraObject;
    string sceneName;

    public void controlCameraObject()
    {
        sceneName = SceneManager.GetActiveScene().name;
        if(IsStringInList(sceneName)) { cameraObject.SetActive(false); }
        else { cameraObject.SetActive(true); }
    }
    
    public bool IsStringInList(string sceneName)
    {
        foreach (string item in cameraOffSceneList)
        {
            if (item.Equals(sceneName))
            {
                return true;
            }
        }
        return false;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        controlCameraObject();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
