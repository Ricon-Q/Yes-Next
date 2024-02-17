using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagnosisSlot_Ui : _InventorySlot_UI
{
    [Header("Base Potion")]
    [SerializeField] Sprite _basePotionSprite;

    public override void UpdateUISlot(_InventorySlot slot)
    {
        if(slot.itemId != -1)
        {
            itemSprite.sprite = PlayerInventoryManager.Instance.itemDataBase.Items[slot.itemId].Icon;
            itemSprite.color = Color.white;

            if(slot.stackSize > 1) itemCount.text = slot.stackSize.ToString();
            else itemCount.text = "";
            UpdateNamePrice();
        }
        else
        {
            itemSprite.sprite = _basePotionSprite;
            itemSprite.color = Color.white;
            itemCount.text = "";
        }
    }

    override public void ClearSlot()
    {
        assignedInventorySlot?.ClearSlot();
        itemSprite.sprite = _basePotionSprite;
        itemSprite.color = Color.white;
        itemCount.text = "";
    }
}
