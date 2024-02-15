using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryDisplay : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _raceButton;
    [SerializeField] private Button _diseaseButton;
    [SerializeField] private Button _symptomButton;
    [SerializeField] private Button _potionButton;

    [Header("Display")]
    [SerializeField] private GameObject _raceDisplay;
    [SerializeField] private GameObject _diseaseDisplay;
    [SerializeField] private GameObject _symptomDisplay;
    [SerializeField] private GameObject _potionDisplay;

    public void OpenGuideBook()
    {
        ChangeCategory(0);
    }

    public void ChangeCategory(int index)
    {
        EnableAllButton();
        DisableAllDisplay();
        switch(index)
        {
            case 0:
                _raceButton.interactable = false;
                _raceDisplay.SetActive(true);
                break;
            case 1:
                _diseaseButton.interactable = false;
                _diseaseDisplay.SetActive(true);
                break;
            case 2:
                _symptomButton.interactable = false;
                _symptomDisplay.SetActive(true);
                break;
            case 3:
                _potionButton.interactable = false;
                _potionDisplay.SetActive(true);
                break;
        }
    }

    public void EnableAllButton()
    {
        _raceButton.interactable = true;
        _diseaseButton.interactable = true;
        _symptomButton.interactable = true;
        _potionButton.interactable = true;
    }
    public void DisableAllDisplay()
    {
        _raceDisplay.SetActive(false);
        _diseaseDisplay.SetActive(false);
        _symptomDisplay.SetActive(false);
        _potionDisplay.SetActive(false);
    }
}
