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

    [Header("Diagnosis")]
    [SerializeField] private TMP_Dropdown _disaseDropdown;
    
    public void AllocatePatientData(PatientData patientData)
    {
        _answerPatientData = new AnswerPatientData();
        _allocatedPatientData = patientData;
    }

    public void ChangeDisase()
    {
        _answerPatientData._disaseName = _disaseDropdown.options[_disaseDropdown.value].text;
    }

    public void SubmitDiagnosis()
    {
        HospitalManager.Instance._dialoguePanel.EndConversation();
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
