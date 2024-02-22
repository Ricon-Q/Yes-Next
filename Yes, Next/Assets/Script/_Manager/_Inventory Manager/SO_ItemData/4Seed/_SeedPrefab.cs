using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _SeedPrefab : MonoBehaviour
{
    public _SeedItemData _seedItemData;
    public Vector3 _position;
    public _TimeData _plantedData;
    public SpriteRenderer _spriteRenderer;

    private void Start()
    {
        UpdateSprite();
    }

    private void Update()
    {
        UpdateSprite();
    }
    
    public void SetData(Vector3 position, _SeedItemData seedItemData, _TimeData today)
    {
        _position = position;
        _seedItemData = seedItemData;
        _plantedData = today;
        UpdateSprite();

        Debug.Log(_plantedData.month + " : " + _plantedData.day + " time : " + _plantedData.hour + " : " + _plantedData.minute);
    }

    private void UpdateSprite()
    {
        int daysSincePlanted = _TimeManager.Instance.DaysSince(_plantedData);
        if (daysSincePlanted < _seedItemData._sprites.Count)
        {
            _spriteRenderer.sprite = _seedItemData._sprites[daysSincePlanted];
        }
        else
        {
            _spriteRenderer.sprite = _seedItemData._sprites[_seedItemData._sprites.Count - 1]; // 마지막 스프라이트 유지
        }
    }
}
