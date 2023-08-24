using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    public void GoToRoom()
    {
        // if(InputManager.Instance.GetLeftClickPressed())
        SceneManager.LoadScene("Eientei_Room");
    }
}
