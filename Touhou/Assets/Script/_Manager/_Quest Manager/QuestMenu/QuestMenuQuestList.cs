using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMenuQuestList : MonoBehaviour
{
    public List<QuestMenuSlot_Ui> _questButtons;

    public void EnterQuestMenu()
    {
        RefreshQuestList();
    }

    public void RefreshQuestList()
    {
        for (int i = 0; i < QuestManager.Instance._playerQuestDictionary.Count; i++)
        {
            _questButtons[i].EnableButton();
        }
        
        for (int i = 0; i < 5 - QuestManager.Instance._playerQuestDictionary.Count; i++)
        {
            _questButtons[i].DisableButton();
        }
    }
}
