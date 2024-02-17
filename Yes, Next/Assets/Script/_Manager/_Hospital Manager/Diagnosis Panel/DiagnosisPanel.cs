using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
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

    public DiagnosisData _diagnosisData;
    public void AllocatePatientData(PatientData patientData)
    {
        _answerPatientData = new AnswerPatientData();
        _allocatedPatientData = patientData;
    }

    public void SubmitDiagnosis()
    {
        CheckDiagnosis();
        HospitalManager.Instance._dialoguePanel.EndConversation();
        _diagnosisData = new DiagnosisData();
    }
    
    public void CheckDiagnosis()
    {
        
    }

    public void ChangeRace(TMP_Dropdown change)
    {
        _diagnosisData._race = _raceDatabase.FindData(change.options[change.value].text);
    }
    public void ChangeDisease(TMP_Dropdown change)
    {
        _diagnosisData._diseaseData = _diseaseDatabase.FindData(change.options[change.value].text);
    }
    public void ChangeSymptom0(TMP_Dropdown change)
    {
        _diagnosisData._symptom0 = _symptomDatabase.FindData(change.options[change.value].text);
    }
    public void ChangeSymptom1(TMP_Dropdown change)
    {
        _diagnosisData._symptom1 = _symptomDatabase.FindData(change.options[change.value].text);
    }
    public void ChangeSymptom2(TMP_Dropdown change)
    {
        _diagnosisData._symptom2 = _symptomDatabase.FindData(change.options[change.value].text);
    }
}

public class DiagnosisData
{
    // 종족
    // 증상 1, 2, 3
    // 질병
    public GuideBookData _race;
    public GuideBookData _symptom0;
    public GuideBookData _symptom1;
    public GuideBookData _symptom2;
    public DiseaseData _diseaseData;

    public DiagnosisData()
    {
        _race = null;
        _symptom0 = null;
        _symptom1 = null;
        _symptom2 = null;
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
