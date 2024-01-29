using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest DataBase", menuName = "Quest System/Quest DataBase")]
public class QuestDataBase : ScriptableObject
{
    public List<QuestData> _questDatas;

    public QuestData FindQuestData(int _findQuestId)
    {
        foreach (var _questData in _questDatas)
        {
            if(_questData._questId == _findQuestId) return _questData;
        }

        return null;
    }

    [ContextMenu("Update QuestData")]
    public void UpdateQuestData()
    {
        for (int i = 0; i < _questDatas.Count; i++)
        {
            if (_questDatas[i] != null)
                _questDatas[i]._questId = i;
        }
    }
}
