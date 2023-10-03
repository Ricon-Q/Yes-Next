using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineCraftSystem : MonoBehaviour
{
    public ItemRecipe selectedRecipe;
    public InventoryItemData mainItemData;
    public int mainItemDataAmount;
    public InventoryItemData subItemData;
    public int subItemDataAmount;
    public InventoryItemData resultItemData;
    public int resultItemDataAmount;

    public void CraftMedicine() // 재료를 슬롯에다가 넣고 craft버튼을 눌렀을때 실행되는 함수
    {        
        if(!CheckItemData()) return;
        else
        {
            resultItemData = selectedRecipe.resultItemData;
            resultItemDataAmount = selectedRecipe.resultItemDataAmount;

            mainItemDataAmount -= selectedRecipe.mainItemDataAmount;
            if(mainItemDataAmount <= 0) mainItemData = null;

            subItemDataAmount -= selectedRecipe.subItemDataAmount;
            if(subItemDataAmount <= 0) subItemData = null;            
        }
    }

    public bool CheckItemData() // 슬롯에 채워진 아이템의 정보와 레시피의 정보가 같은지 체크
    {
        // 제작 결과창에서 아직 아이템을 옮기지 않았을 경우
        if(resultItemData) return false;
        // 아이템 종류가 다를시 false 반환
        if(mainItemData != selectedRecipe.mainItemData || subItemData != selectedRecipe.subItemData) 
            return false;
        // 아이템의 갯수가 요구 갯수보다 적을시 false 반환
        else if(mainItemDataAmount < selectedRecipe.mainItemDataAmount || subItemDataAmount < selectedRecipe.subItemDataAmount) 
            return false;
        else
            return true;
    }
}
