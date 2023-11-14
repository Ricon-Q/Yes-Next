using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class PatientObject : MonoBehaviour
{
    
    private PatientData patientData;    // 환자 정보를 저장
    private PlayerManager playerManager;    // 병원의 수입금, 평판을 관리할 PlayerManger

    // [SerializeField] private TextMeshProUGUI patientName;    // 환자 이름 출력
    [Header("Dialogue UI")]
    [SerializeField] private TextMeshProUGUI dialogueText;    // 환자 대화창
    // [SerializeField] private GameObject DialogueContitnueImg; 
    [SerializeField] private GameObject continueStoryButton;

    [Header("Choices UI")]
    private TextMeshProUGUI[] choicesText;   // 선택지 배열
    [SerializeField] private GameObject[] choices;
    private Story currentStory; // 환자 대화 정보

    private void Awake()
    {
        playerManager = PlayerManager.Instance;

        SetupVariables();
        DialogueManager.Instance.EnterHospitalMode();
    }
    
    public void GeneratePatientData(PatientData patientData)
    {
        this.patientData = patientData;

        PatientMoveAnimation(true);
        DialogueManager.Instance.HospitalChangePatienData(patientData);

        return;
    }

    private void SetupVariables()
    {
        DialogueManager.Instance.dialogueText = this.dialogueText;
        DialogueManager.Instance.choices = this.choices;
        DialogueManager.Instance.continueStoryButton = this.continueStoryButton;
    }

    public void ChoiceButton(int index)
    {
        DialogueManager.Instance.MakeChoice(index);
    }

    private void PatientMoveAnimation(bool isEnterAnimation)
    {
        if(isEnterAnimation)
        {

        }
        else
        {

        }
    }
}
