using UnityEngine;

public class PotionStand : _DynamicInventoryDisplay
{
    [SerializeField] private GameObject _onButton;
    [SerializeField] private GameObject _offButton;
    public void EnterCraftMode()
    {
        _CraftManager.Instance.object_PotionStand.SetActive(true);
        this.inventorySystem = PlayerInventoryManager.Instance.potionInventory;    
        RefreshDynamicInventory(this.inventorySystem);
        _onButton.SetActive(false);
        _offButton.SetActive(true);
    }
    public void ExitToolMode()
    {
        _CraftManager.Instance.object_PotionStand.SetActive(false);
        _offButton.SetActive(false);
        _onButton.SetActive(true);
    }
}
