using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GuildQuestDisplay : MonoBehaviour
{
    public List<int> _questDatas;
    public QuestDescription _questDescription;
    public bool _isQeustRefresh;
    public bool _haveLoadData;

    [Header("Quest Buttons")]
    [SerializeField] private List<GameObject> _buttonsObjects;

    private void Start() 
    {
        _questDatas = new List<int>();
        ExitGuildQuestList();
    }

    public void RefreshGuildQuestList()
    {
        _questDatas = new List<int>(QuestManager.Instance.GetRandomQuests(5));
    }

    public void DisableQuest(int questId)
    {
        for (int i = 0; i < _questDatas.Count; i++)
        {
            if(questId == _questDatas[i]) 
                _questDatas[i] = -1;
        }
    }

    public void RefreshGuildQuestDisplay()
    {
        for (int i = 0; i < _questDatas.Count; i++)
        {
            if(_questDatas[i] == -1)
                _buttonsObjects[i].SetActive(false);
            else
                _buttonsObjects[i].SetActive(true);
        }
    }

    public void OpenQuestDescription(int index)
    {
        _questDescription.AllocateQuestData(_questDatas[index]);
    }

    public void EnterGuildQuestList()
    {
        this.gameObject.SetActive(true);
        // RefreshGuildQuestDisplay();

        // Gamemanager에서 isQeustRefresh true로 설정했다면 새로고침
        if(_isQeustRefresh)
        {   
            RefreshGuildQuestList();
            _haveLoadData = false;
            _isQeustRefresh = false;
            RefreshGuildQuestDisplay();
            return;
        }

        // 게임 불러오기를 했다면 저장했던 길드 퀘스트 불러오기
        if(_haveLoadData)
        {
            if(DataManager.Instance.LoadGuildQuestData(DataManager.Instance.currentSaveIndex))
            {
                _haveLoadData = false;
                _isQeustRefresh = false;
                RefreshGuildQuestDisplay();
                return;
            }
            else
            {
                RefreshGuildQuestList();
                _haveLoadData = false;
                _isQeustRefresh = false;
                RefreshGuildQuestDisplay();
                return;
            }
        }
        if(_questDatas.Count == 0) 
            RefreshGuildQuestList();
        RefreshGuildQuestDisplay();

        // RefreshGuildQuestDisplay();
    }

    public void ExitGuildQuestList()
    {
        this.gameObject.SetActive(false);
        PlayerInputManager.SetPlayerInput(true);
    }
}
