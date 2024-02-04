using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildQuestDisplay : MonoBehaviour
{
    public List<int> _questDatas;
    public QuestDescription _questDescription;
    public bool _isQeustRefresh;
    public bool _haveLoadData;

    private void Start() 
    {
        _questDatas = new List<int>();
        ExitGuildQuestList();
    }

    public void RefreshGuildQuestList()
    {
        _questDatas = new List<int>(QuestManager.Instance.GetRandomQuests(5));
    }

    public void OpenQuestDescription(int index)
    {
        _questDescription.AllocateQuestData(_questDatas[index]);
    }

    public void EnterGuildQuestList()
    {
        this.gameObject.SetActive(true);

        // 게임 불러오기를 했다면 저장했던 길드 퀘스트 불러오기
        if(_haveLoadData)
        {
            DataManager.Instance.LoadGuildQuestData(DataManager.Instance.currentSaveIndex);
            _haveLoadData = false;
            return;
        }
        // Gamemanager에서 isQeustRefresh true로 설정했다면 새로고침
        if(_isQeustRefresh)
        {   
            RefreshGuildQuestList();
            _isQeustRefresh = false;
            return;
        }
        if(_questDatas.Count == 0) 
            RefreshGuildQuestList();
    }

    public void ExitGuildQuestList()
    {
        this.gameObject.SetActive(false);
    }
}
