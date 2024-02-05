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
    
    private bool inputMode = false;
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

    public void SetInputMode(bool active)
    {
        inputMode = active;
    }
    public bool GetInputMode()
    {
        return inputMode;
    }

    public void InputMyInfomation()
    {
        if (InputManager.Instance.GetToggleInventoryPressed())
            MyInfomation.Instance.ToggleInventory();
        else if(InputManager.Instance.GetToggleGuideBookPressed())
            MyInfomation.Instance.ToggleGuideBook();
        else if(InputManager.Instance.GetToggleHospitalInfoPressed())
            MyInfomation.Instance.ToggleHospitalInfo();
        else if(InputManager.Instance.GetToggleOptionPressed())
            MyInfomation.Instance.ToggleOption();
        else if(InputManager.Instance.GetEscapePressed())
            MyInfomation.Instance.MyInfoEscape();

    }
}
