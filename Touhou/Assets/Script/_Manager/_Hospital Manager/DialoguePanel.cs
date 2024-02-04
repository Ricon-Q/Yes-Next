using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using PixelCrushers.DialogueSystem.UnityGUI;
using UnityEngine;

public class DialoguePanel : MonoBehaviour
{
    [Header("Patient Database")]
    public PatientDatabase _patientDatabase;
    public List<PatientData> _patientDatas;

    [Header("Dialogue Database")]
    public DialogueSystemController _dialogueSystemController;
    public StandardDialogueUI _hospitalDialogueUi;
    public StandardDialogueUI _mainDialogueUi;

    private int _patientIndex;
    public void EnterHospitalMode()
    {
        _patientIndex = 0;
        _dialogueSystemController.standardDialogueUI = _hospitalDialogueUi;
        _patientDatas = new List<PatientData>(_patientDatabase.GetRandomPatientData());
        GetNextPatient();
    }

    public void ExitHospitalMode()
    {
        if(_dialogueSystemController.isConversationActive)
            _dialogueSystemController.StopConversation();
        _dialogueSystemController.standardDialogueUI = _mainDialogueUi;
    }

    public void GetNextPatient()
    {
        if(_patientIndex < _patientDatas.Count)
        {
            HospitalManager.Instance._diagnosisPanel.AllocatePatientData(_patientDatas[_patientIndex]);
            _dialogueSystemController.StartConversation(_patientDatas[_patientIndex]._conversationTitle);
            _patientIndex++;
        }
        else
            return;
    }

    public void EndConversation()
    {
        _dialogueSystemController.StopConversation();
        GetNextPatient();
    }
}
