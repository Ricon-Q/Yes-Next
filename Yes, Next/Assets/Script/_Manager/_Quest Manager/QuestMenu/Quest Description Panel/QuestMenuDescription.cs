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

    public void DeallocateQuestData()
    {
        _questData = null;
        _questName.text = "";
        _description.text = "퀘스트를 선택하여 정보 확인";

    }
    
    public void CheckQuestItem()
    {
        if(_questData==null) return; // 퀘스트 데이터가 없을 시
        if(_questDesSlotDisplay.inventorySystem.inventorySlots[0].itemId == _questData._requireItemData.ID && _questDesSlotDisplay.inventorySystem.inventorySlots[0].stackSize > _questData._stackSize )
        {
            // 아이템의 종류는 같고 수량이 더 많을시 -> 남은 수량만큼 Player의 인벤토리로
            PlayerInventoryManager.Instance.AddToInventory(_questDesSlotDisplay.inventorySystem.inventorySlots[0].itemId, _questDesSlotDisplay.inventorySystem.inventorySlots[0].stackSize - _questData._stackSize);
            

            _questDesSlotDisplay.inventorySystem.inventorySlots[0].ClearSlot();

            QuestManager.Instance.CompleteQuest(_questData._questId);

            QuestManager.Instance._questMenuQuestList.RefreshQuestList();
            _questDesSlotDisplay.RefreshDynamicInventory(_questDesSlotDisplay.inventorySystem);
            return;
        }
        else if(_questDesSlotDisplay.inventorySystem.inventorySlots[0].itemId == _questData._requireItemData.ID && _questDesSlotDisplay.inventorySystem.inventorySlots[0].stackSize == _questData._stackSize)
        {
            // 아이템 종류 같고 수량이 같을 시 -> Clear
            _questDesSlotDisplay.inventorySystem.inventorySlots[0].ClearSlot();

            QuestManager.Instance.CompleteQuest(_questData._questId);

            QuestManager.Instance._questMenuQuestList.RefreshQuestList();
            _questDesSlotDisplay.RefreshDynamicInventory(_questDesSlotDisplay.inventorySystem);
            return;
        }
        // 종류가 다르거나 수량이 적을 시
        PixelCrushers.DialogueSystem.DialogueManager.ShowAlert("올바르지 않습니다.");
        return;
    }
}
