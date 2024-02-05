using TMPro;
using UnityEngine;

// 포션 카테고리만 이용 가능
// 두 포션을 합쳐서 하나의 포션으로 변환
// 포션의 순서가 중요함

public class PotionSynthesizer : _DynamicInventoryDisplay
{
    [Header("Potion Synthesizer")]
    [SerializeField] private RecipeDatabase recipeDatabase;
    [SerializeField] private GameObject visualStaminaTime;
    [SerializeField] private TextMeshProUGUI visualStaminaTimeText;
    // [TextArea(10,5)]
    // [SerializeField] private string warningText;

    // Input칸 2개와 Output칸 1개 필요, Output칸에는 아이템을 넣을 수 없음
    // Inventory System[0]이 Main Potion
    // Inventory System[1]이 Sub Potion
    // Inventory System[2]이 Result

    public void EnterCraftMode() 
    {
        OffVisualStaminaTime();
        _CraftManager.Instance.object_PotionSynthesizer.SetActive(true);
        _CraftManager.Instance.potionStand.EnterCraftMode();
        // _CraftManager.Instance.potionStand.EnterCraftMode();
    }
    public void ExitToolMode()
    {
        // 슬롯의 [0~2] 칸에 아이템이 남아있다면 아이템들을 플레이어 인벤토리로 옮긴 후 종료
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

        _CraftManager.Instance.object_PotionSynthesizer.SetActive(false);
        _CraftManager.Instance.potionStand.ExitToolMode();
        _CraftManager.Instance.craftToolCanvas.SetActive(false);
    }

    public void SynthesizePotion()
    {
        // [2] 슬롯이 할당되어있을때 - [2] 슬롯 아이템을 인벤토리로 이동
        if(inventorySystem.inventorySlots[2].itemId != -1)
        {
            PlayerInventoryManager.Instance.potionInventory.AddToInventory(inventorySystem.inventorySlots[2].itemId, inventorySystem.inventorySlots[2].stackSize);   
            inventorySystem.inventorySlots[2].ClearSlot();
            _CraftManager.Instance.potionStand.RefreshDynamicInventory(_CraftManager.Instance.potionStand.inventorySystem);
            RefreshDynamicInventory(inventorySystem);
        }

        RecipeData foundedRecipe = CheckRecipe();

        if(foundedRecipe != null)
        {   
            SuccessCraft(foundedRecipe);
            return;
        }   

        // 적합한 레시피를 찾지 못하면 FailCraft();
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
                inventorySystem.inventorySlots[1].stackSize >= item.requireInputStackSize[1]                 
                )
            
            {
                return item;
            }
        }
        return null;
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

    public void SuccessCraft(RecipeData recipe)
    {
        Debug.Log("Success");
        inventorySystem.inventorySlots[2] = new _InventorySlot(recipe.outputItemData.ID, recipe.outputItemStackSize);
                
        inventorySystem.inventorySlots[0].RemoveFromStack(recipe.requireInputStackSize[0]);
        inventorySystem.inventorySlots[1].RemoveFromStack(recipe.requireInputStackSize[1]);

        RefreshDynamicInventory(inventorySystem);

        _PlayerManager.Instance.playerData.AddCurrentStamina(-recipe.useStamina);
        _TimeManager.Instance.increaseMinute(recipe.useTimeMinute);
    }

    public void FailCraft()
    {
        Debug.Log("Fail");

        // [0] 슬롯 [1] 슬롯 모두 비어있을때 - 시간, 피로도 소모 X
        if(
            inventorySystem.inventorySlots[0].itemId == -1 &&
            inventorySystem.inventorySlots[1].itemId == -1
            )
        {
            return;
        }
        // [0], [1] 슬롯중 하나만 비어져있을때 - 시간, 피로도 소모 X, 기존 포션 삭제 X
        if(
            (inventorySystem.inventorySlots[0].itemId != -1 &&
            inventorySystem.inventorySlots[1].itemId == -1) ||
            (inventorySystem.inventorySlots[0].itemId == -1 &&
            inventorySystem.inventorySlots[1].itemId != -1)
            )
        {
            return;
        }
        // [0], [1] 슬롯 모두 할당되어있을때 - 시간, 피로도 소모 O, 기존 포션 삭제 O
        if(
            inventorySystem.inventorySlots[0].itemId != -1 &&
            inventorySystem.inventorySlots[1].itemId != -1
            )
        {
            _PlayerManager.Instance.playerData.currentStamina -= 5;
            _TimeManager.Instance.increaseMinute(5);

            inventorySystem.inventorySlots[0].ClearSlot();
            inventorySystem.inventorySlots[1].ClearSlot();
            RefreshDynamicInventory(this.inventorySystem);

            return;
        }
    }
}
