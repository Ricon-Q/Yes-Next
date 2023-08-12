using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
    private static CameraManager instance;

    public static CameraManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject cameraManagerGO = new GameObject("CameraManager");
                instance = cameraManagerGO.AddComponent<CameraManager>();
            }
            return instance;
        }
    }
}