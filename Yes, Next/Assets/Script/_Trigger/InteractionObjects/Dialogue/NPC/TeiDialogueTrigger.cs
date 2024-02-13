using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;

public class TeiDialogueTrigger : npcDialogueTrigger
{
    override public void EndConversation()
    {
        bool _isHospitalTrigger = DialogueLua.GetVariable("hospitalTrigger").asBool;
        // Debug.Log(_isHospitalTrigger);
        if(!_isHospitalTrigger)
            PlayerInputManager.SetPlayerInput(true) ;
        else
            HospitalManager.Instance.EnterHospitalMode();
    }
}
