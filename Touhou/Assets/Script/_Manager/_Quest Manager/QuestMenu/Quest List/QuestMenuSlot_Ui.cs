using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QuestMenuSlot_Ui : MonoBehaviour
{
    public QuestData _questData;
    public _TimeData _timeData;
    public void EnableButton()
    {
        this.gameObject.SetActive(true);
    }

    public void DisableButton()
    {
        this.gameObject.SetActive(false);
    }

    [Header("Quest Description")]
    public QuestMenuDescription _questMenuDescription;


    public void AllocateQuestData()
    {
        _questMenuDescription.AllocateQuestData(_questData, _timeData);
    }
}
