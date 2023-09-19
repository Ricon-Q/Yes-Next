using TMPro;

// 상점 모드에서 플레이어의 인벤토리에 해당한다.
public class ShopPlayerDisplay : DynamicInventoryDisplay
{
    // 우측 Slot에 해당하는 인벤토리, 상점모드에 들어갈때 플레이어의 인벤토리를 불러온다.
    // 해당 스크립트에서는 InventoryDisplay 스크립트의 InventorySystem inventorySystem이다.

    // 중앙 하단의 InventorySystem
    public SellDisplay sellDisplay;

    // 중앙 가격 text
    public TextMeshProUGUI sellPriceText;
    // 현재 소지금 text
    public TextMeshProUGUI currentMoneyText;
    // Total 가격 text
    public TextMeshProUGUI totalPriceText;
    private PlayerManager playerManager;
    public long totalSellPrice;
    
    protected override void Start() 
    {
        playerManager = PlayerManager.Instance;
    }
    public void EnterShopMode()
    {
        inventorySystem.Save();
        totalSellPrice = 0;
        UpdatePriceText();
    }

    public void ExitShopMode()
    {
        // Reset();
        inventorySystem.Save();
        RefreshDynamicInventory(this.inventorySystem);
    }

    public void ConfirmDeal()
    {
        totalSellPrice = 0;
        inventorySystem.Save();
        UpdatePriceText();
        this.RefreshDynamicInventory(this.inventorySystem);
    }

    public override void SlotClicked(InventorySlot_UI clickedUISlot)
    {
        // Slot이 빈칸이 아니고, Slot의 아이템이 Shopable일 경우 실행
        if(clickedUISlot.AssignedInventorySlot.ItemData && clickedUISlot.AssignedInventorySlot.ItemData.Shopable)
        {
            totalSellPrice += clickedUISlot.AssignedInventorySlot.ItemData.SellPrice;
            UpdatePriceText();

            sellDisplay.InventorySystem.AddToInventory(clickedUISlot.AssignedInventorySlot.ItemData, 1);
            clickedUISlot.AssignedInventorySlot.RemoveFromStack(1);
            this.RefreshDynamicInventory(this.inventorySystem);
            sellDisplay.RefreshDynamicInventory(sellDisplay.InventorySystem);
        }   
    }

    public void UpdatePriceText()
    {
        currentMoneyText.text = playerManager.playerData.money.ToString("n0");
        sellPriceText.text = totalSellPrice.ToString("n0");
        totalPriceText.text = "+" + totalSellPrice.ToString("n0");
    }

    public void Reset()
    {
        totalSellPrice = 0;
        inventorySystem.Load();
        RefreshDynamicInventory(this.inventorySystem);
        UpdatePriceText();
    }
}
