using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Quest Data", menuName = "Quest System/Quest Data")]
public class QuestData : ScriptableObject
{
    [Header("Quest Info")]
    public string _questName;
    public int _questId;
    [TextArea(10, 5)]
    public string _questDescription;
    public int _deadline;

    [Header("Require Item Info")]
    public InventoryItemData _requireItemData;
    public int _stackSize;

    [Header("Reward Item Info")]
    public int _rewardMoney;
    // public InventoryItemData _rewardItemData;
    // public int _rewardItemCount;
    public int _rewardExp;
}
