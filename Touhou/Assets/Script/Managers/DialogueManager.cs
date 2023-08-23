using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TalkManager talkManager;
    public GameObject talkPanel;
    public TextMeshProUGUI talkText;
    public GameObject scanObject;

    public bool isAction;
    public int talkIndex;

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
    
    public void Interact(GameObject scanObj)
    {

        if(scanObj.CompareTag("Door"))  
        {
            DoorInteract(scanObj); 
            return;
        }
        else 
            DefaultInteract(scanObj);
        
    }

    public void DoorInteract(GameObject scanObj)
    {
        scanObj.GetComponent<Door>().Interact(scanObj); 
    }

    public void DefaultInteract(GameObject scanObj)
    {
        
        scanObject = scanObj;
        ObjectData objData = scanObj.GetComponent<ObjectData>();
        Talk(objData.id, objData.isNpc);
        
        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);
        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            return;
        }
        if(isNpc)
        {
            talkText.text = talkData;
        }
        else
        {
            talkText.text = talkData;
        }

        isAction = true;
        talkIndex++;
    }
}
