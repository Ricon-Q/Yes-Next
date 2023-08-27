// using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    private Dictionary<string, System.Action> choiceActions = new Dictionary<string, System.Action>();

    [SerializeField] private NPC npcScript;


    private Story currentStory;
    
    public bool dialogueIsPlaying { get; private set; }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach(GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }

        choiceActions["Exit"] = ExitDialogueMode;
        choiceActions["Shop"] = EnterShopMode;
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

    private void ContinueStory()
    {
        if(currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();

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
        Debug.Log(choiceText);

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

}