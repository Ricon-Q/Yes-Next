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
    [SerializeField]private int plantedDay;
    public int id;
    // [SerializeField]private GameObject originalPrefab;

    private int currentDay;
    private CropObjectData data;

    private TimeManager timeManager;
    private ObjectManager objectManager;
    // private DialogueManager dialogueManager;

    private SpriteRenderer spriteRenderer;
    public TextMeshProUGUI textMeshPro;
    // private GameObject CropOutput;
    
    private void Awake()
    {
        data = new CropObjectData();
        timeManager = TimeManager.Instance;
        objectManager = ObjectManager.Instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
        plantedDay = timeManager.time.day;
        spriteRenderer.sprite = sprites[0];
    }
    private void Start() 
    {
        SetupData();
        objectManager.AddObjectData(SceneManager.GetActiveScene().name, data);
        // textMeshPro = GameObject.Find("DialogueManager/Canvas/Image")?.GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Update() 
    {
        currentDay = timeManager.time.day;
        SetSprite();
    }

    
    private void SetupData()
    {
        data.name = "CropTest";
        data.obj = Resources.Load<GameObject>("Prefabs/CropTest");
        // data.code = objectManager.sceneObjectData[SceneManager.GetActiveScene().name].Count;
        data.position = this.transform.position;
        data.plantedDay = plantedDay;
        // data.sprites = sprites;
    }

    public void SetPlantedDay(int day)
    {
        plantedDay = day;
    }
    
    private void SetSprite()
    {
        spriteRenderer.sprite = sprites[Math.Clamp(currentDay - plantedDay, 0, sprites.Length-1)];
    }

    // public void Interact(GameObject scanObj)
    // {
    //     textMeshPro.text = 
    //     "Crop Name : " + scanObj.name + 
    //     "\nDay : " + (Math.Clamp(currentDay - plantedDay, 0, sprites.Length-1)+1) + " / " + sprites.Length;

    //     // dialogueManager.ScriptInteract(text)
    // }
}
