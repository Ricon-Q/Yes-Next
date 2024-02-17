using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMenuQuestList : MonoBehaviour
{
    public List<QuestMenuSlot_Ui> _questButtons;
    [SerializeField] private GameObject _noQuestText;

    public void EnterQuestMenu()
    {
        RefreshQuestList();
    }

    public void RefreshQuestList()
    {
        // int index = 0;
        // for (int i = 0; i < 5; i++)
        // {
        //     if(i < QuestManager.Instance._playerQuestDictionary.Count)
        //         _questButtons[i].EnableButton();
        //     else
        //         _questButtons[i].DisableButton();
        // }

        // foreach (var item in QuestManager.Instance._playerQuestDictionary)
        // {
        //     _questButtons[index]._questData = QuestManager.Instance._questDataBase.FindQuestData(item.Key);
        //     _questButtons[index]._timeData = item.Value;
        //     index++;
        // }
        int index = 0;

        foreach (var item in QuestManager.Instance._playerQuestDictionary)
        {
            _questButtons[index]._questData = QuestManager.Instance._questDataBase.FindQuestData(item.Key);
            _questButtons[index].EnableButton();
            _questButtons[index]._timeData = item.Value;
            index++;
        }
        for(int i = index; i < 5; i++)
        {
            _questButtons[i].DisableButton();
        }
        if(index == 0)
            _noQuestText.SetActive(true);
        else
            _noQuestText.SetActive(false);
    }
}
