using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//키보드 방향키를 이용한 상하좌우 이동

public class PlayerManager : MonoBehaviour
{

    public float speed;     // 이동 속도
    public float maxHealth = 100;        // 최대 체력
    public float currentHealth = 100;    // 현재 체력
    public float maxFatigue = 100;       // 최대 피로도
    public float currentFatigue = 100;   // 현재 피로도
    public float maxHunger = 100;        // 최대 만복도
    public float currentHunger = 100;    // 현재 만복도





    // 테스트용 변수들
    public GameObject plantPrefab; // 식물 게임 오브젝트 변수
    private GameObject currentPlant;
    //
    private static PlayerManager instance;

    public static PlayerManager Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    Rigidbody2D rigid;
    float h;
    float v;
    bool isHorizonMove;
    Vector3 dirVec;
    GameObject ScanObject;

    private Vector3 playerPosition;
    // private SaveManager saveManager;
    private TimeManager timeManager;
    [SerializeField] private DialogueManager dialogueManager;
    public InventoryObject inventory;
    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        // saveManager = FindObjectOfType<SaveManager>();
        timeManager = FindObjectOfType<TimeManager>();

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        InputMove();

        //Scan Object
        if(Input.GetButtonDown("Interact") && ScanObject != null)
        {
            ScanInteract(ScanObject);
        }

        // 테스트
        if (Input.GetKeyDown(KeyCode.X))
        {
            PlantNewPlant();    
        }   
        //

        //Inventory Save Load
        if(Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
            Debug.Log("Saved");
        }    
        if(Input.GetKeyDown(KeyCode.A))
        {
            inventory.Load();
            
            Debug.Log("Loaded");
        }
    }

    private void FixedUpdate()
    {
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * speed;

        DrawScanRay();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Debug.Log("Scene Loaded");
        transform.position = playerPosition;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    private void InputMove()
    {
        h = dialogueManager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v = dialogueManager.isAction ? 0 : Input.GetAxisRaw("Vertical");

        bool hDown = dialogueManager.isAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = dialogueManager.isAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = dialogueManager.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = dialogueManager.isAction ? false : Input.GetButtonUp("Vertical");

        // Set isHorizonMove
        if(hDown || vUp)
            isHorizonMove = true;
        else if(vDown || hUp)
            isHorizonMove = false;

        // Set
        if(vDown && v == 1)
            dirVec = Vector3.up;
        else if(vDown && v == -1)
            dirVec = Vector3.down;
        else if(hDown && h == -1)
            dirVec = Vector3.left;
        else if(hDown && h == 1)
            dirVec = Vector3.right;
    }

    private void DrawScanRay()
    {
        //Ray
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Interactable"));

        if(rayHit.collider != null)
        {
            ScanObject = rayHit.collider.gameObject;
        }
        else
        {
            ScanObject = null;
        }
    }

    private void ScanInteract(GameObject ScanObject)
    {
        // Debug.Log("ScanInteract " + ScanObject);
        dialogueManager.Interact(ScanObject);
    }

    // public void SavePlayerPosition()
    // {
    //     saveManager.SavePlayerPosition(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name, playerTransform.position);
        
    // }

    /*public void MovePlayerToPosition(Vector3 targetPosition)
    {
        transform.position = targetPosition;
    }*/

    public void increaseMinute(int minute)
    {
        timeManager.increaseMinute(minute);
    }

    public void increaseHour(int hour)
    {
        timeManager.increaseHour(hour);
    }
    
    public void increaseDay(int day)
    {
        timeManager.increaseDay(day);
    }

    public void setPlayerPosition(Vector3 DoorWayPosition)
    {
        playerPosition = DoorWayPosition;
    }

    //테스트용 함수
    private void PlantNewPlant()
    {
        if (plantPrefab != null)
        {
            Vector3 spawnPosition = new Vector3(rigid.position.x, rigid.position.y, 0) + dirVec * 0.7f; // Raycast 끝 부분에 생성
            currentPlant = Instantiate(plantPrefab, spawnPosition, Quaternion.identity);
        }
    }
    //
}