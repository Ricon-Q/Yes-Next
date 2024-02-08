using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    private static PlayerInputManager instance;

    public static PlayerInputManager Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }
            return instance;
        }
    }
    private void Awake()
    {
     
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    static private bool inputMode = false;
    private void Update()
    {
        Input();
    }

    public void Input()
    {
        if(inputMode == false) return;

        else
        {
            InputMyInfomation();
        }
    }

    static public void SetInputMode(bool active)
    {
        inputMode = active;
    }
    public bool GetInputMode()
    {
        return inputMode;
    }

    static public void SetPlayerInput(bool value)
    {
        // UI Canvas, 플레이어 이동, 플레이어 입력 제어
        UiManager.Instance.ToggleUiCanvas(value);
        inputMode = value;
        PlayerMovement.SetMoveMode(value);
    }

    public void InputMyInfomation()
    {
        if(!MyInfomation.Instance.isMyInfoOpen)
        {
            if (InputManager.Instance.GetToggleInventoryPressed())
                MyInfomation.Instance.ToggleInventory();
            else if(InputManager.Instance.GetToggleGuideBookPressed())
                MyInfomation.Instance.ToggleGuideBook();
            else if(InputManager.Instance.GetToggleQeustPressed())
                MyInfomation.Instance.ToggleQuest();
            else if(InputManager.Instance.GetToggleHospitalInfoPressed())
                MyInfomation.Instance.ToggleHospitalInfo();
            else if(InputManager.Instance.GetToggleOptionPressed())
                MyInfomation.Instance.ToggleOption();
            else if(InputManager.Instance.GetEscapePressed())
                MyInfomation.Instance.MyInfoEscape();
        }
        else
            if (InputManager.Instance.GetToggleInventoryPressed() || InputManager.Instance.GetEscapePressed())
                MyInfomation.Instance.ExitMyInfomation();
    }
}
