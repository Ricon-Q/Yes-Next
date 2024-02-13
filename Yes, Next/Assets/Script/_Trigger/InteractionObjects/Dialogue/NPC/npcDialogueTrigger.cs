using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcDialogueTrigger : InteractionDialogue
{
    public override void Interaction()
    {
        if(dialogueSystemTrigger == null)
            Debug.Log("There is No interaction Function");
        else
        {   
            PlayerInputManager.SetPlayerInput(false);
            dialogueSystemTrigger.OnUse();
        }
    }

    public void EndConversation()
    {
        PlayerInputManager.SetPlayerInput(true);
    }
}
