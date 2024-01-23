using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe Data", menuName = "Craft System/Recipe Data")]
public class RecipeData : ScriptableObject
{
    // 레시피북에 들어갈 레시피 데이터
    [Header("Recipe Info")]
    public string recipeName;
    [TextArea(10, 10)]
    public string description;

    [Header("Stamina, Time")]
    public int useStamina;
    public int useTimeMinute;

    [Header("Input")]
    // public int input; // 레시피에서 Input되는 아이템 종류의 갯수
    public InventoryItemData[] inputItemDatas; // input되는 아이템목록
    public int[] requireInputStackSize; //inputItemData에서 각 index 아이템별 필요 갯수

    [Header("Output")]
    public InventoryItemData outputItemData; // output되는 아이템 정보
    public int outputItemStackSize; // output아이템의 갯수
}
