using UnityEngine;
using TMPro;

public class MortarAndPestle : _DynamicInventoryDisplay
{
    [Header("Mortar and Pestle")]
    [SerializeField] private RecipeDatabase recipeDatabase;
    [SerializeField] private GameObject visualStaminaTime;
    [SerializeField] private TextMeshProUGUI visualStaminaTimeText;

    // Input칸 1개와 Output칸 1개 필요, Output칸에는 아이템을 넣을 수 없음
    // InventorySystem[0]은 Main herb
    // InventorySystem[1]은 Result;

    public void EnterCraftMode()
    {
        OffVisualStaminaTime();
        _CraftManager.Instance.object_MortarAndPestle.SetActive(true);
        _CraftManager.Instance.herbPocket.EnterCraftMode();
    }

    public void ExitToolMode()
    {
        // [0], [1] 슬롯에 아이템이 남아 있다면 아이템들을 플레이어 인벤토리로 옮긴 이후에 종료
        foreach (var item in inventorySystem.inventorySlots)
        {
            if(item.itemId != -1)
            {
                PlayerInventoryManager.Instance.playerInventory.AddToInventory(item.itemId, item.stackSize);
                item.ClearSlot();
            }
        }
        RefreshDynamicInventory(this.inventorySystem);

        _CraftManager.Instance.object_MortarAndPestle.SetActive(false);
        _CraftManager.Instance.herbPocket.ExitToolMode();
        _CraftManager.Instance.craftToolCanvas.SetActive(false);
    }

    public void OnVisualStaminaTime()
    {
        RecipeData foundedRecipe = CheckRecipe();
        if(foundedRecipe != null)
        {   
            visualStaminaTime.SetActive(true);
            visualStaminaTimeText.text = string.Format("Stamina : {0}\nTime : {1}", foundedRecipe.useStamina, foundedRecipe.useTimeMinute);
            return;
        }
        else
        {   
            visualStaminaTime.SetActive(true);
            visualStaminaTimeText.text = "Stamina : ??\nTime : ??";
            return;
        }
    }
    public void OffVisualStaminaTime()
    {
        visualStaminaTime.SetActive(false);
    }
    
    public void Craft()
    {
        // [1] 슬롯이 할당되어있을때 - [1]번 슬롯 아이템을 인벤토리로 이동
        if(inventorySystem.inventorySlots[1].itemId != -1)
        {
            PlayerInventoryManager.Instance.playerInventory.AddToInventory(inventorySystem.inventorySlots[1].itemId, inventorySystem.inventorySlots[1].stackSize);
            inventorySystem.inventorySlots[1].ClearSlot();
            _CraftManager.Instance.herbPocket.RefreshDynamicInventory(_CraftManager.Instance.herbPocket.inventorySystem);
            RefreshDynamicInventory(inventorySystem);
        }

        RecipeData foundedRecipe = CheckRecipe();

        if(foundedRecipe != null)
        {
            SuccessCraft(foundedRecipe);
            return;
        }
        
        else FailCraft();
    }

    public RecipeData CheckRecipe()
    {
        foreach (var item in recipeDatabase.recipeData)
        {
            if(
                inventorySystem.inventorySlots[0].itemId == item.inputItemDatas[0].ID &&
                inventorySystem.inventorySlots[0].stackSize >= item.requireInputStackSize[0]
                )
            
            {
                return item;
            }
        }
        return null;
    }

    public void SuccessCraft(RecipeData recipe)
    {
        Debug.Log("Success");
        inventorySystem.inventorySlots[1] = new _InventorySlot(recipe.outputItemData.ID, recipe.outputItemStackSize);
                
        inventorySystem.inventorySlots[0].RemoveFromStack(recipe.requireInputStackSize[0]);

        RefreshDynamicInventory(inventorySystem);

        _PlayerManager.Instance.playerData.currentStamina -= recipe.useStamina;
        _TimeManager.Instance.increaseMinute(recipe.useTimeMinute);
    }

    public void FailCraft()
    {
        // [0]슬롯이 비어져 있다면 - 시간, 피로도 소모 X, 
        if(inventorySystem.inventorySlots[0].itemId == -1) return;

        // [0]슬롯이 비어져 있지 않다면 - 시간, 피로도 소모 O, 기존 약초 삭제
        if(inventorySystem.inventorySlots[0].itemId != -1)
        {
            _PlayerManager.Instance.playerData.currentStamina -= 5;
            _TimeManager.Instance.increaseMinute(5);

            inventorySystem.inventorySlots[0].ClearSlot();
            RefreshDynamicInventory(this.inventorySystem);

            return;
        }
    }
}
