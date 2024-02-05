using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestMenuDescription : MonoBehaviour
{
    [Header("Quest Description")]
    public TextMeshProUGUI _questName;
    // public TextMeshProUGUI _deadline;
    public TextMeshProUGUI _description;
    private QuestData _questData;
    // private _TimeData _timeData;

    public QuestDesSlotDisplay _questDesSlotDisplay;

    public void AllocateQuestData(QuestData questData, _TimeData timeData)
    {
        _questData = questData;
        _questName.text = _questData._questName;
        // if(timeData)
        _description.text = _questData._questDescription;
    }
    
    public void CheckQuestItem()
    {
        if(_questDesSlotDisplay.inventorySystem.inventorySlots[0].itemId == _questData._requireItemData.ID && _questDesSlotDisplay.inventorySystem.inventorySlots[0].stackSize > _questData._stackSize )
        {
            PlayerInventoryManager.Instance.AddToInventory(_questDesSlotDisplay.inventorySystem.inventorySlots[0].itemId, _questDesSlotDisplay.inventorySystem.inventorySlots[0].stackSize - _questData._stackSize);
            

            _questDesSlotDisplay.inventorySystem.inventorySlots[0].ClearSlot();

            QuestManager.Instance.CompleteQuest(_questData._questId);

            QuestManager.Instance._questMenuQuestList.RefreshQuestList();
            _questDesSlotDisplay.RefreshDynamicInventory(_questDesSlotDisplay.inventorySystem);
            return;
        }
        else if(_questDesSlotDisplay.inventorySystem.inventorySlots[0].itemId == _questData._requireItemData.ID && _questDesSlotDisplay.inventorySystem.inventorySlots[0].stackSize == _questData._stackSize)
        {

            _questDesSlotDisplay.inventorySystem.inventorySlots[0].ClearSlot();

            QuestManager.Instance.CompleteQuest(_questData._questId);

            QuestManager.Instance._questMenuQuestList.RefreshQuestList();
            _questDesSlotDisplay.RefreshDynamicInventory(_questDesSlotDisplay.inventorySystem);
            return;
        }
        return;
    }
}
