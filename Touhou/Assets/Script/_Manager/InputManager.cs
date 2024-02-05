using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// This script acts as a single point for all other scripts to get
// the current input from. It uses Unity's new Input System and
// functions should be mapped to their corresponding controls
// using a PlayerInput component with Unity Events.

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    private Vector2 moveDirection = Vector2.zero;
    private bool jumpPressed = false;
    private bool interactPressed = false;
    private bool submitPressed = false;

    // My Infomation
    private bool toggleInventoryPressed = false;
    private bool toggleGuideBookPressed = false;

    private bool toggleHospitalInfoPressed = false;
    private bool toggleOptionPressed = false;
    private bool _escapePressed = false;

    // Hotbar
    private bool _hotBarInteractPressed = false;
    private Vector2 _mouseScroll = Vector2.zero;
    // private bool onLeftClick = false;

    private static InputManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }
    
    public void IsActive()
    {
        Debug.Log("Input Manager Is " + gameObject.activeSelf);
    }


    // Keyborad

    public void MovePressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveDirection = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            moveDirection = context.ReadValue<Vector2>();
        } 
    }

    public void JumpPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpPressed = true;
        }
        else if (context.canceled)
        {
            jumpPressed = false;
        }
    }

    public void InteractButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            interactPressed = true;
        }
        else if (context.canceled)
        {
            interactPressed = false;
        } 
    }

    public void HotBarInteractButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _hotBarInteractPressed = true;
        }
        else if (context.canceled)
        {
            _hotBarInteractPressed = false;
        } 
    }

    public void EscapePressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _escapePressed = true;
        }
        else if (context.canceled)
        {
            _escapePressed = false;
        } 
    }


    // ========================= My Infomation ========================= //

    public void ToggleInventoryPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            toggleInventoryPressed = true;
        }
        else if (context.canceled)
        {
            toggleInventoryPressed = false;
        } 
    }
    public void ToggleGuideBookPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            toggleGuideBookPressed = true;
        }
        else if (context.canceled)
        {
            toggleGuideBookPressed = false;
        } 
    }
    public void ToggleHospitalInfoPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            toggleHospitalInfoPressed = true;
        }
        else if (context.canceled)
        {
            toggleHospitalInfoPressed = false;
        } 
    }
    public void ToggleOptionPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            toggleOptionPressed = true;
        }
        else if (context.canceled)
        {
            toggleOptionPressed = false;
        } 
    }

    public void SubmitPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            submitPressed = true;
        }
        else if (context.canceled)
        {
            submitPressed = false;
        } 
    }

    // Mouse

    public void MouseScroll(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            _mouseScroll = context.ReadValue<Vector2>();
        }
        else if(context.canceled)
        {
            _mouseScroll = context.ReadValue<Vector2>();
        }
    }

    // Get
    public Vector2 GetMoveDirection() 
    {
        return moveDirection;
    }

    // for any of the below 'Get' methods, if we're getting it then we're also using it,
    // which means we should set it to false so that it can't be used again until actually
    // pressed again.

    public bool GetJumpPressed() 
    {
        bool result = jumpPressed;
        jumpPressed = false;
        return result;
    }

    public bool GetInteractPressed() 
    {
        bool result = interactPressed;
        interactPressed = false;
        return result;
    }

    public bool GetHotBarInteractPressed()
    {
        bool result = _hotBarInteractPressed;
        _hotBarInteractPressed = false;
        return result;
    }

    public bool GetEscapePressed()
    {
        bool result = _escapePressed;
        _escapePressed = false;
        return result;
    }

    // ================== My Infomation  =================//
    public bool GetToggleInventoryPressed()
    {
        bool result = toggleInventoryPressed;
        toggleInventoryPressed = false;
        return result;
    }
    public bool GetToggleGuideBookPressed()
    {
        bool result = toggleGuideBookPressed;
        toggleGuideBookPressed = false;
        return result;
    }
    public bool GetToggleHospitalInfoPressed()
    {
        bool result = toggleHospitalInfoPressed;
        toggleHospitalInfoPressed = false;
        return result;
    }
    public bool GetToggleOptionPressed()
    {
        bool result = toggleOptionPressed;
        toggleOptionPressed = false;
        return result;
    }

    // =================================================== //

    public bool GetSubmitPressed() 
    {
        bool result = submitPressed;
        submitPressed = false;
        return result;
    }

    public void RegisterSubmitPressed() 
    {
        submitPressed = false;
    }

    public Vector2 GetMouseScroll()
    {
        return _mouseScroll;
    }

}