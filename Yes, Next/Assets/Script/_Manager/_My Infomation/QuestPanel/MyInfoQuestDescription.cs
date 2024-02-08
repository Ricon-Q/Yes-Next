using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MyInfoQuestDescription : MonoBehaviour
{   
    [Header("Qeust Text")]
    [SerializeField] private TextMeshProUGUI _questNameText;
    [SerializeField] private TextMeshProUGUI _dDayText;
    [SerializeField] private TextMeshProUGUI _questDescriptionText;
    private QuestData _questData;
    [Header("MyInfomationQuestList")]
    [SerializeField] private MyInfomationQuestList _myInfomationQuestList;
    public void AllocateQuestData(QuestData questData)
    {
        _questData = questData;
        _questNameText.text = questData._questName;
        _dDayText.text = "";
        _questDescriptionText.text = questData._questDescription;
    }
    public void DeallocateQuestData()
    {
        _questData = null;
        _questNameText.text = "";
        _dDayText.text = "";
        _questDescriptionText.text = "퀘스트를 선택해서 퀘스트 내용 확인";
    }

    public void GiveUpQuest()
    {   
        if(_questData)
        {
            QuestManager.Instance.GiveUpQuest(_questData);
            _myInfomationQuestList.RefreshQuestList();
            DeallocateQuestData();
        }
    }
}
