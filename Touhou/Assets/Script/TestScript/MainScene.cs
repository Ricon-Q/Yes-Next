using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;
    public void GoToRoom()
    {
        // if(InputManager.Instance.GetLeftClickPressed())
        SceneManager.LoadScene("Eientei_Room");
    }

    private void Start() 
    {
        if(!DataPersistenceManager.Instance.HasGameData())
        {
            continueGameButton.interactable = false;
        }    
    }

    public void OnNewGameClicked()
    {
        DisableMenuButtons();
        DataPersistenceManager.Instance.NewGame();
        SceneManager.LoadSceneAsync("Eientei_Room");
    }

    public void OnContinueGameClicked()
    {
        DisableMenuButtons();
        SceneManager.LoadSceneAsync("Eientei_Room");
    }

    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }
}
