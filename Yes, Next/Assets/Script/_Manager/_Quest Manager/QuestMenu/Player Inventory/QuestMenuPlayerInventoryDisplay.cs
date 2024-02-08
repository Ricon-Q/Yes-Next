using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestMenuPlayerInventoryDisplay : _DynamicInventoryDisplay
{
    [Header("Button")]
    [SerializeField] private Button _mainInventoryButton;
    [SerializeField] private Button _herbInventoryButton;
    [SerializeField] private Button _potionInventoryButton;
    protected override void Start() 
    {
        // inventorySystem = PlayerInventoryManager.Instance.playerInventory;
    }
    public void EnterQuestMenu()
    {
        ChangeInventory(0);
    }

    public void ExitQuestMenu()
    {
        
    }

    public void ChangeInventory(int index)
    {
        EnableButton();
        switch (index)
        {
            case 0:
                inventorySystem = PlayerInventoryManager.Instance.playerInventory;
                RefreshDynamicInventory(inventorySystem);
                _mainInventoryButton.interactable = false;
                break;
            case 1:
                inventorySystem = PlayerInventoryManager.Instance.herbInventory;
                RefreshDynamicInventory(inventorySystem);
                _herbInventoryButton.interactable = false;
                break;
            case 2:
                inventorySystem = PlayerInventoryManager.Instance.potionInventory;
                RefreshDynamicInventory(inventorySystem);
                _potionInventoryButton.interactable = false;
                break;
        }
    }

    private void EnableButton()
    {
        _mainInventoryButton.interactable = true;
        _herbInventoryButton.interactable = true;
        _potionInventoryButton.interactable = true;
    }

    // override public void CreateInventorySlot()
    // {
    //     slotDictionary = new Dictionary<_InventorySlot_UI, _InventorySlot>();

    //     if(inventorySystem == null) return;

    //     for (int i = 0; i < 12 * inventorySystem.inventoryLevel; i++)
    //     {
    //         var uiSlot = Instantiate(slotPrefab, transform);
    //         slotDictionary.Add(uiSlot, inventorySystem.inventorySlots[i]);
    //         uiSlot.Init(inventorySystem.inventorySlots[i]);
    //         uiSlot.UpdateUISlot();
    //     }
    // }
}
