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
        
        _diseasePanel.SetActive(false);
        _raceSymptomPanel.SetActive(false);
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

    // [Header("DataBase")]
    // [SerializeField] private GuideBookDatabase _raceDatabase;
    // [SerializeField] private GuideBookDatabase _raceSymptomDatabase; 
    // [SerializeField] private DiseaseDatabase _diseaseDatabase;

    public void DisplayDisease(DiseaseData diseaseData)
    {
        _diseasePanel.SetActive(true);
        _raceSymptomPanel.SetActive(false);

        _diseaseNameText.text = diseaseData._name;
        _diseaseDescriptionText.text = diseaseData._description;
    }

    public void DisplayRaceSymptom(GuideBookData guideBookData)
    {
        _diseasePanel.SetActive(false);
        _raceSymptomPanel.SetActive(true);

        _raceSymptomNameText.text = guideBookData._name;
        _raceSymptomDescriptionText.text = guideBookData._description;
    }
}
