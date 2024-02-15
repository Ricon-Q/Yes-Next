using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private GuideBookButton _guideBookButton;

    private void Awake() 
    {
        CreateMortarButton();    
        CreatePotionPotButton();
        FoldMortarAndPestle(true);
        FoldPotionPot(true);
    }

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
    
    public void CreateMortarButton()
    {
        if(_mortarAndPestleRecipeDatabase.recipeDatas.Count == 0) return;

        for(int i = 0; i < _mortarAndPestleRecipeDatabase.recipeDatas.Count; i++)
        {
            var button = Instantiate(_guideBookButton, _mortarAndPestleFold.transform);
            button.AllocateRecipeData(_mortarAndPestleRecipeDatabase.recipeDatas[i]);
        }
    }

    public void CreatePotionPotButton()
    {
        if(_potionPotRecipeDatabase.recipeDatas.Count == 0) return;

        for(int i = 0; i < _potionPotRecipeDatabase.recipeDatas.Count; i++)
        {
            var button = Instantiate(_guideBookButton, _potionPotFold.transform);
            button.AllocateRecipeData(_potionPotRecipeDatabase.recipeDatas[i]);
        }
    }

    public void DisplayRecipe(RecipeData recipeData)
    {
        switch(recipeData.toolType)
        {
            case 0:
                MyInfomation.Instance._guideBook.AllocateRecipeDataMortar(recipeData);
                break;
            case 1:
                MyInfomation.Instance._guideBook.AllocateRecipeDataPotionPot(recipeData);
                break;
            case 2:
                break;
        }
    }
}
