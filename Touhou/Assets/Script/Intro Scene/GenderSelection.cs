using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenderSelection : MonoBehaviour
{
    [Header("Portrait")]
    public Image mainPortrait;
    public Sprite malePortrait;
    public Sprite femalePortrait;

    [Header("ToggleGender")]
    public Button maleButton;
    public Button femaleButton;

    private void Start() 
    {
        maleButton.interactable = false;
        femaleButton.interactable = true;   
        mainPortrait.sprite = malePortrait; 
    }

    public void OnMaleButtonClick()
    {
        maleButton.interactable = false;
        femaleButton.interactable = true;
        mainPortrait.sprite = malePortrait;
    }

    public void OnFemaleButtonClick()
    {
        maleButton.interactable = true;
        femaleButton.interactable = false;
        mainPortrait.sprite = femalePortrait;
    }
}
