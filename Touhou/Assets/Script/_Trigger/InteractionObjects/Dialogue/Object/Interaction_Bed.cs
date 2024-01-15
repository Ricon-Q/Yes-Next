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
            PlayerInputManager.Instance.SetInputMode(false);
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
                DataManager.Instance.SaveSlot();
                break;
            case 3:
                FadeInOutManager.Instance.FadeInOut(FadeInOutTime);
                _TimeManager.Instance.increaseHour(3);
                DataManager.Instance.SaveSlot();
                break;
            case 12:
                FadeInOutManager.Instance.FadeInOut(FadeInOutTime);
                _TimeManager.Instance.SetTargetTime(12);
                DataManager.Instance.SaveSlot();
                break;
            case 18:
                FadeInOutManager.Instance.FadeInOut(FadeInOutTime);
                _TimeManager.Instance.SetTargetTime(18);
                DataManager.Instance.SaveSlot();
                break;
            case 24:
                FadeInOutManager.Instance.FadeInOut(FadeInOutTime);
                _TimeManager.Instance.SetTargetTime(6);
                DataManager.Instance.SaveSlot();
                break;
            default:
                break;
        }
        PlayerInputManager.Instance.SetInputMode(true);
    }
}
