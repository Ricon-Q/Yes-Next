using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;

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
    [SerializeField] private GameObject _toolButtonImage;

    [Header("Tool Panel")]
    [SerializeField] private Button _activeButton;
    [SerializeField] private Animator _animator;

    public void EnterCraftMode()
    {
        OffVisualStaminaTime();
        _CraftManager.Instance.object_PotionPot.SetActive(true);
        _CraftManager.Instance.herbPocket.EnterCraftMode();
        _CraftManager.Instance.potionStand.EnterCraftMode();
    }
    override public void CreateInventorySlot()
    {
        slotDictionary = new Dictionary<_InventorySlot_UI, _InventorySlot>();

        if(inventorySystem == null) return;

        for (int i = 0; i < inventorySystem.inventorySize; i++)
        {
            var uiSlot = Instantiate(slotPrefab, transform);
            slotDictionary.Add(uiSlot, inventorySystem.inventorySlots[i]);
            uiSlot.Init(inventorySystem.inventorySlots[i]);
            uiSlot.UpdateUISlot();
            if(i == inventorySystem.inventorySize-1)
            { 
                inventorySystem.inventorySlots[i].isCraftResultSlot = true;
                // Debug.Log("isCraftResult");
            }
        }
    }

    public override void AssignSlot(_InventorySystem invToDisplay)
    {
        slotDictionary = new Dictionary<_InventorySlot_UI, _InventorySlot>();

        if(invToDisplay == null) return;

        for (int i = 0; i < invToDisplay.inventorySize; i++)
        {
            var uiSlot = Instantiate(slotPrefab, transform);
            slotDictionary.Add(uiSlot, invToDisplay.inventorySlots[i]);
            uiSlot.Init(invToDisplay.inventorySlots[i]);
            uiSlot.UpdateUISlot();

            if(i == inventorySystem.inventorySize-1)
            { 
                inventorySystem.inventorySlots[i].isCraftResultSlot = true;
                // Debug.Log("isCraftResult");
            }
        }
    }
    public void ExitToolMode()
    {
        // [0~3] 슬롯에 아이템이 남아있다면 아이템들을 플레이어 인벤토리로 옮긴 후 종료
        foreach (var item in inventorySystem.inventorySlots)
        {
            if(item.itemId != -1)
            {
                PlayerInventoryManager.Instance.AddToInventory(item.itemId, item.stackSize);
                item.ClearSlot();
            }
        }

        // 슬롯 칸 새로고침
        RefreshDynamicInventory(this.inventorySystem);
        _toolButtonImage.SetActive(true);

        _CraftManager.Instance.object_PotionPot.SetActive(false);
        _CraftManager.Instance.herbPocket.ExitToolMode();
        _CraftManager.Instance.potionStand.ExitToolMode();
        _CraftManager.Instance.craftToolCanvas.SetActive(false);
    }

    public void Craft()
    {
        // [3] 슬롯이 할당되어있을때 - [3] 슬롯 아이템을 인벤토리로 이동
        if(inventorySystem.inventorySlots[3].itemId != -1)
        {
            PlayerInventoryManager.Instance.AddToInventory(inventorySystem.inventorySlots[3].itemId, inventorySystem.inventorySlots[3].stackSize);   
            inventorySystem.inventorySlots[3].ClearSlot();
            _CraftManager.Instance.herbPocket.RefreshDynamicInventory(_CraftManager.Instance.herbPocket.inventorySystem);
            _CraftManager.Instance.potionStand.RefreshDynamicInventory(_CraftManager.Instance.potionStand.inventorySystem);
            RefreshDynamicInventory(inventorySystem);
        }

        RecipeData foundedRecipe = CheckRecipe();

        if(foundedRecipe != null)
        {
            StartCoroutine(PlayCraftAnimation_Success(foundedRecipe));
            return;
        }
        
        else FailCraft();
    }

    public RecipeData CheckRecipe()
    {
        // foreach (var item in recipeDatabase.recipeData)
        // {
        //     if(
        //         inventorySystem.inventorySlots[0].itemId == item.inputItemDatas[0].ID &&
        //         inventorySystem.inventorySlots[0].stackSize >= item.requireInputStackSize[0] &&
        //         inventorySystem.inventorySlots[1].itemId == item.inputItemDatas[1].ID &&
        //         inventorySystem.inventorySlots[1].stackSize >= item.requireInputStackSize[1] &&
        //         inventorySystem.inventorySlots[2].itemId == item.inputItemDatas[2].ID &&
        //         inventorySystem.inventorySlots[2].stackSize >= item.requireInputStackSize[2]                
        //         )
            
        //     {
        //         return item;
        //     }
        // }
        // return null;

        foreach (var recipe in recipeDatabase.recipeDatas)
        {
            bool recipeMatch = true; // 레시피가 일치하는지 추적합니다.

            foreach (var inputItemData in recipe.inputItemDatas)
            {
                bool itemMatchFound = false; // 현재 입력 아이템에 대한 일치 항목을 찾았는지 추적합니다.

                // 인벤토리 슬롯을 순회하며 아이템 검사
                foreach (var inventorySlot in inventorySystem.inventorySlots)
                {
                    if (inventorySlot.itemId == inputItemData.ID && inventorySlot.stackSize >= recipe.requireInputStackSize[Array.IndexOf(recipe.inputItemDatas, inputItemData)])
                    {
                        itemMatchFound = true; // 일치하는 아이템을 찾았습니다.
                        break; // 해당 아이템에 대한 검사를 중단합니다.
                    }
                }

                if (!itemMatchFound) // 일치하는 아이템을 찾지 못한 경우
                {
                    recipeMatch = false; // 레시피가 일치하지 않습니다.
                    break; // 더 이상 이 레시피를 검사할 필요가 없습니다.
                }
            }

            if (recipeMatch) // 모든 입력 아이템에 대해 일치하는 아이템을 찾았다면
            {
                // Debug.Log("Found Recipe : " + recipe.recipeName);
                return recipe; // 해당 레시피 반환
            }
        }
        // Debug.Log("Not Found");
        return null; // 일치하는 레시피를 찾지 못함
    }

    public void SuccessCraft(RecipeData recipe)
    {
        Debug.Log("Success");
        inventorySystem.inventorySlots[3] = new _InventorySlot(recipe.outputItemData.ID, recipe.outputItemStackSize);

        for(int i = 0; i < 3; i++)
            if(inventorySystem.inventorySlots[i].itemId != -1)
                inventorySystem.inventorySlots[i].RemoveFromStack(recipe.requireInputStackSize[i]);

        RefreshDynamicInventory(inventorySystem);

        _PlayerManager.Instance.playerData.AddCurrentStamina(-recipe.useStamina);
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
                StartCoroutine(PlayCraftAnimation_Fail());
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

    private IEnumerator PlayCraftAnimation_Success(RecipeData recipe)
    {
        _activeButton.interactable = false;
        _animator.SetTrigger("PlayAnimation");
        yield return new WaitForSeconds(1.0f);
        _activeButton.interactable = true;
        _animator.SetTrigger("StopAnimation");

        SuccessCraft(recipe);
    }

    private IEnumerator PlayCraftAnimation_Fail()
    {
        _activeButton.interactable = false;
        _animator.SetTrigger("PlayAnimation");
        yield return new WaitForSeconds(1.0f);
        _activeButton.interactable = true;
        _animator.SetTrigger("StopAnimation");

        _PlayerManager.Instance.playerData.currentStamina -= 5;
        _TimeManager.Instance.increaseMinute(5);

        foreach (var item in inventorySystem.inventorySlots)
            if(item.itemId != -1)
                item.RemoveFromStack(1);

        RefreshDynamicInventory(this.inventorySystem);
    }
}
