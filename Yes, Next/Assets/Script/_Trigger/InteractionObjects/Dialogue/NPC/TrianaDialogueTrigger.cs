using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class TrianaDialogueTrigger : npcDialogueTrigger
{
    override public void EndConversation()
    {
        bool _isGuildQuestTrigger = DialogueLua.GetVariable("guildQuestTrigger").asBool;
        // Debug.Log(_isHospitalTrigger);
        if(!_isGuildQuestTrigger)
            PlayerInputManager.SetPlayerInput(true) ;
        else
            QuestManager.Instance.EnterQuestMenu();
    }
}
