using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private static QuestManager instance;

    public static QuestManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // ========================================= //

    [Header("Quest Database")]
    public QuestDataBase _questDataBase;

    // [Header("Guild Quest List")]
    // public List<QuestData> _guildQuestList;

    [Header("Player Quest Rank")]
    public int _playerQuestRank = 0;
    public int _playerQuestExp = 0;

    [Header("Player Quest List")]
    public Dictionary<int, _TimeData> _playerQuestDictionary = new Dictionary<int, _TimeData>();

    [Header("Guild Quest Display")]
    public GuildQuestDisplay _guildQuestDisplay;

    private void Start() 
    {
        _guildQuestDisplay.ExitGuildQuestList();
        _questDataBase.UpdateQuestData();

        _questMenuObject.SetActive(false);
    }

    public void EnterGuildQuestList()
    {
        _guildQuestDisplay.EnterGuildQuestList();
        MyInfomation.Instance.ExitMyInfomation();
        PlayerInputManager.SetPlayerInput(false);
    }

    public void AddQuestToList(int _questIndex)
    {
        // 퀘스트 수락시 플레이어의 퀘스트 리스트에 추가
        // 빈 자리가 있다면 퀘스트 추가, 없다면 퀘스트 수락 불가

        // Dictionary의 크기가 5 이하라면 퀘스트 추가
        if(_playerQuestDictionary.Count <= 5)
        {
            _TimeData tmp = new _TimeData(_TimeManager.Instance.timeData);
            _playerQuestDictionary.Add(_questIndex, tmp);
            return;
        }   
        // Dictionary의 크기가 5 초과라면 퀘스트 추가 불가
        else Debug.Log("퀘스트 추가 불가");
    }

    public void RemoveQuest(int _questIndex)
    {
        // 퀘스트 삭제(플레이어가 직접 삭제하거나, 마감일이 지나면 해당 함수 호출) 
        // 딕셔너리가 키값을 갖고있지 않으면 리턴
        if(!_playerQuestDictionary.ContainsKey(_questIndex)) return;
        else
        {
            _playerQuestDictionary.Remove(_questIndex);
        }
    }

    public void CheckDeadline()
    {
        // 하루가 지날때마다 퀘스트 마감일 확인
        List<int> questToRemove = new List<int>();
        foreach (var item in _playerQuestDictionary)
        {
            _TimeData questAcceptDay = new _TimeData(item.Value);
            questAcceptDay.increaseDay(_questDataBase.FindQuestData(item.Key)._deadline);
            int calculatedDeadline = -(_TimeManager.Instance.DaysSince(questAcceptDay));
            if(calculatedDeadline >= 0)
                continue;
            else
                questToRemove.Add(item.Key);
        }
        if(questToRemove.Count == 0) return;

        foreach (var item in questToRemove)
        {
            string alert = $"\"{_questDataBase.FindQuestData(item)._questName}\" 퀘스트의 마감일이 지나 삭제되었습니다.";
            PixelCrushers.DialogueSystem.DialogueManager.ShowAlert(alert);
        
            RemoveQuest(item);
        }
    }

    public void CompleteQuest(int _questIndex)
    {
        // 퀘스트 성공 함수
        QuestData _questData = _questDataBase.FindQuestData(_questIndex);
        _PlayerManager.Instance.playerData.AddMoney(_questData._rewardMoney);
        _playerQuestExp += _questData._rewardExp;
        // 퀘스트 랭크 증가 확인

        //퀘스트 보상 아이템 존재시 인벤토리에 추가
        // if(_questData._rewardItemData != null)
        // {
        //     switch (_questData._rewardItemData.ItemType)
        //     {
        //         case ItemType.Herb:
        //             PlayerInventoryManager.Instance.herbInventory.AddToInventory(_questData._rewardItemData.ID, _questData._rewardItemCount);
        //             break;
        //         case ItemType.Potion:
        //             PlayerInventoryManager.Instance.potionInventory.AddToInventory(_questData._rewardItemData.ID, _questData._rewardItemCount);
        //             break;
        //         case ItemType.Default:
        //             PlayerInventoryManager.Instance.playerInventory.AddToInventory(_questData._rewardItemData.ID, _questData._rewardItemCount);
        //             break;
        //     }
        // }
        RemoveQuest(_questIndex);
    }

    public void GiveUpQuest(QuestData questData)
    {
        _playerQuestDictionary.Remove(questData._questId);
    }

    public List<int> GetRandomQuests(int count)
    {
        List<int> randomQuests = new List<int>();

        // 요청된 퀘스트 수가 실제 퀘스트 수보다 크면 전체 목록 반환
        if (count >= _questDataBase._questDatas.Count)
        {
            return new List<int>(_questDataBase.ReturnAsInt());
        }

        List<QuestData> copyOfQuests = new List<QuestData>(_questDataBase._questDatas);

        int i = 0;
        while(i < count)
        {
            if (copyOfQuests.Count == 0)
            {
                break;  // 사용 가능한 퀘스트가 더 이상 없음
            }

            int randomIndex = Random.Range(0, copyOfQuests.Count);

            if(_playerQuestDictionary.ContainsKey(randomIndex)) continue;
            if(randomQuests.Contains(randomIndex)) continue;
            randomQuests.Add(randomIndex);
            copyOfQuests.RemoveAt(randomIndex);
            i++;
        }

        return randomQuests;
    }


    // ================================================ //
    // ================================================ //
    
    [Header("Quest Menu - Inventory Display")]
    public QuestMenuPlayerInventoryDisplay _inventoryDisplay;
    public QuestMenuQuestList _questMenuQuestList;
    public GameObject _questMenuObject;
    [SerializeField] QuestMenuDescription _questMenuDescription;

    public void EnterQuestMenu()
    {
        MyInfomation.Instance.ExitMyInfomation();
        PlayerInputManager.SetPlayerInput(false);

        _questMenuObject.SetActive(true);
        _inventoryDisplay.EnterQuestMenu();
        _questMenuQuestList.EnterQuestMenu();
        _questMenuDescription.DeallocateQuestData();
    }

    public void ExitQuestMenu()
    {
        _inventoryDisplay.ExitQuestMenu();
        _questMenuObject.SetActive(false);

        PlayerInputManager.SetPlayerInput(true);
    }

    public void ChangeInventory(int index)
    {
        switch (index)
        {
            case 0:
                _inventoryDisplay.inventorySystem = PlayerInventoryManager.Instance.playerInventory;
                _inventoryDisplay.RefreshDynamicInventory(_inventoryDisplay.inventorySystem);
                break;
            case 1:
                _inventoryDisplay.inventorySystem = PlayerInventoryManager.Instance.herbInventory;
                _inventoryDisplay.RefreshDynamicInventory(_inventoryDisplay.inventorySystem);
                break;
            case 2:
                _inventoryDisplay.inventorySystem = PlayerInventoryManager.Instance.potionInventory;
                _inventoryDisplay.RefreshDynamicInventory(_inventoryDisplay.inventorySystem);
                break;
            default:
                break;
        }
    }
}
