using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionPanel : _DynamicInventoryDisplay
{
    [Header("Potion Detail")]
    [SerializeField] private Image _potionImage;
    [SerializeField] private TextMeshProUGUI _potionName;
    [SerializeField] private TextMeshProUGUI _potionTag;

    protected override void Start()
    {
        this.inventorySystem = PlayerInventoryManager.Instance.potionInventory;
        RefreshDynamicInventory(inventorySystem);
    }
    public override void SlotClicked(_InventorySlot_UI clickedUISlot)
    {
        Debug.Log("Clicked");
        _potionImage.sprite = PlayerInventoryManager.Instance.itemDataBase.Items[clickedUISlot.AssignedInventorySlot.itemId].Icon;
        _potionName.text = PlayerInventoryManager.Instance.itemDataBase.Items[clickedUISlot.AssignedInventorySlot.itemId].DisplayName;
        _potionTag.text = PlayerInventoryManager.Instance.itemDataBase.Items[clickedUISlot.AssignedInventorySlot.itemId].tag;
    }
}
