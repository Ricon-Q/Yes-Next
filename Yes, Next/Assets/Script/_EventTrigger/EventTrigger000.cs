using UnityEngine;

public class EventTrigger000 : MonoBehaviour
{
    public PixelCrushers.DialogueSystem.DialogueSystemTrigger _dialogueSystemTrigger;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(EventManager.Instance._eventDictionary[0]._isEventTriggered)
        {
            EventManager.Instance._eventDictionary[0].TriggerOff();
            _dialogueSystemTrigger.OnUse();
        }
    }

    public void PayInnFee()
    {
        _PlayerManager.Instance.playerData.money -= 400;
    }
}
