using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildQuestDisplay : MonoBehaviour
{
    public List<QuestData> _questDatas;
    public QuestDescription _questDescription;

    private void Start() 
    {
        ExitGuildQuestList();  
    }

    public void RefreshGuildQuestList()
    {
        _questDatas = new List<QuestData>(QuestManager.Instance.GetRandomQuests(5));
    }

    public void OpenQuestDescription(int index)
    {
        _questDescription.AllocateQuestData(_questDatas[index]);
    }

    public void EnterGuildQuestList()
    {
        this.gameObject.SetActive(true);
        // 현재는 길드 퀘스트가 활성화 될 때마다 길드 퀘스트 리스트 새로고침.
        // 추후 주기적으로 길드 퀘스트 새로고침 되도록 수정
        RefreshGuildQuestList();
    }

    public void ExitGuildQuestList()
    {
        this.gameObject.SetActive(false);
    }
}
