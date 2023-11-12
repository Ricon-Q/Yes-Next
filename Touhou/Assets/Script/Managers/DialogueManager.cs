// using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;

    public static DialogueManager Instance
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
            // DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    private TextMeshProUGUI Original_DialogueText;
    public GameObject continueStoryButton;
    private GameObject Original_ContinueStoryButton;

    [Header("Choices UI")]
    public GameObject[] choices;
    private GameObject[] Original_Choices;
    private TextMeshProUGUI[] choicesText;
    private TextMeshProUGUI[] Original_ChoicesText;
    private Dictionary<string, System.Action> choiceActions = new Dictionary<string, System.Action>();

    [SerializeField] private NPC npcScript;


    private Story currentStory;
    
    public bool dialogueIsPlaying { get; private set; }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        continueStoryButton.GetComponent<Button>().onClick.AddListener(OnContinueStoryButtonClicked);
        
        SetupChoiceText();
        BackupVariables();
        SetupChoiceAction();

        // 디버그용 코드
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

    private void BackupVariables()
    {
        Original_DialogueText = dialogueText;
        Original_Choices = choices;
        Original_ChoicesText = choicesText;
        Original_ContinueStoryButton = continueStoryButton;
    }
    private void RollbackVariables()
    {
        dialogueText = Original_DialogueText;
        choices = Original_Choices;
        choicesText = Original_ChoicesText;
        continueStoryButton = Original_ContinueStoryButton;
    }

    private void SetupChoiceAction()
    {
        choiceActions["Exit"] = ExitDialogueMode;
        choiceActions["Shop"] = EnterShopMode;
        choiceActions["Affection +2"] = AddAffection;
    }

    private void Update()
    {
        if(!dialogueIsPlaying)
        {
            return;
        }

        if(InputManager.Instance.GetSubmitPressed())
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON, NPC npcInfo)
    {
        currentStory = new Story(inkJSON.text);
        currentStory.variablesState["npcAffection"] = npcInfo.npcData.affection;
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        npcScript = npcInfo;

        ContinueStory();
    }
    private void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    public void HospitalChangePatienData(PatientData patientData)
    {
        Debug.Log("HospitalChangePatienData Called");
        currentStory = new Story(patientData.diseaseData.dialogueText.text);
        continueStoryButton.GetComponent<Button>().onClick.AddListener(OnContinueStoryButtonClicked);
        dialogueIsPlaying = true;
        
        ContinueStory();
    }
    public void EnterHospitalMode()
    {
        SetupChoiceText();
    }

    public void ExitHospitalMode()
    {
        RollbackVariables();
    }

    private void OnContinueStoryButtonClicked()
    {
        ContinueStory();
    }

    private void ContinueStory()
    {
        if(currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
            Debug.Log(dialogueText.text);
            DisplayChoices();
        }
        else
        {
            ExitDialogueMode();
        }
    }
    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

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

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }
    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }
    public void MakeChoice(int choiceIndex) 
    {
        Choice choice = currentStory.currentChoices[choiceIndex];
        string choiceText = choice.text;
        // Debug.Log(choiceText);

         // 선택지에 해당하는 함수 실행
        if (choiceActions.ContainsKey(choiceText))
        {
            choiceActions[choiceText].Invoke();
        }

        currentStory.ChooseChoiceIndex(choiceIndex);
    }

    public void EnterShopMode()
    {
        ShopManager.Instance.EnterShopMode(npcScript);
        ExitDialogueMode();
    }
    public void AddAffection()
    {
        npcScript.npcData.affection += 2;
        currentStory.variablesState["npcAffection"] = npcScript.npcData.affection;
    }
}