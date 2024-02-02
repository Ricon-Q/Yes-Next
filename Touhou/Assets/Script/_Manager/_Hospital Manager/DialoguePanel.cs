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
    public void EnterHospitalMode()
    {
        _dialogueSystemController.standardDialogueUI = _hospitalDialogueUi;
        _patientDatas = new List<PatientData>(_patientDatabase.GetRandomPatientData());
        TestFunc();
    }

    public void ExitHospitalMode()
    {
        _dialogueSystemController.standardDialogueUI = _mainDialogueUi;
    }


    public void TestFunc()
    {
        _dialogueSystemController.StartConversation(_patientDatas[0]._conversationTitle);
    }
}
