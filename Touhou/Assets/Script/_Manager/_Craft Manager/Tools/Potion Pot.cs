using UnityEngine;
using TMPro;

// 포션, 약초 카테고리 모두 이용 가능
// 1~3개의 재료를 이용해서 포션으로 제작 가능
// 순서 상관 O

// Input칸 3개와 Output칸 1개 필요, Output칸에는 아이템을 넣을 수 없음
// Inventory System[0~2] Ingredient
// Inventory System[3] Result

public class PotionPot : _DynamicInventoryDisplay
{
    [Header("Potion Pot")]
    [SerializeField] private RecipeDatabase recipeDatabase;
    [SerializeField] private GameObject visualStaminaTime;
    [SerializeField] private TextMeshProUGUI visualStaminaTimeText;

    public void EnterCraftMode()
    {
        OffVisualStaminaTime();
        _CraftManager.Instance.object_PotionPot.SetActive(true);
        _CraftManager.Instance.herbPocket.EnterCraftMode();
        // _CraftManager.Instance.potionStand.EnterCraftMode();
    }
    public void ExitToolMode()
    {
        // [0~3] 슬롯에 아이템이 남아있다면 아이템들을 플레이어 인벤토리로 옮긴 후 종료
        foreach (var item in inventorySystem.inventorySlots)
        {
            if(item.itemId != -1)
            {
                // PlayerInventoryManager.Instance.playerInventory.AddToInventory(item.itemId, item.stackSize);
                // item.ClearSlot();
                switch(PlayerInventoryManager.Instance.itemDataBase.Items[item.itemId].ItemType)
                {
                    case ItemType.Herb:
                        PlayerInventoryManager.Instance.herbInventory.AddToInventory(item.itemId, item.stackSize);
                        break;
                    case ItemType.Seed:
                        PlayerInventoryManager.Instance.herbInventory.AddToInventory(item.itemId, item.stackSize);
                        break;
                    case ItemType.Potion:
                        PlayerInventoryManager.Instance.potionInventory.AddToInventory(item.itemId, item.stackSize);
                        break;
                    default:
                        PlayerInventoryManager.Instance.playerInventory.AddToInventory(item.itemId, item.stackSize);            
                        break;
                }
                item.ClearSlot();
            }
        }

        // 슬롯 칸 새로고침
        RefreshDynamicInventory(this.inventorySystem);

        _CraftManager.Instance.object_PotionPot.SetActive(false);
        _CraftManager.Instance.herbPocket.ExitToolMode();
        // _CraftManager.Instance.potionStand.ExitToolMode();
        _CraftManager.Instance.craftToolCanvas.SetActive(false);
    }

    public void Craft()
    {
        // [3] 슬롯이 할당되어있을때 - [3] 슬롯 아이템을 인벤토리로 이동
        if(inventorySystem.inventorySlots[3].itemId != -1)
        {
            PlayerInventoryManager.Instance.potionInventory.AddToInventory(inventorySystem.inventorySlots[3].itemId, inventorySystem.inventorySlots[3].stackSize);   
            inventorySystem.inventorySlots[3].ClearSlot();
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
                inventorySystem.inventorySlots[0].stackSize >= item.requireInputStackSize[0] &&
                inventorySystem.inventorySlots[1].itemId == item.inputItemDatas[1].ID &&
                inventorySystem.inventorySlots[1].stackSize >= item.requireInputStackSize[1] &&
                inventorySystem.inventorySlots[2].itemId == item.inputItemDatas[2].ID &&
                inventorySystem.inventorySlots[2].stackSize >= item.requireInputStackSize[2]                
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
        inventorySystem.inventorySlots[3] = new _InventorySlot(recipe.outputItemData.ID, recipe.outputItemStackSize);

        for(int i = 0; i < 3; i++)
            if(inventorySystem.inventorySlots[i].itemId != -1)
                inventorySystem.inventorySlots[i].RemoveFromStack(recipe.requireInputStackSize[i]);

        RefreshDynamicInventory(inventorySystem);

        _PlayerManager.Instance.playerData.ModifyCurrentStamina(-recipe.useStamina);
        _TimeManager.Instance.increaseMinute(recipe.useTimeMinute);
    }

    public void FailCraft()
    {
        // [0~2] 슬롯이 모두 비어져있다면 - 시간, 피로도 소모 X
        // [0~2] 슬롯중 하나라고 채워져있다면 - 시간, 피로도 소모 O, 기존 재료 삭제
        for(int i = 0; i < 3; i++)
        {
            if(inventorySystem.inventorySlots[i].itemId != -1)
            {
                _PlayerManager.Instance.playerData.currentStamina -= 5;
                _TimeManager.Instance.increaseMinute(5);

                inventorySystem.inventorySlots[0].ClearSlot();
                inventorySystem.inventorySlots[1].ClearSlot();
                inventorySystem.inventorySlots[2].ClearSlot();
                
                RefreshDynamicInventory(this.inventorySystem);

                return;
            }
        }

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
}
