using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryDisplay : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _raceButton;
    [SerializeField] private Button _diseaseButton;
    [SerializeField] private Button _symptomButton;
    [SerializeField] private Button _potionButton;

    [Header("Display")]
    [SerializeField] private GameObject _raceDisplay;
    [SerializeField] private GameObject _diseaseDisplay;
    [SerializeField] private GameObject _symptomDisplay;
    [SerializeField] private GameObject _potionDisplay;

    [Header("Content")]
    [SerializeField] private GameObject _raceContent;
    [SerializeField] private GameObject _diseaseContent;
    [SerializeField] private GameObject _symptomContent;
    [SerializeField] private GameObject _potionContent;

    [Header("DataBase")]
    [SerializeField] private GuideBookDatabase _raceDatabase;
    [SerializeField] private GuideBookDatabase _symptomDatabase; 
    [SerializeField] private DiseaseDatabase _diseaseDatabase;

    [Header("Button UI")]
    [SerializeField] private HospitalGuideButtonUi _hospitalGuideButtonUi;
    [SerializeField] private HospitalDiseaseButtonUi _hospitalDiseaseButtonUi;

    private void Start() 
    {
        CreateRaceContent();
        CreateDiseaseContent();
        CreateSymptomContent();
    }

    public void OpenGuideBook()
    {
        ChangeCategory(0);
    }

    public void ChangeCategory(int index)
    {
        EnableAllButton();
        DisableAllDisplay();
        switch(index)
        {
            case 0:
                _raceButton.interactable = false;
                _raceDisplay.SetActive(true);
                break;
            case 1:
                _diseaseButton.interactable = false;
                _diseaseDisplay.SetActive(true);
                break;
            case 2:
                _symptomButton.interactable = false;
                _symptomDisplay.SetActive(true);
                break;
            case 3:
                _potionButton.interactable = false;
                _potionDisplay.SetActive(true);
                break;
        }
    }

    public void EnableAllButton()
    {
        _raceButton.interactable = true;
        _diseaseButton.interactable = true;
        _symptomButton.interactable = true;
        _potionButton.interactable = true;
    }
    public void DisableAllDisplay()
    {
        _raceDisplay.SetActive(false);
        _diseaseDisplay.SetActive(false);
        _symptomDisplay.SetActive(false);
        _potionDisplay.SetActive(false);
    }

    public void CreateRaceContent()
    {
        if(_raceDatabase._guideBookDatas.Count == 0) return;

        for(int i = 0; i < _raceDatabase._guideBookDatas.Count; i++)
        {
            var button = Instantiate(_hospitalGuideButtonUi, _raceContent.transform);
            button.AllocateData(_raceDatabase._guideBookDatas[i]);
        }
    }
    public void CreateDiseaseContent()
    {
        if(_diseaseDatabase._diseaseDatas.Count == 0) return;

        for(int i = 0; i < _diseaseDatabase._diseaseDatas.Count; i++)
        {
            var button = Instantiate(_hospitalDiseaseButtonUi, _diseaseContent.transform);
            button.AllocateData(_diseaseDatabase._diseaseDatas[i]);
        }
    }
    public void CreateSymptomContent()
    {
        if(_symptomDatabase._guideBookDatas.Count == 0) return;

        for(int i = 0; i < _symptomDatabase._guideBookDatas.Count; i++)
        {
            var button = Instantiate(_hospitalGuideButtonUi, _symptomContent.transform);
            button.AllocateData(_symptomDatabase._guideBookDatas[i]);
        }
    }
    public void CreatePotionContent()
    {
        
    }
}
