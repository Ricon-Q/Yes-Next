using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [Header("Book Panel")]
    [SerializeField] private GameObject _diseasePanel;
    [SerializeField] private TextMeshProUGUI _diseaseNameText;
    [SerializeField] private TextMeshProUGUI _diseaseDescriptionText;
    [SerializeField] private GameObject _raceSymptomPanel;
    [SerializeField] private TextMeshProUGUI _raceSymptomNameText;
    [SerializeField] private TextMeshProUGUI _raceSymptomDescriptionText;

    [Header("DataBase")]
    [SerializeField] private GuideBookDatabase _raceDatabase;
    [SerializeField] private GuideBookDatabase _raceSymptomDatabase; 
    [SerializeField] private DiseaseDatabase _diseaseDatabase;

}
