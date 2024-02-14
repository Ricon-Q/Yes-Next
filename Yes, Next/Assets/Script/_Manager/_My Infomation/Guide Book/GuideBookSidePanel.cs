using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideBookSidePanel : MonoBehaviour
{
    [Header("Recipe Database")]
    [SerializeField] private RecipeDatabase _mortarAndPestleRecipeDatabase;
    [SerializeField] private RecipeDatabase _potionPotRecipeDatabase;
    [SerializeField] private RecipeDatabase _potionSynthesizerRecipeDatabase;

    [Header("Fold")]
    [SerializeField] private GameObject _mortarAndPestleFold;
    private bool _isMortarOpen;
    [SerializeField] private GameObject _potionPotFold;
    private bool _isPotionPotOpen;

    public void FoldMortarAndPestle()
    {
        _isMortarOpen = !_isMortarOpen;
        _mortarAndPestleFold.SetActive(_isMortarOpen);
    }
    public void FoldMortarAndPestle(bool value)
    {
        _isMortarOpen = value;
        _mortarAndPestleFold.SetActive(_isMortarOpen);
    }

    public void FoldPotionPot()
    {
        _isPotionPotOpen = !_isPotionPotOpen;
        _potionPotFold.SetActive(_isPotionPotOpen);
    }
    public void FoldPotionPot(bool value)
    {
        _isPotionPotOpen = value;
        _potionPotFold.SetActive(_isPotionPotOpen);
    }
    
}
