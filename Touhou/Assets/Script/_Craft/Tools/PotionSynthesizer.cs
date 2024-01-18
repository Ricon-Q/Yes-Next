using UnityEngine;

public class PotionSynthesizer : _DynamicInventoryDisplay
{
    // 두 포션을 합쳐서 하나의 포션으로 변환
    // 포션의 순서가 중요함

    [Header("Potion Synthesizer")]
    [SerializeField] private RecipeDatabase recipeDatabase;

    // Input칸 2개와 Output칸 3개 필요, Output칸에는 아이템을 넣을 수 없음
    // Inventory System[0]이 Main Potion
    // Inventory System[1]이 Sub Potion
    // Inventory System[2]이 Result

    public void EnterCraftMode() 
    {
        _CraftManager.Instance.object_PotionSynthesizer.SetActive(true);
        _CraftManager.Instance.herbPocket.EnterCraftMode();
    }

    public void SynthesizePotion()
    {
        foreach (var item in recipeDatabase.recipeData)
        {
            if(
                inventorySystem.inventorySlots[0].itemId == item.inputItemDatas[0].ID &&
                inventorySystem.inventorySlots[0].stackSize >= item.requireInputStackSize[0] &&
                inventorySystem.inventorySlots[1].itemId == item.inputItemDatas[1].ID &&
                inventorySystem.inventorySlots[1].stackSize >= item.requireInputStackSize[1]                 
                )
            
            {
                SuccessCraft(item);
                return;
            }
        }
        // 조합을 실패했을때 Input 아이템 사라지고 실패 Output 출력
        FailCraft();
    }

    public void SuccessCraft(RecipeData recipe)
    {
        Debug.Log("Success");
        inventorySystem.inventorySlots[2] = new _InventorySlot(recipe.outputItemData.ID, recipe.outputItemStackSize);
                
        inventorySystem.inventorySlots[0].RemoveFromStack(recipe.requireInputStackSize[0]);
        inventorySystem.inventorySlots[1].RemoveFromStack(recipe.requireInputStackSize[1]);

        RefreshDynamicInventory(inventorySystem);
    }

    public void FailCraft()
    {
        Debug.Log("Fail");
    }
}
