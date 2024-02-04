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
        int index = 0;
        for (int i = 0; i < 5; i++)
        {
            if(i < QuestManager.Instance._playerQuestDictionary.Count)
                _questButtons[i].EnableButton();
            else
                _questButtons[i].DisableButton();
        }

        foreach (var item in QuestManager.Instance._playerQuestDictionary)
        {
            _questButtons[index]._questData = QuestManager.Instance._questDataBase.FindQuestData(item.Key);
            _questButtons[index]._timeData = item.Value;
            index++;
        }
    }
}
