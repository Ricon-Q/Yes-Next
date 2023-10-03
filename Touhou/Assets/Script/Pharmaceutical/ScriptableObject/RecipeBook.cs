using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Recipe/Recipe Book")]
public class RecipeBook : ScriptableObject
{
    public string recipeBookName;
    public List<ItemRecipe> recipes = new List<ItemRecipe>();
    // public int recipeAmount;

    // public void AddRecipe(ItemRecipe recipe)
    // {
    //     recipes.Add(recipe);
    //     recipeAmount = recipes.Count;
    // }

    // public void RemoveRecipe(ItemRecipe recipe)
    // {
    //     recipes.Remove(recipe);
    //     recipeAmount = recipes.Count;
    // }
}
