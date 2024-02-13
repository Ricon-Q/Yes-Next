using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using UnityEngine.UI;

// 약초 카테고리만 이용 가능
// 한 종류의 약초를 갈아서 약초 가루로 변경 가능

public class MortarAndPestle : _DynamicInventoryDisplay
{
    [Header("Mortar and Pestle")]
    [SerializeField] private RecipeDatabase recipeDatabase;
    [SerializeField] private GameObject visualStaminaTime;
    [SerializeField] private TextMeshProUGUI visualStaminaTimeText;
    [SerializeField] private GameObject _toolButtonImage;

    [Header("Tool Panel")]
    [SerializeField] private Button _activeButton;
    [SerializeField] private Animator _animator;

    // Input칸 1개와 Output칸 1개 필요, Output칸에는 아이템을 넣을 수 없음
    // InventorySystem[0]은 Main herb
    // InventorySystem[1]은 Result;

    public void EnterCraftMode()
    {
        OffVisualStaminaTime();
        _CraftManager.Instance.object_MortarAndPestle.SetActive(true);
        _CraftManager.Instance.herbPocket.EnterCraftMode();
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
                Debug.Log("isCraftResult");
            }
        }
    }

    public void ExitToolMode()
    {
        // [0], [1] 슬롯에 아이템이 남아 있다면 아이템들을 플레이어 인벤토리로 옮긴 이후에 종료
        foreach (var item in inventorySystem.inventorySlots)
        {
            if(item.itemId != -1)
            {
                PlayerInventoryManager.Instance.AddToInventory(item.itemId, item.stackSize);
                item.ClearSlot();
            }
        }
        RefreshDynamicInventory(this.inventorySystem);
        _toolButtonImage.SetActive(true);

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
            PlayerInventoryManager.Instance.AddToInventory(inventorySystem.inventorySlots[1].itemId, inventorySystem.inventorySlots[1].stackSize);
            inventorySystem.inventorySlots[1].ClearSlot();
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
            StartCoroutine(PlayCraftAnimation_Fail());
            // _PlayerManager.Instance.playerData.currentStamina -= 5;
            // _TimeManager.Instance.increaseMinute(5);

            // inventorySystem.inventorySlots[0].ClearSlot();
            // RefreshDynamicInventory(this.inventorySystem);

            return;
        }
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

        inventorySystem.inventorySlots[0].RemoveFromStack(1);
        RefreshDynamicInventory(this.inventorySystem);
    }
}
