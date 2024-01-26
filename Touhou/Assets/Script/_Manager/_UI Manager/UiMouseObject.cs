using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UiMouseObject : MonoBehaviour
{
    public SpriteRenderer _spriteRenderer;
    //Placeable 아이템이라면 
    public void AllocateSprite(Sprite _previewSprite)
    {
        _spriteRenderer.sprite = _previewSprite;
    }
    public void DeallocateSprite()
    {
        if(_spriteRenderer.sprite != null)
            _spriteRenderer.sprite = null;
    }

    public virtual void EnablePreview()
    {
        _spriteRenderer.color = Color.green;
    }

    public virtual void DisablePreview()
    {
        _spriteRenderer.color = Color.red;
    }
}
