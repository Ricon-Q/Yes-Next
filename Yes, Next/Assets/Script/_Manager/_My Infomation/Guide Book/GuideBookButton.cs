using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GuideBookButton : MonoBehaviour
{
    public RecipeData _recipeData;
    public TextMeshProUGUI _recipeName;

    public void AllocateRecipeData(RecipeData recipeData)
    {
        _recipeData = recipeData;
        _recipeName.text = _recipeData.recipeName;
    }

    public void DisplayRecipe()
    {
        MyInfomation.Instance._guideBook._guideBookSidePanel.DisplayRecipe(_recipeData);
    }
}
