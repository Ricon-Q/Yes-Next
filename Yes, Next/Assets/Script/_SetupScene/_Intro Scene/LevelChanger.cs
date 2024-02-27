using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public AreaData townArea;
    public void LevelChange()
    {
        CameraManager.Instance.TogglePlayerCamera(true);

        _PlayerManager.Instance.playerData.currentArea = townArea.areaName;
        CameraManager.Instance.ChangeCameraBorder("Home");
        FadeInOutManager.Instance.ChangeScene("Town", new Vector3(-58.5f, -23f, 0));
        UiManager.Instance.ToggleUiCanvas(true);
    }   
}
