using PixelCrushers.DialogueSystem;
using UnityEngine;

public class Interaction_Bench : InteractionDialogue
{
    override public void Interaction()
    {
        if(dialogueSystemTrigger == null)
            Debug.Log("There is No interaction Function");
        else
        {
            // Debug.Log("Interaction");
            // PlayerInputManager.Instance.SetInputMode(false);
            
            MyInfomation.Instance.ExitMyInfomation();
            PlayerInputManager.SetPlayerInput(false);
            
            DialogueLua.SetVariable("CurrentTime_Hour", _TimeManager.Instance.timeData.hour);   
            dialogueSystemController.standardDialogueUI = nonNpcDialogueUi;
            dialogueSystemTrigger.OnUse();
        }
    }

    public void Sleep(int FadeInOutTime)
    {
        int sleepTime = DialogueLua.GetVariable("SleepTime_Hour").asInt;
        switch (sleepTime)
        {
            case 1:
                FadeInOutManager.Instance.FadeInOut(FadeInOutTime);
                _TimeManager.Instance.increaseHour(1);
                break;
            case 3:
                FadeInOutManager.Instance.FadeInOut(FadeInOutTime);
                _TimeManager.Instance.increaseHour(3);
                break;
            default:
                break;
        }
        // PlayerInputManager.Instance.SetInputMode(true);
        PlayerInputManager.SetPlayerInput(true);
    }
}
