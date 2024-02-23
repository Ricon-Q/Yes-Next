using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuideBook : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _recipeNameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;

    [Header("Mortar and Pestle")]
    [SerializeField] private GameObject _mortarAndPestleBook;
    [SerializeField] private Image _mortarAndPestleItemImage;
    [SerializeField] private TextMeshProUGUI _mortarAndPestleItemAmount;
    [SerializeField] private TextMeshProUGUI _mortarItemName;
    

    
    [Header("Potion Pot")]
    [SerializeField] private GameObject _potionPotBook;
    [SerializeField] private List<Image> _potionPotItemImages;
    [SerializeField] private List<TextMeshProUGUI> _potionPotItemAmounts;
    [SerializeField] private List<TextMeshProUGUI> _potionPotItemName;
    

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
        _descriptionText.text = "";
    }

    public void AllocateRecipeDataMortar(RecipeData recipeData)
    {
        OffAllBook();
        _mortarAndPestleBook.SetActive(true);
        
        _recipeNameText.text = recipeData.recipeName;
        _mortarAndPestleItemImage.sprite = recipeData.inputItemDatas[0].Icon;
        _mortarAndPestleItemAmount.text = recipeData.requireInputStackSize[0].ToString();
        _descriptionText.text = recipeData.description;

        AllocateMortarItemName(recipeData);
    }

    public void AllocateRecipeDataPotionPot(RecipeData recipeData)
    {
        OffAllBook();
        _potionPotBook.SetActive(true);

        _recipeNameText.text = recipeData.recipeName;

        for (int i = 0; i < 3; i++)
        {
            if(recipeData.inputItemDatas[i] == null)
            {
                _potionPotItemImages[i].sprite = null;
                _potionPotItemImages[i].color = Color.clear; 
                _potionPotItemAmounts[i].text = "";
            }
            else
            {
                _potionPotItemImages[i].color = Color.white; 
                _potionPotItemImages[i].sprite = recipeData.inputItemDatas[i].Icon;;
                _potionPotItemAmounts[i].text = recipeData.requireInputStackSize[0].ToString();
            }
        }
        
        _descriptionText.text = recipeData.description;

        AllocatePotionPotItemName(recipeData);
    }

    public void AllocateMortarItemName(RecipeData recipeData)
    {
        _mortarItemName.text = recipeData.inputItemDatas[0].DisplayName;
    }

    public void AllocatePotionPotItemName(RecipeData recipeData)
    {
        for (int i = 0; i < 3; i++)
        {
            if(recipeData.inputItemDatas[i] == null)
            {
                _potionPotItemName[i].text = "";
            }
            else
            {
                _potionPotItemName[i].text =recipeData.inputItemDatas[i].DisplayName;
            }
        }
    }

    public void OffAllBook()
    {
        _mortarAndPestleBook.SetActive(false);
        _potionPotBook.SetActive(false);
    }
}
