using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineCraftSystem : MonoBehaviour
{
    public RecipeBook recipeBook;
    public ItemRecipe foundRecipe;
    public ToolItemDisplay toolItemDisplay;

    public InventoryItemData mainItemData;
    public int mainItemDataAmount;
    public InventoryItemData subItemData;
    public int subItemDataAmount;
    public InventoryItemData resultItemData;
    public int resultItemDataAmount;

    public void CraftMedicine() // 재료를 슬롯에다가 넣고 craft버튼을 눌렀을때 실행되는 함수
    {        
        Debug.Log("Call Craft Medicine func");
        if(CanCraft() == false) return;
        else
        {
            if(resultItemData != null) 
            {
                if(resultItemData == foundRecipe.resultItemData)
                    resultItemDataAmount += foundRecipe.resultItemDataAmount;
            }
            else
            {
                resultItemData = foundRecipe.resultItemData;
                resultItemDataAmount = foundRecipe.resultItemDataAmount;
            }

            mainItemDataAmount -= foundRecipe.mainItemDataAmount;
            if(mainItemDataAmount <= 0) mainItemData = null;

            subItemDataAmount -= foundRecipe.subItemDataAmount;
            if(subItemDataAmount <= 0) subItemData = null;            
        }
        toolItemDisplay.UpdateItemData();
        foundRecipe = null;
    }


    public bool CanCraft() // 슬롯에 채워진 아이템의 정보와 레시피의 정보가 같은지 체크
    {
        // 제작 결과창에서 아직 아이템을 옮기지 않았을 경우
        Debug.Log("Call CanCraft func");
        // if(resultItemData != null) 
        // {
        //     Debug.Log("resultItemData != null");
        //     return false;
        // }
        foreach (var recipe in recipeBook.recipes)
        {
            // 1. mainItem과 subItem이 레시피와 일치하는지 확인
            if(recipe.mainItemData == mainItemData && recipe.subItemData == subItemData)
            {
                // 2. 요구 수량이 같은지 확인
                if(recipe.mainItemDataAmount <= mainItemDataAmount && recipe.subItemDataAmount <= subItemDataAmount)
                {
                    foundRecipe = recipe;
                    
                    Debug.Log("Recipe Found");
                    return true;
                }
            }
        }
        Debug.Log("Recipe Not Found");
        return false;
    }
}
