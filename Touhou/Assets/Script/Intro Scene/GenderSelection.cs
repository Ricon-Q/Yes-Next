using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GenderSelection : MonoBehaviour
{
    [Header("Name")]
    public TMP_InputField nameInput;

    [Header("Portrait")]
    public Image mainPortrait;
    public Sprite malePortrait;
    public Sprite femalePortrait;

    [Header("ToggleGender")]
    public Button maleButton;
    public Button femaleButton;
    private bool isMale;

    private void Start() 
    {
        isMale = true;
        maleButton.interactable = false;
        femaleButton.interactable = true;   
        mainPortrait.sprite = malePortrait; 
    }

    public void OnMaleButtonClick()
    {
        isMale = true;
        maleButton.interactable = false;
        femaleButton.interactable = true;
        mainPortrait.sprite = malePortrait;
    }

    public void OnFemaleButtonClick()
    {
        isMale = false;
        maleButton.interactable = true;
        femaleButton.interactable = false;
        mainPortrait.sprite = femalePortrait;
    }

    public void Submit()
    {
        _PlayerManager.Instance.playerData.isMale = isMale;
        _PlayerManager.Instance.playerData.name = nameInput.text;
        // _PlayerManager.Instance.playerData.playerPortrait = mainPortrait.sprite;
        InventoryManager.Instance.UpdateCharacterInfo();
        FadeInOutManager.Instance.ChangeScene("Intro Cutscene");
    }
}

