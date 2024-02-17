using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuideBook : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _recipeNameText;

    [Header("Mortar and Pestle")]
    [SerializeField] private GameObject _mortarAndPestleBook;
    [SerializeField] private Image _mortarAndPestleItemImage;
    [SerializeField] private TextMeshProUGUI _mortarAndPestleItemAmount;

    
    [Header("Potion Pot")]
    [SerializeField] private GameObject _potionPotBook;
    [SerializeField] private Image _potionPotItemImage0;
    [SerializeField] private TextMeshProUGUI _potionPotItemAmount0;
    [SerializeField] private Image _potionPotItemImage1;
    [SerializeField] private TextMeshProUGUI _potionPotItemAmount1;
    [SerializeField] private Image _potionPotItemImage2;
    [SerializeField] private TextMeshProUGUI _potionPotItemAmount2;

    private RecipeData _recipeData;

    [Header("Guide Book Side Panel")]
    public GuideBookSidePanel _guideBookSidePanel;

    public void OpenGuideBook()
    {
        _guideBookSidePanel.FoldMortarAndPestle(true);
        _guideBookSidePanel.FoldPotionPot(true);
        _guideBookSidePanel.FoldMortarAndPestle(false);
        _guideBookSidePanel.FoldPotionPot(false);

        OffAllBook();
        _recipeNameText.text = "레시피 확인";
    }

    public void AllocateRecipeDataMortar(RecipeData recipeData)
    {
        OffAllBook();
        _mortarAndPestleBook.SetActive(true);
        
        _recipeNameText.text = recipeData.recipeName;
        _mortarAndPestleItemImage.sprite = recipeData.inputItemDatas[0].Icon;
        _mortarAndPestleItemAmount.text = recipeData.requireInputStackSize[0].ToString();
    }

    public void AllocateRecipeDataPotionPot(RecipeData recipeData)
    {
        OffAllBook();
        _potionPotBook.SetActive(true);

        _recipeNameText.text = recipeData.recipeName;
        _potionPotItemImage0.sprite = recipeData.inputItemDatas[0].Icon;
        _potionPotItemAmount0.text = recipeData.requireInputStackSize[0].ToString();
        _potionPotItemImage1.sprite = recipeData.inputItemDatas[1].Icon;
        _potionPotItemAmount1.text = recipeData.requireInputStackSize[1].ToString();
        _potionPotItemImage2.sprite = recipeData.inputItemDatas[2].Icon;
        _potionPotItemAmount2.text = recipeData.requireInputStackSize[2].ToString();
    }

    public void OffAllBook()
    {
        _mortarAndPestleBook.SetActive(false);
        _potionPotBook.SetActive(false);
    }
}
