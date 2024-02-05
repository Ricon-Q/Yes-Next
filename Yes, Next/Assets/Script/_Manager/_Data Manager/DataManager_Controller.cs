using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager_Controller : MonoBehaviour
{
    public void Save()
    {
        DataManager.Instance.SaveSlot();
    }
}
