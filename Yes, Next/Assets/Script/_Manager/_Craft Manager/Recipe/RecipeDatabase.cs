using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe Book", menuName = "Craft System/Recipe Book")]
public class RecipeDatabase : ScriptableObject
{
    // 포션 제작에 쓰이는 레시피 북
    // 도구별로 나뉘며 Morta And Pestle, Potion Pot, Potion Systhesizer이 있다
    public RecipeData[] recipeData;
}