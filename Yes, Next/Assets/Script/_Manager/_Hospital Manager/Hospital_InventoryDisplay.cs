using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hospital_InventoryDisplay : _DynamicInventoryDisplay
{
    [Header("Inventory Button")]
    [SerializeField] private Button _mainInvButton;
    [SerializeField] private Button _herbInvButton;
    [SerializeField] private Button _potionInvButton;

    protected override void Start()
    {
        this.inventorySystem = PlayerInventoryManager.Instance.playerInventory;
    }
    public void ChangeInventory(int index)
    {
        EnableAllButton();
        switch(index)
        {
             case 0:
                inventorySystem = PlayerInventoryManager.Instance.playerInventory;
                RefreshDynamicInventory(inventorySystem);
                _mainInvButton.interactable = false;
                break;
            case 1:
                inventorySystem = PlayerInventoryManager.Instance.herbInventory;
                RefreshDynamicInventory(inventorySystem);
                _herbInvButton.interactable = false;
                break;
            case 2:
                inventorySystem =  PlayerInventoryManager.Instance.potionInventory;
                RefreshDynamicInventory(inventorySystem);
                _potionInvButton.interactable = false;
                break;
            default:
                break;
        }
    }

    private void EnableAllButton()
    {
        _mainInvButton.interactable = true;
        _herbInvButton.interactable = true;
        _potionInvButton.interactable = true;
    }
}
