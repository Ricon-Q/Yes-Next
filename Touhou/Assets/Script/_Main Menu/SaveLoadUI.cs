using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadUI : MonoBehaviour
{
    
    public GameObject newGame;
    public GameObject loadPanel;
    public GameObject emptySavePanel;

    public void EnableContinue()
    {
        newGame.SetActive(false);
        loadPanel.SetActive(true);
        emptySavePanel.SetActive(false);
    }

    public void DisableContinue()
    {
        newGame.SetActive(true);
        loadPanel.SetActive(false);
        emptySavePanel.SetActive(true);
    }
}
