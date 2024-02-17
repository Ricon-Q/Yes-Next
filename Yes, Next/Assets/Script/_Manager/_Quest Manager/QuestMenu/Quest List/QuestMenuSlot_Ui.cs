using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QuestMenuSlot_Ui : MonoBehaviour
{
    public QuestData _questData;
    public _TimeData _timeData;
    [SerializeField] private TextMeshProUGUI _questName;
    public void EnableButton()
    {
        this.gameObject.SetActive(true);
        _questName.text = _questData._questName;
    }

    public void DisableButton()
    {
        this.gameObject.SetActive(false);
    }

    [Header("Quest Description")]
    public QuestMenuDescription _questMenuDescription;
    [SerializeField] private MyInfoQuestDescription _myInfoQuestDescription;


    public void AllocateQuestData()
    {
        _questMenuDescription.AllocateQuestData(_questData, _timeData);
    }

    public void MyInfoAllocate()
    {
        _myInfoQuestDescription.AllocateQuestData(_questData);
    }
}
