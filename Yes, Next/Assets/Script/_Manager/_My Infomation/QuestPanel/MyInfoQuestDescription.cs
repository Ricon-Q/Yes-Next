using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyInfoQuestDescription : MonoBehaviour
{   
    [Header("Qeust Text")]
    [SerializeField] private TextMeshProUGUI _questNameText;
    [SerializeField] private TextMeshProUGUI _dDayText;
    [SerializeField] private TextMeshProUGUI _questDescriptionText;
    
    [Header("Quest Item")]
    [SerializeField] private Image _itemImg;
    [SerializeField] private TextMeshProUGUI _itemCount;
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _rewardMoney;

    private QuestData _questData;
    [Header("MyInfomationQuestList")]
    [SerializeField] private MyInfomationQuestList _myInfomationQuestList;
    public void AllocateQuestData(QuestData questData)
    {
        _questData = questData;
        _questNameText.text = questData._questName;
        int day = -CalculateDeadline(questData);
        _dDayText.text = day <= 0 ? "D-Day" : "D-" + day.ToString();
        _questDescriptionText.text = questData._questDescription;
        AllocateQuestItem(questData);
    }

    public void AllocateQuestItem(QuestData questData)
    {
        _itemImg.color = Color.white;
        _itemImg.sprite = questData._requireItemData.Icon;
        _itemCount.text = questData._stackSize.ToString("n0");
        _itemName.text = questData._requireItemData.DisplayName;
        _rewardMoney.text = questData._rewardMoney.ToString("n0");
    }

    public int CalculateDeadline(QuestData questData)
    {
        _TimeData questAcceptDay = new _TimeData(QuestManager.Instance._playerQuestDictionary[questData._questId]);
        questAcceptDay.increaseDay(questData._deadline);

        int calculatedDeadline = _TimeManager.Instance.DaysSince(questAcceptDay);
        // Debug.Log(calculatedDeadline);
        return calculatedDeadline;
    }
    public void DeallocateQuestData()
    {
        _questData = null;
        _questNameText.text = "";
        _dDayText.text = "";
        _questDescriptionText.text = "퀘스트를 선택해서 퀘스트 내용 확인";
        DeallocateQuestItem();
    }
    public void DeallocateQuestItem()
    {
        _itemImg.color = Color.clear;
        _itemImg.sprite = null;
        _itemCount.text = "";
        _itemName.text = "";
        _rewardMoney.text = "";
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
