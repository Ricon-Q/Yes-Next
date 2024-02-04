using TMPro;
using UnityEngine;

public class QuestDescription : MonoBehaviour
{
    [Header("Quest Info")]
    public TextMeshProUGUI _questName;
    public TextMeshProUGUI _questDescription;
    public TextMeshProUGUI _questDeadline;
    
    [Header("Reward")]
    public InventoryItemData _rewardItemData;
    public GameObject _rewardItemDataObject;
    public TextMeshProUGUI _rewardItemDataText;
    public TextMeshProUGUI _rewardExp;

    private QuestData _questData;

    private void Start() 
    {
        this.gameObject.SetActive(false);    
    }

    public void AllocateQuestData(int questId)
    {
        this.gameObject.SetActive(true);
        _questData = QuestManager.Instance._questDataBase.FindQuestData(questId);
        
        _questName.text = _questData._questName;
        _questDescription.text = _questData._questDescription;

        if(_questData._rewardItemData != null)
        {
            _rewardItemDataObject.SetActive(true);
            _rewardItemData = _questData._rewardItemData;
            _rewardItemDataText.text = _questData._rewardItemData.DisplayName + " X " + _questData._rewardItemCount.ToString();
        }
        else
        {
            _rewardItemDataObject.SetActive(false);
        }
        _rewardExp.text = "Exp : " + _questData._rewardExp.ToString();
    }

    public void AcceptQuest()
    {
        if(!QuestManager.Instance._playerQuestDictionary.ContainsKey(_questData._questId))
        {
            QuestManager.Instance.AddQuestToList(_questData._questId);
        }
        else PixelCrushers.DialogueSystem.DialogueManager.ShowAlert("이미 추가된 퀘스트입니다.");
    }
}
