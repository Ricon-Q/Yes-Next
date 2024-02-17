using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class InteractionDialogue : MonoBehaviour
{
    // 상호작용 가능 물건중 대화창이 있는 물건
    protected bool playerInRange;
    
    [Header("Visual Cue")]
    [SerializeField] protected GameObject visualCue;

    [Header("Dialogue System")]
    [SerializeField] protected DialogueSystemTrigger dialogueSystemTrigger;
    [SerializeField] protected DialogueSystemController dialogueSystemController;
    [SerializeField] protected StandardDialogueUI npcDialogueUi;
    [SerializeField] protected StandardDialogueUI nonNpcDialogueUi;
    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
        dialogueSystemController = GameObject.Find("Dialogue Manager").GetComponent<DialogueSystemController>();
    }

    private void Update()
    {
        if(playerInRange)
        {
            visualCue.SetActive(true);
            if(InputManager.Instance.GetInteractPressed())
            {
                Interaction();
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    public virtual void Interaction()
    {
        if(dialogueSystemTrigger == null)
            Debug.Log("There is No interaction Function");
        else
        {
            dialogueSystemController.standardDialogueUI = nonNpcDialogueUi;
            dialogueSystemTrigger.OnUse();
        }
    }

    public void SetUiDefault()
    {
        dialogueSystemController.dialogueUI = npcDialogueUi;
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if(collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collider) 
    {
         if(collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
