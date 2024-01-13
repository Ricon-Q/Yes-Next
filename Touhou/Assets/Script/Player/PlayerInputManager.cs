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
            if (InputManager.Instance.GetToggleInventoryPressed())
            {
                Debug.Log("ToogleInv");
                
                if(ShopManager.Instance.isShopMode) return;
                
                PlayerInventoryManager.Instance.ToggleInventory();
            }
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
}
