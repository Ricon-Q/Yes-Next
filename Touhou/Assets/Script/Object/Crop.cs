using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// 해당 스크립트는 농사에 사용되는 농작물 스크립트이다
// 1. n개의 스프라이트를 가지며 농작물의 종류에 따라 다르다
// 2. 오브젝트 생성시 TimeMager의 day를 받아와 plantedDay에 저장한다
// 3. 현재의 day를 currentDay에 저장
// 4. (currentDay - plantedDay)를 계산하여 스프라이트 순서 결정
// 5. 수확시 수확물을 플레이어의 인벤토리에 추가한다

public class Crop : MonoBehaviour
{
    [SerializeField]private Sprite[] sprites;
    private int currentDay;
    public CropData cropData;
    public bool spawnedBefore = false;
    private SpriteRenderer spriteRenderer;
    public string objectName;
    
    private void Awake() 
    {
        cropData = new CropData(
                                    TimeManager.Instance.timeData.day, 
                                    this.transform.position, 
                                    this.objectName,
                                    SceneManager.GetActiveScene().name
                                );
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[0];
    }
    private void Start()
    {
        CheckSpawnedBefore();
    }
    private void Update() 
    {
        currentDay = TimeManager.Instance.timeData.day;
        SetSprite();
    }
    
    private void SetSprite()
    {
        spriteRenderer.sprite = sprites[Math.Clamp(currentDay - cropData.plantedDay, 0, sprites.Length-1)];
    }

    public void CheckSpawnedBefore()
    {
        if(spawnedBefore) return;
        CropManager.Instance.AddCropData(cropData);
    }
}
