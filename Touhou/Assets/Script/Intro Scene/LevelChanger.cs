using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public void LevelChange()
    {
        FadeInOutManager.Instance.ChangeScene("Town_Center", new Vector3(5, 0, 0));
    }   
}
