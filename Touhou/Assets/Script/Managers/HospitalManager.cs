using System.Collections;
using System.Collections.Generic;
using TMPro;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


// Hospital Phase 싱글톤

public class HospitalManager : MonoBehaviour
{
    private static HospitalManager instance;
    public static HospitalManager Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }
            return instance;
        }
    }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        else { Destroy(this.gameObject); }
    }

    private bool isHospitalMode;

    [Header("Patient DataBase")]
    [SerializeField] private PatientDataBase patientDataBase; // 환자 데이터 베이스
    private Queue<PatientData> patientQueue; // 환자 큐
    private PatientData currentPatientData;

    [Header("Panel")]
    [SerializeField] private GameObject hospitalPanel;
    [SerializeField] private GameObject patientPanel;
    
    [Header("Patient Dialogue UI")]
    [SerializeField] private GameObject patientDialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject continueStoryButton;

    [Header("Choice UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    [SerializeField] private Dictionary<string, System.Action> choiceActions = 
                        new Dictionary<string, System.Action>();

    [Header("Hospital Inventory Display")]
    [SerializeField] private GameObject hospitalInventoryDisplayObject;
    [SerializeField] private HospitalInventoryDisplay hospitalInventoryDisplay;

    private Story patientStory;

    private void Start()
    {
        isHospitalMode = false;
        hospitalPanel.SetActive(false);
        patientPanel.SetActive(false);
        patientDialoguePanel.SetActive(false);
        SetupChoiceText();
        ToggleHospitalInventoryDisplay(false);
        SetupChoiceAction();
    }

    private void SetupChoiceText()
    {
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach(GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
        
            index++;
        }
    }
    private void SetupChoiceAction()
    {
        Debug.Log("SetupChoiceAction");
        choiceActions["처방하기"] = Prescribe;
        choiceActions["질문하기"] = Ask;
        choiceActions["진료포기"] = GiveUp;
        choiceActions["취소"] = Cancle;
    }

    public void StartHospitalMode()
    {
        Debug.Log("Start Hospital Mode Called");

        // 병원 레벨 정보 가져오기 및 환자 수 설정
        long hospitalLevel = PlayerManager.Instance.playerData.hospitalLevel;
        long patientCount = (long)UnityEngine.Random.Range(hospitalLevel * 5 - 3, hospitalLevel * 5);

        isHospitalMode = true;
        hospitalPanel.SetActive(true);

        patientQueue = new Queue<PatientData>();
        GeneratePatientQueue(patientCount);
        
        StartPatientPhase();
    }

    private void GeneratePatientQueue(long patientCount)
    {
        // 환자 수에 따라 데이터 베이스에서 랜덤하게 환자정보를 가져와 큐에 채워넣는 함수
        for(int i = 0; i < patientCount; i++)
        {
            if(patientDataBase.Items != null)
            {
                int randomPatientID = UnityEngine.Random.Range(0, patientDataBase.Items.Length);
                patientQueue.Enqueue(patientDataBase.Items[randomPatientID]);
            }
        }
    }

    private void StartPatientPhase()
    {
        // 환자 큐 설정이 완료된 이후에 환자 페이즈 시작
        // 환자 큐에 정보가 남아있다면 Dequeue한 이후에 해당 정보를 가지고 함수 실행
    
        if(patientQueue.Count > 0)
        {
            // 환자 큐에 정보가 남아있다면, patientData를 진료 함수로 넘겨서 실행
            currentPatientData = patientQueue.Dequeue();
            Diagnosis();
        }
        else
        {
            // 환자 큐에 정보가 남아있지 않다면 병원 페이즈의 정산 화면 보여준 뒤 모드 종료
            EndPatientPhase();
        }
    }

    private void Diagnosis()
    {
        patientStory = new Story(currentPatientData.diseaseData.dialogueText.text);
        patientPanel.SetActive(true);
        patientDialoguePanel.SetActive(true);

        ContinueStory();
    }
    private void EndDiagnosis()
    {
        Debug.Log("End Diagnosis");
        patientPanel.SetActive(true);
        patientDialoguePanel.SetActive(true);
        StartPatientPhase();
    }

    public void ContinueStory()
    {
        if(patientStory.canContinue)
        {
            dialogueText.text = patientStory.Continue();
            
            if(patientStory.currentChoices.Count > 0)
            {
                DisplayChoices();
            }
            else
            {
                HideChoices();
            }
        }
        else
        {
            EndDiagnosis();
        }
    }

    private void DisplayChoices()
    {
        continueStoryButton.SetActive(false);

        List<Choice> currentChoices = patientStory.currentChoices;
        if(currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given : " + currentChoices.Count);
        }

        int index = 0;
        foreach(Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for(int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private void HideChoices()
    {
        
        continueStoryButton.SetActive(true);
        for(int i = 0; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        Choice choice = patientStory.currentChoices[choiceIndex];
        string choiceText = choice.text;
        Debug.Log(choiceText);

        //  선택지에 해당하는 함수 실행
        if (choiceActions.ContainsKey(choiceText))
        {
            choiceActions[choiceText].Invoke();
        }

        patientStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }

    private void EndPatientPhase()
    {
        Debug.Log("환자 페이즈 종료");
        isHospitalMode = false;
        
        patientPanel.SetActive(false);
        patientDialoguePanel.SetActive(false);
        hospitalPanel.SetActive(false);
        SceneManager.LoadScene("Debug_Main");
    }

    public void Prescribe()
    {   
        ToggleHospitalInventoryDisplay(true);
        Debug.Log("Prescribe");
        hospitalInventoryDisplay.Display();
    }

    public void Ask()
    {
        Debug.Log("Ask");
    }

    public void GiveUp()
    {
        Debug.Log("GiveUp");
    }
    
    public void Cancle()
    {
        if(hospitalInventoryDisplay.isDisplayOpen == true)
            ToggleHospitalInventoryDisplay(false);
    }

    private void ToggleHospitalInventoryDisplay(bool active)
    {
        if(active == true)
        {
            hospitalInventoryDisplay.isDisplayOpen = true;
            hospitalInventoryDisplayObject.SetActive(true);
        }

        else
        {
            hospitalInventoryDisplayObject.SetActive(false);
            hospitalInventoryDisplay.isDisplayOpen = false;
        }
    } 

    public void GiveMedicine(InventoryItemData itemData)
    {
        ToggleHospitalInventoryDisplay(false);
        if(itemData == currentPatientData.diseaseData.correctMedicine)
        {
            Debug.Log("Correct Medicine");
            patientStory.ChoosePathString("Correct");
            ContinueStory();
        }
        else
        {
            Debug.Log("Incorrect Medicine");
            patientStory.ChoosePathString("Incorrect");
            ContinueStory();
        }
    }
}
