using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PixelCrushers.DialogueSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class DiagnosisPanel : MonoBehaviour
{
    [Header("Patient Data")]
    [SerializeField] private PatientData _allocatedPatientData;
    public AnswerPatientData _answerPatientData;
    [Header("Database")]
    [SerializeField] private GuideBookDatabase _raceDatabase;
    [SerializeField] private GuideBookDatabase _symptomDatabase; 
    [SerializeField] private DiseaseDatabase _diseaseDatabase;

    [Header("Drop Down")]
    [SerializeField] private TMP_Dropdown _raceDropdown;
    [SerializeField] private List<TMP_Dropdown> _symptomDropdowns;
    [SerializeField] private TMP_Dropdown _diseaseDropdown;

    [Header("Diagnosis Slot Display")]
    [SerializeField] private DiagnosisSlotDisplay _diagnosisSlotDisplay;

    [Header("Submit Button")]
    public Button _submitButton;

    public DiagnosisData _diagnosisData;
    public void AllocatePatientData(PatientData patientData)
    {
        _answerPatientData = new AnswerPatientData();
        _allocatedPatientData = patientData;
    }

    private void Update() 
    {
        if(DialogueLua.GetVariable("_giveupDiagnosis").asBool)
            GiveupPatient();
    }

    public void SubmitDiagnosis()
    {
        CheckDiagnosis();
        // DialogueSystemController.SendMessage("Continue", "NodeName");
        HospitalManager.Instance._dialoguePanel.EndConversation();

        HospitalManager.Instance._yesNext.interactable = true;
        HospitalManager.Instance._endHospital.interactable = true;
        _submitButton.interactable = false;

        if(_diagnosisSlotDisplay.inventorySystem.inventorySlots[0].itemId != -1)
        {
            _diagnosisSlotDisplay.inventorySystem.inventorySlots[0].RemoveFromStack(1);
            _diagnosisSlotDisplay.RefreshDynamicInventory(_diagnosisSlotDisplay.inventorySystem);
        }
    }

    public void NextPatient()
    {
        SetupDiagnosis();
        HospitalManager.Instance._dialoguePanel.StartConversation();

        HospitalManager.Instance._yesNext.interactable = false;
        HospitalManager.Instance._endHospital.interactable = false;
        
        _submitButton.interactable = true;
    }

    public void GiveupPatient()
    {
        DialogueLua.SetVariable("_giveupDiagnosis", false);
        HospitalManager.Instance._dialoguePanel.EndConversation();

        HospitalManager.Instance._yesNext.interactable = true;
        HospitalManager.Instance._endHospital.interactable = true;
        _submitButton.interactable = false;
    }
    
    public void CheckDiagnosis()
    {
        int _score = 0;
        if(_diagnosisData._race == _allocatedPatientData._race) 
            _score += 20;
        
        foreach (var item in _diagnosisData._symptoms)
            if(_allocatedPatientData._diseaseData._symptomDatas.Contains(item)) 
                _score += 20;

        if(_diagnosisData._diseaseData == _allocatedPatientData._diseaseData)
            _score += 20;
        
        if(_diagnosisSlotDisplay.inventorySystem.inventorySlots[0].itemId == _allocatedPatientData._potionItemData.ID)
            _score += 20;
        else if(_diagnosisSlotDisplay.inventorySystem.inventorySlots[0].itemId == -1)
            _score += 0;
        else if(_diagnosisSlotDisplay.inventorySystem.inventorySlots[0].itemId != _allocatedPatientData._potionItemData.ID)
            _score += -10;
        Debug.Log(_score);
        _PlayerManager.Instance.playerData.money += _score;
        _PlayerManager.Instance.playerData.currentStamina -= 3;
        
    }
    public void SetupDiagnosis()
    {
        ChangeRace();
        ChangeDisease();
        for (int i = 0; i < 3; i++)
        {
            ChangeSymptom(i);
        }
    }

    public void ChangeRace()
    {
        _diagnosisData._race = _raceDatabase.FindData(_raceDropdown.options[_raceDropdown.value].text);
    }
    public void ChangeDisease()
    {
        _diagnosisData._diseaseData = _diseaseDatabase.FindData(_diseaseDropdown.options[_diseaseDropdown.value].text);
    }
    public void ChangeSymptom(int index)
    {
        _diagnosisData._symptoms[index] = _symptomDatabase.FindData(_symptomDropdowns[index].options[_symptomDropdowns[index].value].text);
    }
}

public class DiagnosisData
{
    // 종족
    // 증상 1, 2, 3
    // 질병
    public GuideBookData _race;
    public List<GuideBookData> _symptoms;
    public DiseaseData _diseaseData;

    public DiagnosisData()
    {
        _race = null;
        _symptoms = new List<GuideBookData>(3);
        for (int i = 0; i < 3; i++)
        {
            _symptoms.Add(null); 
        }
        _diseaseData = null;
    }
}

public class AnswerPatientData
{
    [Header("Disase")]
    // 질병 종류
    public string _disaseName;
    // 포션 데이터
    public InventoryItemData _potionItemData;

    public AnswerPatientData()
    {
        _disaseName ="";
        _potionItemData = null;
    }
}
