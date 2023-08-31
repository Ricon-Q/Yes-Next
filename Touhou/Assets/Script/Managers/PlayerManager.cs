using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

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
    public long money = 0; // 현재 자금

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
    Vector3 dirVec;
    GameObject ScanObject;

    private Vector3 playerPosition;
    public InventoryObject inventory;
    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

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
        // //Inventory Save Load
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

    private void HandleMove()
    {
        dirVec = InputManager.Instance.GetMoveDirection();
        rigid.velocity = dirVec * speed;
        // Debug.Log(rigid.velocity);
    }

    private void FixedUpdate()
    {   
        if(DialogueManager.Instance.dialogueIsPlaying 
            || InventoryManager.Instance.isInventoryOpen 
            || ShopManager.Instance.isShopMode)
        {
            return;
        }
        HandleMove();
        DrawScanRay();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
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
    public void SetPlayerPosition(Vector3 DoorWayPosition)
    {
        playerPosition = DoorWayPosition;
    }
}