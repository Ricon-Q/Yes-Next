using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class HotBarSelected : MonoBehaviour
{
    [SerializeField] private int _hotBarSelected = 0;
    [SerializeField] private RectTransform _image;

    private void Update() 
    {
        ChangeHotBar();
        HandleInteract();
    }

    public void ChangeHotBar()
    {
        if(InputManager.Instance.GetMouseScroll().y > 0)
        {
            _hotBarSelected = Mathf.Clamp(_hotBarSelected+1, 0, 11);
            MoveImage();
        }
        else if(InputManager.Instance.GetMouseScroll().y < 0)
        {
            _hotBarSelected = Mathf.Clamp(_hotBarSelected-1, 0, 11);
            MoveImage();
        }
        else return;
    }

    public void MoveImage()
    {
        _image.anchoredPosition  = new Vector3(_hotBarSelected * 100, 0, 0); 
    }

    public void HandleInteract()
    {
        if(!InputManager.Instance.GetHotBarInteractPressed()) return;
        
        else
        {
            int _hotbarIndex = PlayerInventoryManager.Instance.playerInventory.inventorySlots[_hotBarSelected].itemId;
            if(_hotbarIndex != -1)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = Camera.main.nearClipPlane; // Set the z value to the camera's near clip plane
                Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePos);
                worldMousePosition.z = 0;
                // Debug.Log(worldMousePosition);
                PlayerInventoryManager.Instance.itemDataBase.Items[_hotbarIndex].Interact();
            }
        }
    }
}
