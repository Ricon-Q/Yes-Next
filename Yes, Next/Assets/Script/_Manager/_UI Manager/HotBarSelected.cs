using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HotBarSelected : MonoBehaviour
{
    [SerializeField] private int _hotBarSelected = 0;
    [SerializeField] private RectTransform _image;
    [SerializeField] private UiMouseObject _uiMouseObject;

    [Header("Slot X Position")]
    [SerializeField] private List<float> _slotXPositions;
    [Header("Camera")]
    [SerializeField] private Camera _camera;

    private int _previewIndex = 0;

    private void Start() 
    {
        MoveImage();    
        ChagnePreview();
    }
    private void Update() 
    {
        ChangeHotBar();
        // if(canInteract)
        HandleInteract();
        
        if(InputManager.Instance.GetTurnPlaceable()) 
        {
            _previewIndex++;
            ChagnePreview();
        }
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
            }
        }
    }

    public void ChangeHotBar()
    {
        // 마우스 스크롤을 이용해서 핫바 이동
        if(InputManager.Instance.GetMouseScroll().y > 0)
        {
            SelectedIndex = Mathf.Clamp(_hotBarSelected + 1, 0, 11);
        }
        else if(InputManager.Instance.GetMouseScroll().y < 0)
        {
            SelectedIndex = Mathf.Clamp(_hotBarSelected - 1, 0, 11);
        }
    }
    public void MoveImage()
    {
        // 마우스 스크롤로 인해 핫바가 이동하면 그에 맞춰서 선택 이미지 이동
        _image.anchoredPosition  = new Vector3(_slotXPositions[_hotBarSelected], -6, 0); 
        ChagnePreview();
    }

    public void ChagnePreview()
    {
        // 선택된 단축키 아이템이 null이 아니라면 (설치형 아이템이라면) 아이템을 UiMouseObject에 Allocate
        int _hotbarIndex = PlayerInventoryManager.Instance.playerInventory.inventorySlots[SelectedIndex].itemId;
        
        if(_hotbarIndex == -1) 
        {
            _uiMouseObject.DeallocateSprite(); 
            return;
        }

        // Placeable이라면 PreviewSprite 표시
        if(PlayerInventoryManager.Instance.itemDataBase.Items[_hotbarIndex].ItemType == ItemType.Placeable)
        {
            _PlaceableItemData _tmp = PlayerInventoryManager.Instance.itemDataBase.Items[_hotbarIndex] as _PlaceableItemData;
            _previewIndex = _previewIndex % _tmp._previewSprites.Count;
            Debug.Log(_previewIndex);
            _uiMouseObject.AllocateSprite(_tmp._previewSprites[_previewIndex], _tmp._placeObjects[_previewIndex]);
        }
        // 이외라면 X
        else
            _uiMouseObject.DeallocateSprite();
    }

    public void HandleInteract()
    {
        // 마우스 위치 설정, uiMouseObject의 프리뷰를 Grid에 Snap
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        Vector3 worldMousePosition = _camera.ScreenToWorldPoint(mousePos);
        worldMousePosition.z = 0;

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
                        if(_uiMouseObject._canPlace == true)
                        {
                            PlaceableObject tmp = new PlaceableObject(SceneManager.GetActiveScene().name, _hotbarIndex, _uiMouseObject.transform.position, _previewIndex);
                            ObjectManager.Instance._placeableObjects.Add(tmp);
                            PlayerInventoryManager.Instance.itemDataBase.Items[_hotbarIndex].Interact(_uiMouseObject.transform.position, _previewIndex);
                        }
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
