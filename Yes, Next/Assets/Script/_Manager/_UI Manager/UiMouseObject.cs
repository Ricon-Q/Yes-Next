using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UiMouseObject : MonoBehaviour
{
    public SpriteRenderer _spriteRenderer;
    [SerializeField] private BoxCollider2D _boxCollider2D;
    private _PlaceableHolder _placeableHolder;

    public bool _canPlace = false;
    //Placeable 아이템이라면 
    public void AllocateSprite(Sprite _previewSprite, _PlaceableHolder placeableItemData)
    {
        _placeableHolder = placeableItemData;
        _spriteRenderer.sprite = _previewSprite;
        _boxCollider2D.offset = placeableItemData._previewCollider.offset;
        _boxCollider2D.size = placeableItemData._previewCollider.size;
    }
    public void DeallocateSprite()
    {
        if(_spriteRenderer.sprite != null)
        {
            _spriteRenderer.sprite = null;
            _boxCollider2D.size = Vector2.zero;
        }
    }

    public virtual void EnablePreview()
    {
        _canPlace = true;
        _spriteRenderer.color = Color.white;
    }

    public virtual void DisablePreview()
    {
        _canPlace = false;
        _spriteRenderer.color = Color.red;
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        Debug.Log(collider.gameObject.name);
        DisablePreview();
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
        DisablePreview();
    }

    private void OnTriggerExit2D(Collider2D collider) 
    {
        AreaData tmp = CameraManager.Instance.areaDatabase.findArea(_PlayerManager.Instance.playerData.currentArea);
        if(_placeableHolder.areaDatas.Contains(tmp))
            EnablePreview();
        else
            DisablePreview();
    }

}
