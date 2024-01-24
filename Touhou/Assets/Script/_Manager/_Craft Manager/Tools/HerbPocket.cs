public class HerbPocket : _DynamicInventoryDisplay
{
    public void EnterCraftMode() 
    {
        _CraftManager.Instance.object_HerbPocket.SetActive(true);
        this.inventorySystem = PlayerInventoryManager.Instance.herbInventory;    
        RefreshDynamicInventory(this.inventorySystem);
    }
    public void ExitToolMode()
    {
        _CraftManager.Instance.object_HerbPocket.SetActive(false);
    }
}
