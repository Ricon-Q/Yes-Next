using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class HotBarSelected : MonoBehaviour
{
    [SerializeField] private int _hotBarSelected = 0;
    [SerializeField] private RectTransform _image;
    [SerializeField] private UiMouseObject _uiMouseObject;

    private void Start() 
    {
        MoveImage();    
    }
    private void Update() 
    {
        ChangeHotBar();
        HandleInteract();
    }

    private int SelectedIndex
    {
        get { return _hotBarSelected; }
        set
        {
            if (_hotBarSelected != value)
            {
                _hotBarSelected = value;
                MoveImage();
                // Call function a whenever _hotBarSelected changes
                // a();
            }
        }
    }

    public void ChangeHotBar()
    {
        if(InputManager.Instance.GetMouseScroll().y > 0)
        {
            SelectedIndex = Mathf.Clamp(_hotBarSelected + 1, 0, 11);
        }
        else if(InputManager.Instance.GetMouseScroll().y < 0)
        {
            SelectedIndex = Mathf.Clamp(_hotBarSelected - 1, 0, 11);
        }
        // If there's no change in _hotBarSelected, MoveImage and a() will still be called
    }
    public void MoveImage()
    {
        _image.anchoredPosition  = new Vector3(SelectedIndex * 100, 0, 0); 
        ChagnePreview();
    }

    public void ChagnePreview()
    {
        int _hotbarIndex = PlayerInventoryManager.Instance.playerInventory.inventorySlots[SelectedIndex].itemId;
        
        if(_hotbarIndex == -1) return;

        // Placeable이라면 PreviewSprite 표시
        if(PlayerInventoryManager.Instance.itemDataBase.Items[_hotbarIndex].ItemType == ItemType.Placeable)
        {
            _uiMouseObject.AllocateSprite(PlayerInventoryManager.Instance.itemDataBase.Items[_hotbarIndex]._previewSprite);
        }
        // 이외라면 X
        else
            _uiMouseObject.DeallocateSprite();
    }

    public void HandleInteract()
    {
        
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePos);
        worldMousePosition.z = 0;
        // worldMousePosition.z = 0;

        _uiMouseObject.transform.position = RoundVector3(worldMousePosition);


        int _hotbarIndex = PlayerInventoryManager.Instance.playerInventory.inventorySlots[SelectedIndex].itemId;
        
        if(!InputManager.Instance.GetHotBarInteractPressed()) return;
        
        else
        {
            if(_hotbarIndex != -1)
            {
                switch (PlayerInventoryManager.Instance.itemDataBase.Items[_hotbarIndex].ItemType)
                {
                    case ItemType.Placeable:
                        PlayerInventoryManager.Instance.itemDataBase.Items[_hotbarIndex].Interact(_uiMouseObject.transform.position);
                        break;
                    case ItemType.Default:
                        break;
                }
            }
        }
    }

    Vector3 RoundVector3(Vector3 inputVector)
    {
        float roundedX = Mathf.Round(inputVector.x);
        float roundedY = Mathf.Round(inputVector.y);
        float roundedZ = Mathf.Round(inputVector.z);

        return new Vector3(roundedX, roundedY, roundedZ);
    }
}
