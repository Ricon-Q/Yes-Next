using PixelCrushers.DialogueSystem;
using UnityEngine;

public class Interaction_Bed : InteractionDialogue
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
    public void SleepAndSave(int FadeInOutTime)
    {
        // Debug.Log("SleepAndSave");
        int sleepTime = DialogueLua.GetVariable("SleepTime_Hour").asInt;
        // Debug.Log(sleepTime);
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
            case 12:
                FadeInOutManager.Instance.FadeInOut(FadeInOutTime);
                _TimeManager.Instance.SetTargetTimeHour(12);
                break;
            case 18:
                FadeInOutManager.Instance.FadeInOut(FadeInOutTime);
                _TimeManager.Instance.SetTargetTimeHour(18);
                break;
            case 24:
                FadeInOutManager.Instance.FadeInOut(FadeInOutTime);
                _TimeManager.Instance.SetTargetTimeHour(6);
                GameManager.Instance.StartNewDay();
                DataManager.Instance.SaveSlot();
                break;
            default:
                break;
        }
        // PlayerInputManager.Instance.SetInputMode(true);
        PlayerInputManager.SetPlayerInput(true);
    }
}
