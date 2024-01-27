using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UiMouseObject : MonoBehaviour
{
    public SpriteRenderer _spriteRenderer;
    [SerializeField] private BoxCollider2D _boxCollider2D;

    public bool _canPlace = false;
    //Placeable 아이템이라면 
    public void AllocateSprite(Sprite _previewSprite)
    {
        _spriteRenderer.sprite = _previewSprite;
        _boxCollider2D.size = new Vector2(_previewSprite.rect.width/16, _previewSprite.rect.height/16);
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
        _spriteRenderer.color = Color.green;
    }

    public virtual void DisablePreview()
    {
        _canPlace = false;
        _spriteRenderer.color = Color.red;
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        // Debug.Log(collider.gameObject.name);
        DisablePreview();
    }

    private void OnTriggerExit2D(Collider2D collider) 
    {
        EnablePreview();
    }

}
