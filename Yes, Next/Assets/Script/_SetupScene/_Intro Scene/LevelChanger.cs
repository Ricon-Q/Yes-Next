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
        FadeInOutManager.Instance.ChangeScene("Town", new Vector3(5, 0, 0));
        UiManager.Instance.ToggleUiCanvas(true);
    }   
}
