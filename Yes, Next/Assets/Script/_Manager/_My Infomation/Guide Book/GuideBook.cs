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
    [SerializeField] private Image __potionPotItemImage0;
    [SerializeField] private TextMeshProUGUI _potionPotItemAmount0;
    [SerializeField] private Image __potionPotItemImage1;
    [SerializeField] private TextMeshProUGUI _potionPotItemAmount1;
    [SerializeField] private Image __potionPotItemImage2;
    [SerializeField] private TextMeshProUGUI _potionPotItemAmount2;

    private RecipeData _recipeData;

    [Header("Guide Book Side Panel")]
    [SerializeField] private GuideBookSidePanel _guideBookSidePanel;

    public void OpenGuideBook()
    {
        _guideBookSidePanel.FoldMortarAndPestle(false);
        _guideBookSidePanel.FoldPotionPot(false);
    }
}
