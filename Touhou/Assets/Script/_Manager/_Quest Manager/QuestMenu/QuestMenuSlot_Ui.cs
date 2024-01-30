using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMenuSlot_Ui : MonoBehaviour
{
    public QuestData _questData;

    public void EnableButton()
    {
        this.gameObject.SetActive(true);
    }

    public void DisableButton()
    {
        this.gameObject.SetActive(false);
    }
}
