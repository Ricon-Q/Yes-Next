using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPanel : MonoBehaviour
{
    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }

    public void BackToTitle()
    {
        GameManager.Instance.BackToTitle();
    }
}
