using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HospitalGuideBook : MonoBehaviour
{
    [SerializeField] private GameObject _guideBookObject;
    [Header("Category Display")]
    [SerializeField] private CategoryDisplay _categoryDisplay;
    
    public void EnterHospitalMode()
    {
        CloseGuideBook();
    }
    public void OpenGuideBook()
    {
        _guideBookObject.SetActive(true);
        _categoryDisplay.OpenGuideBook();
    }

    public void CloseGuideBook()
    {
        _guideBookObject.SetActive(false);
    }

}
